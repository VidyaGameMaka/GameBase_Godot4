using Godot;
using System;
using System.Collections.Generic;

public partial class GameMaster : Node {

    public static GameMaster instance;

    //Release.Features.Patch
    public static string gameVersion = "0.1.1 Build Date: 2/25/2024";

    //PlayerData: The slot number that the game will save and load to by default
    //Slot 0 is the "default" slot but is reserved for debugging only and will not save to file.
    public static int currentSlotNum = 0;

    //GameData: The slot number that the game will save and load to by default
    private static int gameDataSlotNum = 1;

    public static bool paused = false;
    public static bool pauseAllowed = false;
    public static bool ignoreUserInput = false;

    public static bool showDebuggingMessages = true;

    //Runtime Player Data - The PlayerData object the game will actually use during gameplay.
    public static PlayerData rPlayerData = new PlayerData();

    //Rumtime Game Data - The GameData object the game will actually use during gamplay
    public static GameData rGameData = new GameData();

    //Data Types Enum
    public enum SaveTypes { playerDat, gameDat }

    //Save Slots
    public static PlayerData loadedPlayerDataSlot1 = new PlayerData();
    public static PlayerData loadedPlayerDataSlot2 = new PlayerData();
    public static PlayerData loadedPlayerDataSlot3 = new PlayerData();

    //(ExtraSlots) Uncomment these lines to enable additional save slots
    //public static PlayerData loadedPlayerDataSlot4 = new PlayerData();
    //public static PlayerData loadedPlayerDataSlot5 = new PlayerData();
    //public static PlayerData loadedPlayerDataSlot6 = new PlayerData();
    //public static PlayerData loadedPlayerDataSlot7 = new PlayerData();
    //public static PlayerData loadedPlayerDataSlot8 = new PlayerData();
    //public static PlayerData loadedPlayerDataSlot9 = new PlayerData();
    //public static PlayerData loadedPlayerDataSlot10 = new PlayerData();

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

