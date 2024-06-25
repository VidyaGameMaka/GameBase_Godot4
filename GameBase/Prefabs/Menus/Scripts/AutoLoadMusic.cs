using Godot;
using System;
using System.Collections.Generic;

public partial class AutoLoadMusic : Node {

    public override void _Ready() {
        ChangeMusic();
    }

    public void ChangeMusic() {
        string scenePath = GetTree().CurrentScene.SceneFilePath;
        foreach (var item in GameMaster.sceneData.scnDict) {
            if (item.Value.path == GetTree().CurrentScene.SceneFilePath) {
                if (item.Value.musicPath == "") {
                    GD.Print("(AutoloadMusic): " + scenePath + " Empty Music Path, Stopping Music");
                    Snd.inst.StopMusic();
                }
                else {
                    Snd.inst.PlayMusic(item.Value.musicPath);
                    GD.Print("(AutoloadMusic): " + scenePath + " Playing: " + item.Value.musicPath);
                }
                break;
            }
        }

    }

}
