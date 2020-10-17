using System;
using Godot;

public class PressureButton : StaticBody2D {
    [Export] bool staysPressed;
    public bool Pressed = false;
    [Signal] public delegate void Toggled(bool toggle);
    private Sprite sprite;
    Area2D interact;

    public void Release() {
    }
    public override void _Ready() {
        interact = GetNode<Area2D>("Interact");
        interact.Connect("body_entered", this, nameof(BodyEntered));
        sprite = GetNode<Sprite>("Sprite");
    }

    public override void _PhysicsProcess(float delta) {
        if (Pressed && !staysPressed && interact.GetOverlappingBodies().Count == 0) {
            Pressed = false;
            EmitSignal(nameof(Toggled), false);
            sprite.Frame = sprite.Frame - 1;
        }
    }

    public void BodyEntered(PhysicsBody2D body) {
        if (!Pressed) {
            Pressed = true;
            EmitSignal(nameof(Toggled), true);
            sprite.Frame = sprite.Frame + 1;
        }
    }
}
