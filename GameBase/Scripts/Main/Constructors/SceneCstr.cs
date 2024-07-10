using Godot;
public partial class SceneCstr : GodotObject {
    public SceneCstr(string cPrettyName, bool cPauseAllowed, bool cSaveAllowed, string cPath, string cMusicPath) {
        prettyName = cPrettyName;
        pauseAllowed = cPauseAllowed;
        saveAllowed = cSaveAllowed;
        path = cPath;
        musicPath = cMusicPath;
    }
    public string path;
    public string prettyName;
    public string musicPath;
    public bool pauseAllowed;
    public bool saveAllowed;
}