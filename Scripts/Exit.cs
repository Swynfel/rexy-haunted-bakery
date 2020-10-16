using System;
using Godot;

public class Exit : Area2D {
    public override void _Ready() {
        Connect("body_entered", this, nameof(BodyEntered));
    }

    public void BodyEntered(PhysicsBody2D body) {
        if (body is Rexy rexy) {
            GetParent<Level>().FinishLevel();
        }
    }
}
