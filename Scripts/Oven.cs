using System;
using Godot;

public class Oven : StaticBody2D {
    [Export] NodePath spritePath;
    [Export] NodePath interactPath;
    Area2D interact;
    ShaderMaterial shader;
    public override void _Ready() {
        interact = GetNode<Area2D>(interactPath);
        interact.Connect("body_entered", this, nameof(BodyEntered));
        interact.Connect("body_exited", this, nameof(BodyExited));
        shader = (ShaderMaterial) GetNode<Sprite>(spritePath).Material;
    }


    public void Select() {
        shader.SetShaderParam("selected", 1f);
    }

    public void UnSelect() {
        shader.SetShaderParam("selected", 0f);
    }

    public void BodyEntered(PhysicsBody2D body) {
        if (body is Rexy rexy) {
            rexy.Claw.NextToOven(this);
        }
    }

    public void BodyExited(PhysicsBody2D body) {
        if (body is Rexy rexy) {
            rexy.Claw.LeaveOven(this);
        }
    }
}
