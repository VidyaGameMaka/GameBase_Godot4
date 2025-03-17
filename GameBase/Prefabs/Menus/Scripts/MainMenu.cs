using Godot;

public partial class MainMenu : Node2D {

    public static MainMenu instance;

    [Export] private int defaultLayer;

    [Export] private CanvasLayer[] canvasLayers;

    [ExportGroup("Main Menu Buttons")]
    [Export] private Button startButton;
    [Export] private Button audioButton;
    [Export] private Button videoButton;
    [Export] private Button languageButton;
    [Export] private Button quitButton;

    public override void _Ready() {
        instance = this;

        SetupButtons();

        ShowLayer(defaultLayer);
    }

    private void SetupButtons() {
        startButton.Text = Lng.mainMenu[0]; //Start
        audioButton.Text = Lng.mainMenu[1]; //Audio
        videoButton.Text = Lng.mainMenu[2]; //Video
        quitButton.Text = Lng.mainMenu[4]; //Quit
        languageButton.Text = Lng.mainMenu[5]; //Language
    }

    public void ShowLayer(int myInt) {
        HideAllLayers();
        canvasLayers[myInt].Show();
    }

    private void HideAllLayers() {
        foreach (var layer in canvasLayers) {
            layer.Hide();
        }
    }

    private void _on_start_button_button_up() {
        ShowLayer(0);
    }


    private void _on_audio_button_button_up() {
        ShowLayer(1);
    }

    private void _on_video_button_button_up() {
        ShowLayer(2);
    }

    public void _on_language_button_button_up() {
        SceneManager.instance.ChangeScene(eSceneNames.LanguageChooser);
    }

    public void _on_quit_button_button_up() {
        SceneManager.instance.QuitGame();
    }

}
