using System;
using Godot;
using Utils;

public class MainMenuOnlineOptionsButton : GrabFocusButton {
    public override void _Ready() {
        base._Ready();
        if (Global.Chapter == ChapterId.NONE) {
            GrabFocus();
        }
    }

    protected override void ButtonPressed() {
        GD.Print("[TODO]: Online Options");
    }
}