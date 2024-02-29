//Inherits from Lng. English data file
public class en : Lng {

    protected override void ImplementationRun() {

        #region Tester
        tester = "English Test";
        #endregion

        #region Base Dictionaries
        //Main Menu
        mainMenu[0] = "Start";
        mainMenu[1] = "Audio";
        mainMenu[2] = "Video";
        mainMenu[3] = "Quit";
        mainMenu[4] = "Back";

        //Audio Menu
        audioMenu[0] = "Master";
        audioMenu[1] = "Music";
        audioMenu[2] = "SFX";
        audioMenu[3] = "Voice";
        audioMenu[4] = "Male";
        audioMenu[5] = "Female";

        //Save/Load/Delete Menu
        saveLoadMenu[0] = "New";
        saveLoadMenu[1] = "Load";
        saveLoadMenu[2] = "Delete";
        saveLoadMenu[3] = "Reset all Data";

        //Video Menu
        videoMenu[0] = "Windowed";
        videoMenu[1] = "Maximized";
        videoMenu[2] = "Full Screen";
        videoMenu[3] = "Window Size";
        #endregion

        #region Example Dialogue
        exampleDialogue[0] = new DiaCstr("This is example dialogue.", "res://GameBase/Audio/Music/blanksound.wav");
        exampleDialogue[1] = new DiaCstr("You can specify the text for each language.", "res://GameBase/Audio/Music/blanksound.wav");
        exampleDialogue[2] = new DiaCstr("As well as a different voice file for each language", "res://GameBase/Audio/Music/blanksound.wav");
        #endregion
    }

}
