using System;
using Godot;

public class FetchToggle : CheckButton {
    public override void _Ready() {
        Connect("toggled", this, nameof(Toggled));
        Pressed = Global.Online;
    }
    public void Toggled(bool pressed) {
        Global.Online = pressed;
    }
}
