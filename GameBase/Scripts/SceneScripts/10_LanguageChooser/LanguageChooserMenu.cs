using Godot;

public partial class LanguageChooserMenu : CanvasLayer {


    public void _on_english_button_button_up() {
        GameMaster.gameData.language = Languages.english;
        Translator.instance.ChangeLanguage();
        SceneManager.instance.ChangeScene(eSceneNames.Main);
    }

    public void _on_spanish_button_button_up() {
        GameMaster.gameData.language = Languages.spanish;
        Translator.instance.ChangeLanguage();
        SceneManager.instance.ChangeScene(eSceneNames.Main);
    }

    public void _on_french_button_button_up() {
        GameMaster.gameData.language = Languages.french;
        Translator.instance.ChangeLanguage();
        SceneManager.instance.ChangeScene(eSceneNames.Main);
    }

}
