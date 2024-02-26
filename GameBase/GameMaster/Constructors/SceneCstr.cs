/// <summary>
/// SceneCstr extends the capability of Dictionaries to hold more than 2 values.
/// path = Path to the scene
/// musicPath = Path to the music
/// prettyName = Name to display to the end user
/// pauseAllowed = Can the game be paused in this scene?
/// </summary>
public class SceneCstr {
    public SceneCstr(string path, string musicPath, string prettyName, bool pauseAllowed) {
        this.path = path;
        this.prettyName = prettyName;
        this.pauseAllowed = pauseAllowed;
        this.musicPath = musicPath;
    }
    public string path;
    public string musicPath;
    public string prettyName;
    public bool pauseAllowed;
}