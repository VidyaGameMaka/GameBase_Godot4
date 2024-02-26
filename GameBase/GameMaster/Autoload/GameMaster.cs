using Godot;
using System;

public partial class GameMaster : Node {

    public static GameMaster instance;

    //Release.Features.Patch
    public static string gameVersion = "0.1.1 Build Date: 2/25/2024";

    //PlayerData: The slot number that the game will save and load to by default
    public static int currentSlotNum = 0;

    //GameData: The slot number that the game will save and load to by default
    private static int gameDataSlotNum = 1;

    public static bool paused = false;
    public static bool pauseAllowed = false;
    public static bool ignoreUserInput = false;

    public static bool showDebuggingMessages = true;

    //Base Player Data
    public static PlayerData playerData = new PlayerData();

    //Runtime Player Data - The Player Data object the game will actually use during gameplay.
    public static PlayerData runPlayerData = new PlayerData();

    //Game Data
    public static GameData gameData = new GameData();

    //Data Types Enum
    public enum SaveTypes { playerDat, gameDat }

    //Save Slots
    public static PlayerData loadedPlayerDataSlot1 = new PlayerData();
    public static PlayerData loadedPlayerDataSlot2 = new PlayerData();
    public static PlayerData loadedPlayerDataSlot3 = new PlayerData();

    //Audio Bus Indexes
    public static int master_index, music_index, sfx_index, voice_index, male_index, female_index;

    public override void _Ready() {
        instance = this;

        //To recreate the save files, unncomment ResetAllData();
        //This will destroy all data in the save files and overwrite them with
        //the default data in GameData slot 1 and PlayerData slots 1, 2 and 3
        //Be sure to restore the comment after using this to reset the data.
        //ResetAllData();

        //Load Game System Data
        LoadGameData();

        //Load saved Player Data into seperate fields so they can be displayed / manipulated on the save/load menu
        LoadPlayerDataSlot(1);
        LoadPlayerDataSlot(2);
        LoadPlayerDataSlot(3);

        //Apply GameData Video Settings on Game Start
        ApplyGameDataVideoSettings();

        //Apply GameData Audio Settings on Game Start
        SetupAudioBusIndexes();
        ApplyGameDataAudioSettings();

        //This will tell us that GameMaster object was included in autoload.
        GD.Print("(GameMaster) Gamemaster Ready");
    }

    public static void ApplyGameDataVideoSettings() {

        //Determine which monitor the window is on
        int currentScreen = DisplayServer.WindowGetCurrentScreen();

        //Make full screen on the monitor the window is on
        if (gameData.isFullScreen == true) {
            DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen, currentScreen);
        }

        if (gameData.isFullScreen == false) {
            //Make window mode on the monitor the window is on
            DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed, currentScreen);

            //Use gameData ResolutionIndex to find the resolution that is currently set.
            //This will return a Vector2I That WindowSetSize is expecting.
            Vector2I currentRez = gameData.windowResolutions[gameData.resolutionIndex];
            DisplayServer.WindowSetSize(currentRez);

