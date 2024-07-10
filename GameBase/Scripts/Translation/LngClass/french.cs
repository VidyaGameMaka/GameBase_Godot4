//Inherits from Lng. French data file

public class french : Lng {

    protected override void ImplementationRun() {

        #region Base Dictionaries
        //Languages Dictionary is the same in all data sets
        languages[Languages.english] = "English";
        languages[Languages.spanish] = "Español";
        languages[Languages.french] = "Français";

        //Main Menu
        mainMenu[0] = "Commencer";
        mainMenu[1] = "l'audio";
        mainMenu[2] = "Vidéo";
        mainMenu[3] = "Quitter";
        mainMenu[4] = "Retourner";
        mainMenu[5] = "Langue";

        //Audio Menu
        audioMenu[0] = "Maître";
        audioMenu[1] = "Musique";
        audioMenu[2] = "Effets sonores";
        audioMenu[3] = "Voix";
        audioMenu[4] = "Mâle";
        audioMenu[5] = "Femelle";

        //Save/Load/Delete Menu
        saveLoadMenu[0] = "Nouveau";
        saveLoadMenu[1] = "Charger";
        saveLoadMenu[2] = "Supprimer";
        saveLoadMenu[3] = "Réinitialiser toutes les données";

        //Video Menu
        videoMenu[0] = "Fenêtré";
        videoMenu[1] = "Plein écran";
        videoMenu[2] = "Résolution";
        videoMenu[3] = "Appliquer";
        #endregion


    }

}
