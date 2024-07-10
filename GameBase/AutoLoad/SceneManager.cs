using Godot;
using System;
using System.Collections.Generic;

public partial class SceneManager : Node {

    public static SceneManager instance;

    private eSceneNames previousSceneName;
    
    public static eSceneNames currentScene { get; private set; } = eSceneNames.Main;

    public override void _Ready() {
        instance = this;

        //This will tell us that SceneManager object was included in autoload.
        GD.Print("(SceneManager) SceneManager Ready");
    }

    public void ReturnToMainMenu() {
        string ChangeToPath = GameMaster.sceneData.mainMenu.path;

        GameMaster.FullSave();

        //Change Scene
        GetTree().ChangeSceneToFile(ChangeToPath);
    }

    public void ChangeScene(eSceneNames mySceneName) {
        string myPath = GameMaster.sceneData.scnDict[mySceneName].path;
        GameMaster.pauseAllowed = GameMaster.sceneData.scnDict[mySceneName].pauseAllowed;
        GameMaster.playerData.savedScene = mySceneName;
        previousSceneName = mySceneName;
        currentScene = mySceneName;
        GetTree().ChangeSceneToFile(myPath);
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
        GD.Print("(SceneManager) Saving and Quitting");
        GameMaster.SaveGameData();

        GameMaster.FullSave();
        GetTree().Quit();
    }
}