            //Center window on current screen.            
            Vector2I currentWinSize = DisplayServer.ScreenGetSize(currentScreen);
            DisplayServer.WindowSetPosition(new Vector2I(
                currentWinSize.X/2 - currentRez.X /2,
                currentWinSize.Y/ 2 - currentRez.Y / 2),
                currentScreen);
        }

        //Save Game Data when Resolution is applied.
        SaveGameData();
    }

    //Assign Bus Indexes
    private void SetupAudioBusIndexes() {        
        master_index = AudioServer.GetBusIndex("Master");
        music_index = AudioServer.GetBusIndex("Music");
        sfx_index = AudioServer.GetBusIndex("SFX");
        voice_index = AudioServer.GetBusIndex("Voice");
        male_index = AudioServer.GetBusIndex("Male");
        female_index = AudioServer.GetBusIndex("Female");
    }

    //Apply Bus settings from gameData to each Audio Channel
    private static void ApplyGameDataAudioSettings() {
        AudioServer.SetBusVolumeDb(master_index, Mathf.LinearToDb(gameData.masterVolume));
        AudioServer.SetBusVolumeDb(music_index, Mathf.LinearToDb(gameData.musicVolume));
        AudioServer.SetBusVolumeDb(sfx_index, Mathf.LinearToDb(gameData.sfxVolume));
        AudioServer.SetBusVolumeDb(voice_index, Mathf.LinearToDb(gameData.voiceVolume));
        AudioServer.SetBusVolumeDb(male_index, Mathf.LinearToDb(gameData.maleVolume));
        AudioServer.SetBusVolumeDb(female_index, Mathf.LinearToDb(gameData.femaleVolume));
    }

    /// <summary>
    /// This is the recommended method to use to save data.
    /// Combined Method that saves PlayerData and GameData with a single call.
    /// No argument required as GameMaster.currentSlotNum is default
    /// </summary>
    public static void FullSave() {
        //Save Player Data using the currentSlotNum
        Save(SaveTypes.playerDat, currentSlotNum);
        //Save Game Data
        Save(SaveTypes.gameDat, gameDataSlotNum);
    }

    /// <summary>
    /// Loads the specified slot into the runPlayerData object
    /// </summary>
    /// <param name="mySlot"></param>
    public static void LoadRunPlayerData(int mySlot) {
        if (mySlot == 1) { runPlayerData = loadedPlayerDataSlot1; }
        if (mySlot == 2) { runPlayerData = loadedPlayerDataSlot1; }
        if (mySlot == 3) { runPlayerData = loadedPlayerDataSlot1; }
    }

    /// <summary>
    /// Resets the Runtime player data object to default values.
    /// </summary>
    public static void ClearRunPlayerData() {
        runPlayerData = new PlayerData();
    }


    //Simplified Player Data Methods
    /// <summary>
    /// Loads the playerData static with the slot number specified
    /// </summary>
    /// <param name="slotNum"></param>
    private static void SavePlayerData(int slotNum) { Save(SaveTypes.playerDat, slotNum); }
    
    /// <summary>
    /// //Loads the selected slot file data into the runtime playerData slot.
    /// </summary>
    /// <param name="slotNum">1, 2, or 3</param>    
    public static void LoadPlayerData(int slotNum) { Load(SaveTypes.playerDat, slotNum, false); }
    
    /// <summary>
    /// //Loads the playerData into one of the setup slot objects
    /// </summary>
    /// <param name="slotNum">1, 2, or 3</param>
    public static void LoadPlayerDataSlot(int slotNum) { Load(SaveTypes.playerDat, slotNum, true); }
    public static void DeletePlayerData(int slotNum) { Delete(SaveTypes.playerDat, slotNum); }

    //Simplified Game Data Methods
    public static void SaveGameData() { Save(SaveTypes.gameDat, gameDataSlotNum); }
    public static void LoadGameData() { Load(SaveTypes.gameDat, gameDataSlotNum); }
    public static void DeleteGameData() { Delete(SaveTypes.gameDat, gameDataSlotNum); }

    /// <summary>
    /// Saves the runtime gameData or playerData to the specified slot.
    /// Performs filesystem calls. This function is not intended to be called directly.
    /// </summary>
    /// <param name="mySaveType"></param>
    /// <param name="slotNum"></param>
    private static void Save(SaveTypes mySaveType, int slotNum) {
        //Don't save slot 0
        if (slotNum == 0) { return; }

        //Make the string path to the save file
        string myFilePath = "user://" + mySaveType.ToString() + slotNum + ".sav";

        //Save File Object
        using var saveGame = FileAccess.Open(myFilePath, FileAccess.ModeFlags.Write);

        //Empty String Object to hold the data
        string jsonString = string.Empty;

        //Convert Entire Class to Json String using NewtonSoft.Json
        if (mySaveType == SaveTypes.playerDat) {
            jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(playerData);
        }
        if (mySaveType == SaveTypes.gameDat) {
            jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(gameData);
        }

        //Write String to File
        saveGame.StoreLine(jsonString);
    }

    /// <summary>
    /// Load a file from the OS filesystem into specified object.
    /// </summary>
    /// <param name="mySaveType"></param>
    /// <param name="slotNum"></param>
    /// <param name="loadToSlot"></param>
    private static void Load(SaveTypes mySaveType, int slotNum, bool loadToSlot = false) {
        //Create string to file path.
        string myFilePath = "user://" + mySaveType.ToString() + slotNum + ".sav";

        //Can't open file. Initialize the slot.
        if (FileAccess.FileExists(myFilePath) == false) {
            if (showDebuggingMessages) { GD.Print("(GameMaster) File doesnt exist: " + myFilePath); }
            initializeSlots(mySaveType, slotNum);
            return;
        }

        // Open File
        var saveGame = FileAccess.Open(myFilePath, FileAccess.ModeFlags.Read);

        //Read File Contents. File is only one line, so it does not need to be iterated.
        var jsonString = saveGame.GetLine();

        if (mySaveType == SaveTypes.playerDat) {
            if (loadToSlot == false) {
                Newtonsoft.Json.JsonConvert.PopulateObject(jsonString, playerData);
            } else {
                if (slotNum == 1) { Newtonsoft.Json.JsonConvert.PopulateObject(jsonString, loadedPlayerDataSlot1); }
                if (slotNum == 2) { Newtonsoft.Json.JsonConvert.PopulateObject(jsonString, loadedPlayerDataSlot2); }
                if (slotNum == 3) { Newtonsoft.Json.JsonConvert.PopulateObject(jsonString, loadedPlayerDataSlot3); }
            }
        }

        if (mySaveType == SaveTypes.gameDat) { Newtonsoft.Json.JsonConvert.PopulateObject(jsonString, gameData); }
    }

    /// <summary>
    /// Delete data file. Does not actually delete files from the OS filesystem.
    /// Instead it overwrites the data in memory and overwrites it on the specified file.
    /// </summary>
    /// <param name="mySaveType"></param>
    /// <param name="slotNum"></param>
    private static void Delete(SaveTypes mySaveType, int slotNum) {
        string myFilePath = "user://" + mySaveType.ToString() + slotNum + ".sav";

        //Overwrite Player Data for Specified Slot
        if (mySaveType == SaveTypes.playerDat) { initializeSlots(SaveTypes.playerDat, slotNum); }

        //Overwrite Default Game Data for Specified Slot
        if (mySaveType == SaveTypes.gameDat) { initializeSlots(SaveTypes.gameDat, gameDataSlotNum); }

        //Save to file
        Save(mySaveType, slotNum);
    }

    /// <summary>
    /// If a data file is missing, then it will be created.
    /// </summary>
    /// <param name="mySaveType"></param>
    /// <param name="slotNum"></param>
    private static void initializeSlots(SaveTypes mySaveType, int slotNum) {
        if (mySaveType == SaveTypes.playerDat) {
            if (slotNum == 0) { playerData = new PlayerData(); }
            if (slotNum == 1) { loadedPlayerDataSlot1 = new PlayerData(); SavePlayerData(slotNum); }
            if (slotNum == 2) { loadedPlayerDataSlot2 = new PlayerData(); SavePlayerData(slotNum); }
            if (slotNum == 3) { loadedPlayerDataSlot3 = new PlayerData(); SavePlayerData(slotNum); }
        }
        if (mySaveType == SaveTypes.gameDat) { gameData = new GameData(); SaveGameData(); }
    }

    /// <summary>
    /// Resets all of the Saved Data.
    /// </summary>
    public static void ResetAllData() {
        DeleteGameData();
        DeletePlayerData(1);
        DeletePlayerData(2);
        DeletePlayerData(3);

        GD.Print("(GameMaster) ResetAllData - All Game Data Reset!");
    }

}
