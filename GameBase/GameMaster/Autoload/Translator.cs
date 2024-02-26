using Godot;
using System;

public partial class Translator : Node {

	public static Translator instance;

    public static Lng rLng = new Lng();

    public override void _Ready() {
	    ChangeLanguage();
	}

    public void ChangeLanguage() {
        switch (GameMaster.gameData.language) {
            case Languages.en:
                rLng = new en();
                break;
            case Languages.sp:
                rLng = new sp();
                break;
            default:
                break;
        }

        rLng.Run();

        GD.Print("(Translator) Language Selected: " + GameMaster.gameData.language);
        GD.Print("(Translator) Test: " + Lng.tester);

    }


}
