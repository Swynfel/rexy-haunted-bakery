using System;
using Godot;
using Utils;

public class MainMenuQuitButton : GrabFocusButton {
    public override void _Ready() {
        base._Ready();
        if (OS.HasFeature("Web")) {
            QueueFree();
        }
    }
    protected override void ButtonPressed() {
        GetTree().Quit();
    }
}