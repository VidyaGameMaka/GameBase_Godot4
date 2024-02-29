using Godot;
using System;
using System.Collections.Generic;

public enum ScreenTypes {
    Windowed = 10,
    Maximized = 20,
    FullScreen = 30,
}

public class GameData {  

    //The default resolution and also the resolution the user selected
    public int resolutionIndex = 3;

    //If true, run the game full screen
    public ScreenTypes screenType = ScreenTypes.Windowed;

    //Windowed Resolution Presets, Only Come into Effect when ScreenMode is Windowed.
    //Godot does not change the resolution of the monitor when it goes FullScreen, it uses the screen's resolution as
    //set by the operating system.
    public Dictionary<int, Vector2I> windowResolutions = new Dictionary<int, Vector2I>() {
        { 0, new Vector2I(480, 270) },
        { 1, new Vector2I(960, 540) },
        { 2, new Vector2I(1280, 720) },
        { 3, new Vector2I(1920, 1080) },
        { 4, new Vector2I(2560, 1440) },
        { 5, new Vector2I(3840, 2160) },
    };

    //Dialogue UI Settings
    public float dialogueBG_alpha = 0.4f;
    public int fontSizeMax = 11;

    //Audio Volumes
    public float masterVolume = 1;
    public float sfxVolume = 1;
    public float musicVolume = 0.40f;
    public float voiceVolume = 1;  
    public float maleVolume = 1;
    public float femaleVolume = 1;

    //Default Audio Volumes
    public const float default_masterMaxVolume = 1;
    public const float default_musicMaxVolume = 0.40f;
    public const float default_soundfxMaxVolume = 1;
    public const float default_voiceMaxVolume = 1;
    public const float default_femaleMaxVolume = 1;
    public const float default_maleMaxVolume = 1;

    //Translation System
    public bool languageChosen = false;
    public Languages language = Languages.en;

}