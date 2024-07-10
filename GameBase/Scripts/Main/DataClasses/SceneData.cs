using System.Collections.Generic;

public enum eSceneNames {
    Main = 10,
    Level1 = 20,
    Level2 = 30
}

public class SceneData {

    /// <summary>
    /// Use this Dictionary whenever you add or change a scene you want included in the Scene Manager.
    /// Scene Enum, Pretty Scene Name, pauseAllowed, Scene Path, Music Path
    /// If a music path is not specified for a scene, the music will stop playing.
    /// </summary>
    public Dictionary<eSceneNames, SceneCstr> scnDict = new Dictionary<eSceneNames, SceneCstr>() {
        {eSceneNames.Main, new SceneCstr("Main", false, "res://GameBase/Scenes/10_Main.tscn", "res://GameBase/Audio/Music/KevinMcLeold_CloudDancer.mp3")},
        {eSceneNames.Level1, new SceneCstr("Level One", true, "res://GameBase/Scenes/20_Level1.tscn", "res://GameBase/Audio/Music/Scheming_Weasel_faster_KevinMcLeold_incompetch.mp3") },
        {eSceneNames.Level2, new SceneCstr("Level Two", true, "res://GameBase/Scenes/30_Level2.tscn", "res://GameBase/Audio/Music/Sneaky_Snitch_Kevin_McLeold_incomptech.mp3") },
    };

    public SceneCstr mainMenu = new SceneCstr("Main", false, "res://GameBase/Scenes/10_Main.tscn", "res://GameBase/Audio/Music/KevinMcLeold_CloudDancer.mp3");


}
