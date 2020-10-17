using System;
using Godot;

public class ToggleTile : StaticBody2D {
    [Export] NodePath buttonPath;
    [Export] bool isOpposite;
    private Sprite sprite;
    private CollisionShape2D collision;
    public void Release() {
    }
    private uint mask;
    private uint layer;
    public override void _Ready() {
        mask = CollisionMask;
        layer = CollisionLayer;
        sprite = GetNode<Sprite>("Sprite");
        GetNode(buttonPath).Connect(nameof(PressureButton.Toggled), this, nameof(Toggled));
        Toggled(false);
    }
    public void Toggled(bool toggle) {
        if (toggle != isOpposite) {
            // Show
            sprite.Frame = 1;
            CollisionMask = mask;
            CollisionLayer = layer;
        } else {
            // Hide
            sprite.Frame = 0;
            CollisionMask = 0;
            CollisionLayer = 0;
        }
    }
}
