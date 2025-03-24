using Godot;

namespace VGame {
    public partial class LanguageChooserMenu : CanvasLayer {

        [Export] private Button englishButton;
        [Export] private Button spanishButton;
        [Export] private Button frenchButton;

        public override void _Ready() {
            //Set the text for each button
            englishButton.Text = Lng.languages[Languages.english];
            spanishButton.Text = Lng.languages[Languages.spanish];
            frenchButton.Text = Lng.languages[Languages.french];
        }

        //English Button Action
        public void _on_english_button_button_up() {
            //Set the language for the game
            GameMaster.gameData.language = Languages.english;
            buttonClickFinal();
        }

        //Spanish Button Action
        public void _on_spanish_button_button_up() {
            GameMaster.gameData.language = Languages.spanish;
            buttonClickFinal();
        }

        //French Button Action
        public void _on_french_button_button_up() {
            GameMaster.gameData.language = Languages.french;
            buttonClickFinal();
        }

        //Methods all of the Buttons need to call after setting the language
        private void buttonClickFinal() {
            //Change language in the game using Translator
            Translator.instance.ChangeLanguage();
            //Go back to the main scene.
            SceneManager.instance.ChangeScene(eSceneNames.Main);
        }


    }
}
