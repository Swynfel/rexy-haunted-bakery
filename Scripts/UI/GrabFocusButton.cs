using System;
using Godot;
using Utils;

public class GrabFocusButton : Button {
    public override void _Ready() {
        Connect("mouse_entered", this, nameof(MouseEntered));
        // Connect("mouse_exited", this, nameof(MouseExited));
        Connect("focus_entered", this, nameof(FocusEntered));
        Connect("focus_exited", this, nameof(FocusExited));
        Connect("pressed", this, nameof(ButtonPressed));
    }

    private void MouseEntered() {
        GrabFocus();
    }

    // private void MouseExited() {
    //     ReleaseFocus();
    // }

    protected virtual void FocusEntered() {
    }

    protected virtual void FocusExited() {
    }

    protected virtual void ButtonPressed() {

    }
}