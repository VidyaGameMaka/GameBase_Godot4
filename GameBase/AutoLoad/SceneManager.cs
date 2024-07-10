using Godot;

public partial class SceneManager : Node {

    public static SceneManager instance;

    private eSceneNames previousSceneName;

    public static eSceneNames currentScene { get; private set; } = eSceneNames.Main;

    public override void _Ready() {
        instance = this;

        //This will tell us that SceneManager object was included in autoload.
        if (GameMaster.showDebuggingMessages) { GD.Print("(SceneManager) SceneManager Ready"); }
    }

    public void ReturnToMainMenu() {
        //Try to Save
        GameMaster.FullSave();

        //Change Scene to Main Menu
        ChangeScene(eSceneNames.Main);
    }

    //Only use SceneManager.instance.ChangeScene to change scenes for the system to function correctly.
    public void ChangeScene(eSceneNames mySceneName) {
        //Set the path to the scene to change to
        string myPath = GameMaster.sceneData.scnDict[mySceneName].path;

        //Read the sceneData dictionary and determine if pausing is allowed
        GameMaster.pauseAllowed = GameMaster.sceneData.scnDict[mySceneName].pauseAllowed;

        //Read the sceneData dictionary and determine if saving is allowed
        GameMaster.saveAllowed = GameMaster.sceneData.scnDict[mySceneName].saveAllowed;

        //Change the scene to save
        GameMaster.playerData.savedScene = mySceneName;

        //Keep record of the previous scene
        previousSceneName = mySceneName;

        //Set the current scene
        currentScene = mySceneName;

        //Save Game using FullSave
        GameMaster.FullSave();

        //Change Scene
        GetTree().ChangeSceneToFile(myPath);
    }

    //Receive notification from the Operating System's Window Manager
    public override void _Notification(int what) {
        if (what == NotificationWMCloseRequest) {
            if (GameMaster.showDebuggingMessages) { GD.Print("(SceneManager) Quit Requested by Window Manager."); }

            //Save the Current Game on SceneManager and Quit
            QuitGame();
        }
    }

    //Save GameData and PlayerData then Quit
    public void QuitGame() {
        if (GameMaster.showDebuggingMessages) { GD.Print("(SceneManager) Saving and Quitting"); }

        //Save
        GameMaster.FullSave();

        //Quit Game
        GetTree().Quit();
    }
}