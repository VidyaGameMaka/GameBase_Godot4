using Godot;
using System;

public partial class VideoMenu : CanvasLayer {

    [ExportGroup("Window")]
    [Export] private Label winLabel;
    [Export] private CheckButton winCheckButton;

    [ExportGroup("Resolution")]
    [Export] private Label resLabel;
    [Export] private OptionButton resOptionsButton;

    [ExportGroup("System Buttons")]
    [Export] private Button applyButton;
    [Export] private Button backButton;

    private int mySelectedRez = 0;


    public override void _Ready() {
        //Set the check button's toggle mode to match the window's setting
        if (DisplayServer.WindowGetMode() == DisplayServer.WindowMode.Fullscreen) {
            winCheckButton.ButtonPressed = true;
        } else {
            winCheckButton.ButtonPressed = false;
        }

        //Set Initial Value for the Window Label
        SetWindowLabel(winCheckButton.ButtonPressed);

        //Add the defined resolutions above to the options button
        AddResolutionsToButton();

        SetResLabel();
	}

    private void SetResLabel() {
        resLabel.Text = Lng.videoMenu[2]; //Resolution

        applyButton.Text = Lng.videoMenu[3]; //apply
        backButton.Text = Lng.mainMenu[4]; //back
    }

    private void SetWindowLabel(bool isSet) {
        if (isSet == true) {
            winLabel.Text = Lng.videoMenu[1]; //"Full Screen"
        } else {
            winLabel.Text = Lng.videoMenu[0]; //"Windowed"
        }
    }

    private void AddResolutionsToButton() {
        //Add the current resolution to the top of the list.
        Vector2I currentRez = GameMaster.gameData.windowResolutions[GameMaster.gameData.resolutionIndex];
        string currentString = currentRez.X + "x" + currentRez.Y;
        resOptionsButton.AddItem(currentString, GameMaster.gameData.resolutionIndex);

        //Iterate through each entry in resolutionList array and add them as strings to the button       
        //List of Resolutions is stored in GameData.cs
        foreach (var item in GameMaster.gameData.windowResolutions) {

            //Only add resolutions to the rest of the button if they are not the current res
            if (item.Key != GameMaster.gameData.resolutionIndex) {
                string myString = item.Value.X + "x" + item.Value.Y;
                resOptionsButton.AddItem(myString, item.Key);
            }

        }

    }

    public void _on_back_button_button_up() {
        GameMaster.SaveGameData();
        MainMenu.instance.ShowLayer(0);
    }

    public void _on_windowmode_check_button_toggled(bool toggled) {
        GameMaster.gameData.isFullScreen = toggled;
        SetWindowLabel(toggled);
    }

    public void _on_resolution_options_button_item_selected(int myInt) {
        GD.Print("Int Sent: " +  myInt);
        GD.Print("ID: " + resOptionsButton.GetItemId(myInt));
        

        //Get the ID from the argument myInt
        int myID = resOptionsButton.GetItemId(myInt);
        mySelectedRez = myID;
    }

    public void _on_apply_button_button_up() {
        GameMaster.gameData.resolutionIndex = mySelectedRez;
        //Actual application of the Video Settins are in GameMaster so that the code is only written once and
        //can be run at the start of the game when GameMaster initalizes on autoload
        GameMaster.ApplyGameDataVideoSettings();
    }

}
