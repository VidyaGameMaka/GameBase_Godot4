using Godot;

namespace VGame {
    public partial class SaveLoadDeleteMenu : CanvasLayer {

        [ExportGroup("Buttons")]
        [ExportSubgroup("Button Set 1")]
        [Export] public Button new1;
        [Export] public Button load1;
        [Export] public Button delete1;

        [ExportSubgroup("Button Set 2")]
        [Export] public Button new2;
        [Export] public Button load2;
        [Export] public Button delete2;

        [ExportSubgroup("Button Set 3")]
        [Export] public Button new3;
        [Export] public Button load3;
        [Export] public Button delete3;

        [ExportSubgroup("System Buttons")]
        [Export] public Button backButton;
        [Export] public Button resetButton;

        [ExportGroup("Labels")]
        [Export] public Label infoLabel1;
        [Export] public Label infoLabel2;
        [Export] public Label infoLabel3;

        public override void _Ready() {
            Init();
        }

        private void Init() {
            //Refresh the playerData to reflect what is saved in the file.
            GameMaster.LoadPlayerDataSlots();

            SetupMenu();
        }

        private void SetupMenu() {
            SetButtonText();

            DisableAllButtons();

            UpdateInfoLabels();

            if (GameMaster.loadedPlayerDataSlots[1].newFile == true) {
                new1.Disabled = false;
                new1.Visible = true;
            }
            else {
                load1.Disabled = false;
                load1.Visible = true;
                delete1.Disabled = false;
                delete1.Visible = true;
            }

            if (GameMaster.loadedPlayerDataSlots[2].newFile == true) {
                new2.Disabled = false;
                new2.Visible = true;
            }
            else {
                load2.Disabled = false;
                load2.Visible = true;
                delete2.Disabled = false;
                delete2.Visible = true;
            }


            if (GameMaster.loadedPlayerDataSlots[3].newFile == true) {
                new3.Disabled = false;
                new3.Visible = true;
            }
            else {
                load3.Disabled = false;
                load3.Visible = true;
                delete3.Disabled = false;
                delete3.Visible = true;
            }
        }

        private void SetButtonText() {
            new1.Text = Lng.saveLoadMenu[0] + " 1"; //new
            load1.Text = Lng.saveLoadMenu[1] + " 1"; //load
            delete1.Text = Lng.saveLoadMenu[2] + " 1"; //delete

            new2.Text = Lng.saveLoadMenu[0] + " 2"; //new
            load2.Text = Lng.saveLoadMenu[1] + " 2"; //load
            delete2.Text = Lng.saveLoadMenu[2] + " 2"; //delete

            new3.Text = Lng.saveLoadMenu[0] + " 3"; //new
            load3.Text = Lng.saveLoadMenu[1] + " 3"; //load
            delete3.Text = Lng.saveLoadMenu[2] + " 3"; //delete

            backButton.Text = Lng.mainMenu[4]; //Back
            resetButton.Text = Lng.saveLoadMenu[3]; //Reset all data
        }


        private void UpdateInfoLabels() {
            infoLabel1.Text = "New file: " + GameMaster.loadedPlayerDataSlots[1].newFile.ToString() + "     ";
            infoLabel1.Text += "Scene: " + GameMaster.loadedPlayerDataSlots[1].savedScene.ToString() + "     ";
            infoLabel1.Text += "Test: " + GameMaster.loadedPlayerDataSlots[1].sampleDictionary["test"];

            infoLabel2.Text = "New file: " + GameMaster.loadedPlayerDataSlots[2].newFile.ToString() + "     ";
            infoLabel2.Text += "Scene: " + GameMaster.loadedPlayerDataSlots[2].savedScene.ToString() + "     ";
            infoLabel2.Text += "Test: " + GameMaster.loadedPlayerDataSlots[2].sampleDictionary["test"];

            infoLabel3.Text = "New file: " + GameMaster.loadedPlayerDataSlots[3].newFile.ToString() + "     ";
            infoLabel3.Text += "Scene: " + GameMaster.loadedPlayerDataSlots[3].savedScene.ToString() + "     ";
            infoLabel3.Text += "Test: " + GameMaster.loadedPlayerDataSlots[3].sampleDictionary["test"];
        }


        public void _on_new_load_button_up(int myInt) {
            //Set the current slot to the one sent by the argument
            GameMaster.currentSlotNum = myInt;
            //Load the slot sent by the argument into GameMaster.playerData
            GameMaster.playerData = GameMaster.loadedPlayerDataSlots[myInt];

            //Make this save slot as not a new file
            GameMaster.playerData.newFile = false;
            //Set Version of Save File
            GameMaster.playerData.saveFileVersion = GameMaster.gameVersion;

            //Change Scene to start the game or anything else that you'd like to do.
            SceneManager.instance.ChangeScene(GameMaster.playerData.savedScene);
        }

        public void _on_delete_button_up(int myInt) {
            if (GameMaster.showDebuggingMessages) { GD.Print("(MainMenu) Slot deleted: " + myInt); }
            GameMaster.DeletePlayerData(myInt);

            //Redo Menu Setup
            Init();
        }

        public void _on_allreset_button_button_up() {
            if (GameMaster.showDebuggingMessages) { GD.Print("(MainMenu) All Data reset!"); }
            GameMaster.DeleteGameData();
            GameMaster.DeletePlayerData(1);
            GameMaster.DeletePlayerData(2);
            GameMaster.DeletePlayerData(3);

            //Redo Menu Setup
            Init();
        }

        public void _on_back_button_button_up() {
            GameMaster.SaveGameData();
            MainMenu.instance.ShowLayer(3);
        }


        private void DisableAllButtons() {
            new1.Visible = false;
            new1.Disabled = true;
            load1.Visible = false;
            load1.Disabled = true;
            delete1.Visible = false;
            delete1.Disabled = true;

            new2.Visible = false;
            new2.Disabled = true;
            load2.Visible = false;
            load2.Disabled = true;
            delete2.Visible = false;
            delete2.Disabled = true;

            new3.Visible = false;
            new3.Disabled = true;
            load3.Visible = false;
            load3.Disabled = true;
            delete3.Visible = false;
            delete3.Disabled = true;
        }

    }
}
