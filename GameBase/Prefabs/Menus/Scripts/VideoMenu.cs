using Godot;
using System;

public partial class VideoMenu : CanvasLayer {

    [ExportGroup("Labels")]
    [Export] private Label screenTypeLabel;
    [Export] private Label windowSizeLabel;

    [ExportGroup("Buttons")]
    [Export] private Button maxBtn;
    [Export] private Button fullBtn;   
    [Export] private OptionButton windowedOptionsButton;
    [Export] private Button backButton;

    private int mySelectedRez = 0;

    public override void _Ready() {       
        //Set Initial Value for the Window Label
        SetLabels();

        //Add the defined resolutions above to the options button
        AddResolutionsToButton();
	}

  
    private void SetLabels() {
        if (GameMaster.gameData.screenType == ScreenTypes.Maximized) { screenTypeLabel.Text = Lng.videoMenu[1]; }
        if (GameMaster.gameData.screenType == ScreenTypes.FullScreen) { screenTypeLabel.Text = Lng.videoMenu[2]; }

        windowSizeLabel.Text = Lng.videoMenu[3]; //"Window Size";      
        backButton.Text = Lng.mainMenu[4]; //back
    }

    private void AddResolutionsToButton() {
        //Add the current resolution to the top of the list.
        Vector2I currentRez = GameMaster.gameData.windowResolutions[GameMaster.gameData.resolutionIndex];
        string currentString = currentRez.X + "x" + currentRez.Y;
        windowedOptionsButton.AddItem(currentString, GameMaster.gameData.resolutionIndex);

        //Iterate through each entry in resolutionList array and add them as strings to the button       
        //List of Resolutions is stored in GameData.cs
        foreach (var item in GameMaster.gameData.windowResolutions) {

            //Only add resolutions to the rest of the button if they are not the current res
            if (item.Key != GameMaster.gameData.resolutionIndex) {
                string myString = item.Value.X + "x" + item.Value.Y;
                windowedOptionsButton.AddItem(myString, item.Key);
            }

        }

    }

    public void _on_windowed_options_button_item_selected(int myInt) {
        //Get the ID from the argument myInt
        int myID = windowedOptionsButton.GetItemId(myInt);
        mySelectedRez = myID;

        GameMaster.gameData.resolutionIndex = mySelectedRez;
        GameMaster.gameData.screenType = ScreenTypes.Windowed;
        GameMaster.ApplyGameDataVideoSettings();
        SetLabels();
    }

    public void _on_maximized_button_button_up() {
        GameMaster.gameData.screenType = ScreenTypes.Maximized;
        GameMaster.ApplyGameDataVideoSettings();
        SetLabels();
    }

    public void _on_fullscreen_button_button_up() {
        GameMaster.gameData.screenType = ScreenTypes.FullScreen;
        GameMaster.ApplyGameDataVideoSettings();
        SetLabels();
    }

    public void _on_back_button_button_up() {
        GameMaster.SaveGameData();
        MainMenu.instance.ShowLayer(0);
    }


}
