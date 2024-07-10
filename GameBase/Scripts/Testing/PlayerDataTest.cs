using Godot;

public partial class PlayerDataTest : Node {

    [Export] public Label myLabel;

    public override void _Ready() {
        myLabel.Text = "GameMaster Current Slot: " + GameMaster.currentSlotNum + "\n\n";

        myLabel.Text = "GetTree().CurrentScene.SceneFilePath: \n" + GetTree().CurrentScene.SceneFilePath + "\n\n";

        myLabel.Text += "SceneManager Current Scene: " + SceneManager.currentScene + "\n";
        myLabel.Text += "SceneManager Current Scene Path: \n" + GameMaster.sceneData.scnDict[SceneManager.currentScene].path + "\n\n";

        myLabel.Text += "Sample Dictionary Text: " + GameMaster.playerData.sampleDictionary["test"];
    }

    public void _on_return_button_button_up() {
        SceneManager.instance.ReturnToMainMenu();
    }

    public void _on_level_1_button_button_up() {
        SceneManager.instance.ChangeScene(eSceneNames.Level1);
    }

    public void _on_level_2_button_button_up() {
        SceneManager.instance.ChangeScene(eSceneNames.Level2);
    }

}
