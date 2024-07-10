using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class GameMaster : Node {

    public static GameMaster instance;

    //Release.Features.Patch
    public static string gameVersion = "0.1.2 Build Date: 6/25/2024";

    //Total Save Slots: Change this to the number of slots you want your game to support.
    private static int totalSaveSlots = 3;

    //---You shouldn't need to change anything beneath this line unless you are customizing your own save system.---

    //The slot number that the game will save and load to by default
    public static int currentSlotNum = 0;

    public static bool paused = false;
    public static bool pauseAllowed = false;
    public static bool saveAllowed = false;
    public static bool ignoreUserInput = false;

    public static bool showDebuggingMessages = true;

    //Data Types Enum
    public enum SaveTypes { playerDat, gameDat }

    //Game Data Runtime
    public static GameData gameData = new GameData();

    //Player Data Runtime
    public static PlayerData playerData = new PlayerData();

    //loaded Player Data Slots: Array that holds the player data either initialized new or loaded from a file.
    public static Dictionary<int, PlayerData> loadedPlayerDataSlots = new Dictionary<int, PlayerData>();

    //Audio Bus Indexes
    public static int master_index, music_index, sfx_index, voice_index, male_index, female_index;

    //Scene Data Object
    public static SceneData sceneData = new SceneData();

    #region Initialize
    public override void _Ready() {
        instance = this;

        //To blank out all existing files, uncomment these three lines
        //SaveGameData();
        //for (int i = 1; i < totalSaveSlots; i++) {
        //    SavePlayerData(i);
        //}      

        //Load Game System Data
        LoadGameData();

        //Load saved Player Data into seperate fields so they can be displayed and manipulated on the save/load menu
        LoadPlayerDataSlots();

        //Apply GameData Video Settings on Game Start
        ApplyGameDataVideoSettings();

        //Apply GameData Audio Settings on Game Start
        SetupAudioBusIndexes();
        ApplyGameDataAudioSettings();

        //This will tell us that GameMaster object was included in autoload.
        if (showDebuggingMessages) { GD.Print("(GameMaster) Gamemaster Ready"); }
    }

    //Read the playerData for each slot from file
    public static void LoadPlayerDataSlots() {
        for (int i = 1; i <= totalSaveSlots; i++) {
            if (showDebuggingMessages) { GD.Print("Loaded Slot: " + i); }
            LoadPlayerDataintoSlot(i);
        }
        if (showDebuggingMessages) { GD.Print("(GameMaster) playerData slots loaded"); }
    }
    #endregion

    #region SAVE, LOAD, RESET & DELETE Methods intended to be used externally
    /// <summary>
    /// SAVE
    /// Combined Method that saves PlayerData and GameData with a single call.
    /// This is the recommended method used to save.
    /// </summary>
    public static void FullSave() {
        //Save Game Data
        Save(SaveTypes.gameDat, 1);
        if (showDebuggingMessages) { GD.Print("(GameMaster) Saved gameData"); }

        //Only Save Player Data using the currentSlotNum if saving is allowed for the current scene
        if (saveAllowed == true) {
            Save(SaveTypes.playerDat, currentSlotNum);
            if (showDebuggingMessages) { GD.Print("(GameMaster) Saved playerData"); }
        }

    }

    /// <summary>
    /// LOAD
    /// Loads specified slot data into the runtime playerData slot.
    /// This is the recommended method used to load player data.
    /// </summary>
    public static void LoadPlayerRuntime(int slotNum) { playerData = loadedPlayerDataSlots[slotNum]; }


    /// <summary>
    /// RESET
    /// Sets playerData to default values. Does not alter files on disk.
    /// </summary>
    public static void ResetPlayerRuntime() { playerData = new PlayerData(); }


    /// <summary>
    /// DELETE
    /// Deletes player data by reinitializing the object and file on disk.
    /// Requires a slotNum to be specified.
    /// </summary>
    /// <param name="slotNum"></param>
    public static void DeletePlayerData(int slotNum) { DelPlayerData(slotNum); }

    #endregion


    #region Short Game Data Methods
    /// <summary>
    /// Save only gameData. Useful only when wanting to save system data on UI.
    /// </summary>
    public static void SaveGameData() { Save(SaveTypes.gameDat, 1); }

    /// <summary>
    /// Delete and reset gameData to default values. Useful only for testing.
    /// </summary>
    public static void DeleteGameData() { Delete(SaveTypes.gameDat, 1); }

    /// <summary>
    /// Load gameData object with data from file. Used only during initialization.
    /// </summary>    
    private static void LoadGameData() { Load(SaveTypes.gameDat, 1); }
    #endregion


    #region Short Player Data Methods
    private static void SavePlayerData(int slotNum) { Save(SaveTypes.playerDat, slotNum); }
    private static void DelPlayerData(int slotNum) { Delete(SaveTypes.playerDat, slotNum); }
    private static void LoadPlayerDataintoSlot(int slotNum) { Load(SaveTypes.playerDat, slotNum); }
    #endregion

    #region Main Save/Load/Delete/Initialize Methods   
    private static void Save(SaveTypes mySaveType, int slotNum) {
        //Don't save slot 0
        if (slotNum == 0) { return; }

        string myFilePath = "user://" + mySaveType.ToString() + slotNum + ".sav";

        //Save File Object using Godot.FileAccess
        using var saveGame = FileAccess.Open(myFilePath, FileAccess.ModeFlags.Write);

        //Empty String Object to store the data
        string jsonString = string.Empty;

        //Convert Entire Class to a one-line Json String using NewtonSoft.Json.
        //playerDat type / playerData
        if (mySaveType == SaveTypes.playerDat) {
            //Serialize playerData into jsonString
            jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(playerData);
            //Update Dictionary loadedPlayerDataSlots
            loadedPlayerDataSlots[slotNum] = playerData;
        }
        //gameDat type / gameData
        if (mySaveType == SaveTypes.gameDat) {
            jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(gameData);
        }

        //Write String to File
        saveGame.StoreLine(jsonString);
    }

    /// <summary>
    /// Saves data to a file in the user's application persistent path.
    /// https://docs.godotengine.org/en/stable/tutorials/io/data_paths.html
    /// </summary>
    /// <param name="mySaveType">SaveTypes.playerDat or SaveTypes.gameDat</param>
    /// <param name="slotNum">Int</param>
    private static void Load(SaveTypes mySaveType, int slotNum) {
        //Don't load slot 0
        if (slotNum == 0) { return; }

        //Specify save file path.
        string myFilePath = "user://" + mySaveType.ToString() + slotNum + ".sav";

        //Can't open file. Initialize the slot.
        if (FileAccess.FileExists(myFilePath) == false) {
            if (showDebuggingMessages) { GD.Print("(GameMaster) File doesnt exist: " + myFilePath); }
            InitializeSlot(mySaveType, slotNum);
            return;
        }

        // Open File
        var saveGame = FileAccess.Open(myFilePath, FileAccess.ModeFlags.Read);

        //Read File Contents. File is only one line, so it does not need to be iterated.
        var jsonString = saveGame.GetLine();

        //Load jsonString into gameData
        if (mySaveType == SaveTypes.gameDat) {
            gameData = Newtonsoft.Json.JsonConvert.DeserializeObject<GameData>(jsonString);
        }

        //Load jsonString into loadedPlayerDataSlots[slotnum]
        if (mySaveType == SaveTypes.playerDat) {
            loadedPlayerDataSlots[slotNum] = Newtonsoft.Json.JsonConvert.DeserializeObject<PlayerData>(jsonString);
        }

    }

    /// <summary>
    /// Deletes specified save file by overwriting it with default values in
    /// the specified class: PlayerData.cs or GameData.cs
    /// This only reflects in GameMaster. Your UI code will also need to update itself
    /// after performing this operation.
    /// </summary>
    /// <param name="mySaveType">SaveTypes.playerDat or SaveTypes.gameDat</param>
    /// <param name="slotNum">Int</param>
    private static void Delete(SaveTypes mySaveType, int slotNum) {
        //Specify save file path.
        string myFilePath = "user://" + mySaveType.ToString() + slotNum + ".sav";

        //Overwrite specified player data slot with default values
        if (mySaveType == SaveTypes.playerDat) {
            InitializeSlot(SaveTypes.playerDat, slotNum);
        }

        //Overwrite default game data with default values
        if (mySaveType == SaveTypes.gameDat) {
            InitializeSlot(SaveTypes.gameDat, 1);
        }

        //Save to file
        Save(mySaveType, slotNum);
    }

    /// <summary>
    /// Actual method that performs slot initialization and erasure.
    /// </summary>
    /// <param name="mySaveType"></param>
    /// <param name="slotNum"></param>
    private static void InitializeSlot(SaveTypes mySaveType, int slotNum) {
        if (mySaveType == SaveTypes.playerDat) {
            loadedPlayerDataSlots[slotNum] = new PlayerData();
            SavePlayerData(slotNum);
        }
        if (mySaveType == SaveTypes.gameDat) {
            gameData = new GameData();
            SaveGameData();
        }
    }
    #endregion


    // Video and Audio methods that are used by GameMaster during initialization.   
    #region Video Initialization
    public static void ApplyGameDataVideoSettings() {
        //The stored resolution values are meaningless
        //since Godot does not change the host's screen resolution on FullScreen.
        if (gameData.isFullScreen == true) {
            DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
        }

        //If the game is not FullScreen, then use the stored resolution to set the window size.
        //The window should automatically center onto the primary screen.
        if (gameData.isFullScreen == false) {
            DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);

            //Use ElementAt with the index number from GameMaster.gameData.resolutionIndex to get key and value from the dictionary
            //ElementAt is a method provided by System.Linq.
            int key = gameData.windowResolutions.ElementAt(gameData.resolutionIndex).Key;
            int value = gameData.windowResolutions.ElementAt(gameData.resolutionIndex).Value;
            DisplayServer.WindowSetSize(new Vector2I(key, value));
        }

        //Save Game Data when Resolution is applied.
        SaveGameData();
    }
    #endregion

    #region Audio Initialization
    private void SetupAudioBusIndexes() {
        //Assign Bus Index numbers to their string names on the bus.
        master_index = AudioServer.GetBusIndex("Master");
        music_index = AudioServer.GetBusIndex("Music");
        sfx_index = AudioServer.GetBusIndex("SFX");
        voice_index = AudioServer.GetBusIndex("Voice");
        male_index = AudioServer.GetBusIndex("Male");
        female_index = AudioServer.GetBusIndex("Female");
    }

    public static void ApplyGameDataAudioSettings() {
        //Set the volume of each bus to the stored value using Mathf.LinearToDb to
        //convert the stored float range between (0 to 1) to the audio bus range of (-80 to +6)
        AudioServer.SetBusVolumeDb(master_index, Mathf.LinearToDb(gameData.masterVolume));
        AudioServer.SetBusVolumeDb(music_index, Mathf.LinearToDb(gameData.musicVolume));
        AudioServer.SetBusVolumeDb(sfx_index, Mathf.LinearToDb(gameData.sfxVolume));
        AudioServer.SetBusVolumeDb(voice_index, Mathf.LinearToDb(gameData.voiceVolume));
        AudioServer.SetBusVolumeDb(male_index, Mathf.LinearToDb(gameData.maleVolume));
        AudioServer.SetBusVolumeDb(female_index, Mathf.LinearToDb(gameData.femaleVolume));
    }
    #endregion

}