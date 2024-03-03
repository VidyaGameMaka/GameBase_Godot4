using Godot;
using System;

/// <summary>
/// Quick Code Snippet to implement a simple scene change when a button is pressed.
/// </summary>
public partial class ButtonChangeScene : Button {

	[Export] private eSceneNames myScene;

	public void _on_button_up() {
        SceneManager.instance.ChangeScene(myScene);
    }

}
