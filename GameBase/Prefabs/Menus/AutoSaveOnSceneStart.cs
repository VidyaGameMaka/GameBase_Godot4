using Godot;
using System;

public partial class AutoSaveOnSceneStart : Node {
	
	public override void _Ready() {

        if (GameMaster.sceneData.scnDict[SceneManager.currentScene].saveAllowed == true) {
            GameMaster.FullSave();
            GD.Print("(AutoSave) Saved Game.");
        }
        
	}

}
