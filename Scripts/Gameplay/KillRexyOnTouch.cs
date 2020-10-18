using System;
using Godot;

public class KillRexyOnTouch : Area2D {
    public override void _Ready() {
        Connect("body_entered", this, nameof(BodyEntered));
    }

    public void BodyEntered(Node body) {
        if (body is Rexy rexy) {
            rexy.Kill();
        }
    }
}
