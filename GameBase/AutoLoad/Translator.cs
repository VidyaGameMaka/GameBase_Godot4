using Godot;

public partial class Translator : Node {

    public static Translator instance;

    private Lng runLang;

    public override void _Ready() {
        instance = this;
        ChangeLanguage();
    }

    public void ChangeLanguage() {
        switch (GameMaster.gameData.language) {
            case Languages.english:
                runLang = new english();
                break;
            case Languages.spanish:
                runLang = new spanish();
                break;
            case Languages.french:
                runLang = new french();
                break;
            default:
                runLang = new english();
                break;
        }

        runLang.Run();

        if (GameMaster.showDebuggingMessages) { GD.Print("(Translator) Language Selected: " + GameMaster.gameData.language); }
    }


}
