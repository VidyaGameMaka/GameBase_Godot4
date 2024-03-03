using Godot;
using System;
using System.Collections.Generic;

public enum eSceneNames {
    Main = 10,
    Level1 = 20,
    Level2 = 30
}

public partial class SceneManager : Node {
    /// <summary>
    /// Use this Dictionary whenever you add or change a scene you want included in the Scene Manager.
    /// Scene Enum, Scene Path, Pretty Scene Name, pauseAllowed
    /// </summary>
    public Dictionary<eSceneNames, SceneCstr> sceneDictionary = new Dictionary<eSceneNames, SceneCstr>() {
        {eSceneNames.Main, new SceneCstr("res://Scenes/10_Main.tscn", "res://GameBase/Audio/Music/blanksound.wav", "Main", false, false) },
        {eSceneNames.Level1, new SceneCstr("res://Scenes/20_Level1.tscn", "res://GameBase/Audio/Music/blanksound.wav", "Level One", true, true) },
        {eSceneNames.Level2, new SceneCstr("res://Scenes/30_Level2.tscn", "res://GameBase/Audio/Music/blanksound.wav", "Level Two", true, true) },
    };

    public static SceneManager instance;

    public override void _Ready() {
        instance = this;

        //This will tell us that SceneManager object was included in autoload.
        GD.Print("(SceneManager) SceneManager Ready");
    }

    public void ChangeScene(eSceneNames mySceneName) {        
        string myPath = sceneDictionary[mySceneName].path;

        //Set pause allowed to match scene we're switching to
        GameMaster.pauseAllowed = sceneDictionary[mySceneName].pauseAllowed;

        

        //If the scene being switched to is allowed to be saved, update the savedScene on rPlayerData
        if (sceneDictionary[mySceneName].saveAllowed == true) {
            GameMaster.rPlayerData.savedScene = mySceneName;
        }
        
        //Change scene
        GetTree().ChangeSceneToFile(myPath);
        GD.Print("Changed to Scene: " + myPath);
        GD.Print("Pause allowed: " + sceneDictionary[mySceneName].pauseAllowed);
        GD.Print("Save Allowed: " + sceneDictionary[mySceneName].saveAllowed);


        //Save Data
        GameMaster.FullSave();
    }

    //Receive notification from the Operating System's Window Manager
    public override void _Notification(int what) {
        if (what == NotificationWMCloseRequest) {
            GD.Print("(SceneManager) Quit Requested by Window Manager.");
            //Save the Current Game on SceneManager and Quit
            QuitGame();
        }
    }

    //Save GameData and PlayerData then Quit
    public void QuitGame() {
        GD.Print("(SceneManager) Save Window Settings from DisplayServer");
        GameMaster.SetVideoOnQuit();

        GD.Print("(SceneManager) Saving and Quitting");
        GameMaster.FullSave();

        //Done, Quit
        GetTree().Quit();
    }

}