        //(ExtraSlots) Uncomment these lines to enable additional save slots
        //LoadPlayerDataSlot(4);
        //LoadPlayerDataSlot(5);
        //LoadPlayerDataSlot(6);
        //LoadPlayerDataSlot(7);
        //LoadPlayerDataSlot(8);
        //LoadPlayerDataSlot(9);
        //LoadPlayerDataSlot(10);

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
        if (rGameData.screenType == ScreenTypes.FullScreen) {
            DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen, currentScreen);
        }

        if (rGameData.screenType == ScreenTypes.Maximized) {
            DisplayServer.WindowSetMode(DisplayServer.WindowMode.Maximized, currentScreen);
        }


        if (rGameData.screenType == ScreenTypes.Windowed) {
            //Make window mode on the monitor the window is on
            DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed, currentScreen);

            //Use gameData ResolutionIndex to find the resolution that is currently set.
            //This will return a Vector2I That WindowSetSize is expecting.
            Vector2I currentRez = rGameData.windowResolutions[rGameData.resolutionIndex];
            DisplayServer.WindowSetSize(currentRez);

            //Center window on current screen.            
            Vector2I currentWinSize = DisplayServer.ScreenGetSize(currentScreen);
            DisplayServer.WindowSetPosition(new Vector2I(
                currentWinSize.X / 2 - currentRez.X / 2,
                currentWinSize.Y / 2 - currentRez.Y / 2),
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
        AudioServer.SetBusVolumeDb(master_index, Mathf.LinearToDb(rGameData.masterVolume));
        AudioServer.SetBusVolumeDb(music_index, Mathf.LinearToDb(rGameData.musicVolume));
        AudioServer.SetBusVolumeDb(sfx_index, Mathf.LinearToDb(rGameData.sfxVolume));
        AudioServer.SetBusVolumeDb(voice_index, Mathf.LinearToDb(rGameData.voiceVolume));
        AudioServer.SetBusVolumeDb(male_index, Mathf.LinearToDb(rGameData.maleVolume));
        AudioServer.SetBusVolumeDb(female_index, Mathf.LinearToDb(rGameData.femaleVolume));
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
        if (mySlot == 1) { rPlayerData = loadedPlayerDataSlot1; }
        if (mySlot == 2) { rPlayerData = loadedPlayerDataSlot2; }
        if (mySlot == 3) { rPlayerData = loadedPlayerDataSlot3; }

        //(ExtraSlots) Uncomment these lines to enable additional save slots
        //if (mySlot == 4) { rPlayerData = loadedPlayerDataSlot4; }
        //if (mySlot == 5) { rPlayerData = loadedPlayerDataSlot5; }
        //if (mySlot == 6) { rPlayerData = loadedPlayerDataSlot6; }
        //if (mySlot == 7) { rPlayerData = loadedPlayerDataSlot7; }
        //if (mySlot == 8) { rPlayerData = loadedPlayerDataSlot8; }
        //if (mySlot == 9) { rPlayerData = loadedPlayerDataSlot9; }
        //if (mySlot == 10) { rPlayerData = loadedPlayerDataSlot10; }
    }

    /// <summary>
    /// Resets the Runtime player data object to default values.
    /// </summary>
    public static void ClearRunPlayerData() {
        rPlayerData = new PlayerData();
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
        //Slot 0 is reserved as the blank slot so don't save it.
        if (slotNum == 0) { return; }

        //Make the string path to the save file
        string myFilePath = "user://" + mySaveType.ToString() + slotNum + ".sav";

        //Save File Object
        using var saveGame = FileAccess.Open(myFilePath, FileAccess.ModeFlags.Write);

        //Empty String Object to hold the data
        string jsonString = string.Empty;

        //Convert Entire Class to Json String using NewtonSoft.Json
        if (mySaveType == SaveTypes.playerDat) {
            jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(rPlayerData);
        }
        if (mySaveType == SaveTypes.gameDat) {
            jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(rGameData);
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
        //Slot 0 is reserved as the blank slot so don't load it.
        if (slotNum == 0) { return; }

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
                Newtonsoft.Json.JsonConvert.PopulateObject(jsonString, rPlayerData);
            } else {
                if (slotNum == 1) {
                    try { Newtonsoft.Json.JsonConvert.PopulateObject(jsonString, loadedPlayerDataSlot1); } catch { loadedPlayerDataSlot1 = new PlayerData(); }
                }
                if (slotNum == 2) {
                    try { Newtonsoft.Json.JsonConvert.PopulateObject(jsonString, loadedPlayerDataSlot2); } catch { loadedPlayerDataSlot2 = new PlayerData(); }
                }
                if (slotNum == 3) {
                    try { Newtonsoft.Json.JsonConvert.PopulateObject(jsonString, loadedPlayerDataSlot3); } catch { loadedPlayerDataSlot3 = new PlayerData(); }
                }

                //(ExtraSlots) Uncomment these lines to enable additional save slots
                //if (slotNum == 4) { try { Newtonsoft.Json.JsonConvert.PopulateObject(jsonString, loadedPlayerDataSlot4); } catch { loadedPlayerDataSlot4 = new PlayerData(); }}
                //if (slotNum == 5) { try { Newtonsoft.Json.JsonConvert.PopulateObject(jsonString, loadedPlayerDataSlot5); } catch { loadedPlayerDataSlot5 = new PlayerData(); }}
                //if (slotNum == 6) { try { Newtonsoft.Json.JsonConvert.PopulateObject(jsonString, loadedPlayerDataSlot6); } catch { loadedPlayerDataSlot6 = new PlayerData(); }}
                //if (slotNum == 7) { try { Newtonsoft.Json.JsonConvert.PopulateObject(jsonString, loadedPlayerDataSlot7); } catch { loadedPlayerDataSlot6 = new PlayerData(); }}
                //if (slotNum == 8) { try { Newtonsoft.Json.JsonConvert.PopulateObject(jsonString, loadedPlayerDataSlot8); } catch { loadedPlayerDataSlot8 = new PlayerData(); }}
                //if (slotNum == 9) { try { Newtonsoft.Json.JsonConvert.PopulateObject(jsonString, loadedPlayerDataSlot9); } catch { loadedPlayerDataSlot9 = new PlayerData(); }}
                //if (slotNum == 10) { try { Newtonsoft.Json.JsonConvert.PopulateObject(jsonString, loadedPlayerDataSlot10); } catch { loadedPlayerDataSlot10 = new PlayerData(); }}
            }
        }

        if (mySaveType == SaveTypes.gameDat) {
            try { Newtonsoft.Json.JsonConvert.PopulateObject(jsonString, rGameData); } catch { rGameData = new GameData(); }
        }

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
            if (slotNum == 0) { rPlayerData = new PlayerData(); }
            if (slotNum == 1) { loadedPlayerDataSlot1 = new PlayerData(); SavePlayerData(slotNum); }
            if (slotNum == 2) { loadedPlayerDataSlot2 = new PlayerData(); SavePlayerData(slotNum); }
            if (slotNum == 3) { loadedPlayerDataSlot3 = new PlayerData(); SavePlayerData(slotNum); }

            //(ExtraSlots) Uncomment these lines to enable additional save slots
            //if (slotNum == 4) { loadedPlayerDataSlot4 = new PlayerData(); SavePlayerData(slotNum); }
            //if (slotNum == 5) { loadedPlayerDataSlot5 = new PlayerData(); SavePlayerData(slotNum); }
            //if (slotNum == 6) { loadedPlayerDataSlot6 = new PlayerData(); SavePlayerData(slotNum); }
            //if (slotNum == 7) { loadedPlayerDataSlot7 = new PlayerData(); SavePlayerData(slotNum); }
            //if (slotNum == 8) { loadedPlayerDataSlot8 = new PlayerData(); SavePlayerData(slotNum); }
            //if (slotNum == 9) { loadedPlayerDataSlot9 = new PlayerData(); SavePlayerData(slotNum); }
            //if (slotNum == 10) { loadedPlayerDataSlot10 = new PlayerData(); SavePlayerData(slotNum); }
        }
        if (mySaveType == SaveTypes.gameDat) { rGameData = new GameData(); SaveGameData(); }
    }

    /// <summary>
    /// Resets all of the Saved Data.
    /// </summary>
    public static void ResetAllData() {
        DeleteGameData();
        DeletePlayerData(1);
        DeletePlayerData(2);
        DeletePlayerData(3);

        //(ExtraSlots) Uncomment these lines to enable additional save slots
        //DeletePlayerData(4);
        //DeletePlayerData(5);
        //DeletePlayerData(6);
        //DeletePlayerData(7);
        //DeletePlayerData(8);
        //DeletePlayerData(9);
        //DeletePlayerData(10);

        Log("(GameMaster) ResetAllData - All Game Data Reset!");
    }

    /// <summary>
    /// Collects system messages to be displayed in-game.
    /// </summary>
    public static string SystemMessages { get; private set; } = string.Empty;

    private static List<string> SystemMessageList = new List<string>();
    public static void Log(string myInput) {
        GD.Print(myInput);

        if (SystemMessageList.Count > 15) { SystemMessageList.RemoveAt(1); }
        if (SystemMessageList.Count == 0) { SystemMessageList.Add("System Messages:"); }

        SystemMessageList.Add("(" + myInput + ")");

        SystemMessages = String.Join("     ", SystemMessageList);
    }

    public static void SetVideoOnQuit() {
        if (DisplayServer.WindowGetMode() == DisplayServer.WindowMode.Fullscreen) { rGameData.screenType = ScreenTypes.FullScreen; }
        if (DisplayServer.WindowGetMode() == DisplayServer.WindowMode.Maximized) { rGameData.screenType = ScreenTypes.Maximized; }
        if (DisplayServer.WindowGetMode() == DisplayServer.WindowMode.Windowed) { rGameData.screenType = ScreenTypes.Windowed; }
    }


}
