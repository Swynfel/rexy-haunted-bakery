using System;
using Godot;

public class Rexy : KinematicBody2D {

    [Export] public float VerticalAcceleration = 10f;
    public AnimatedSprite Anim;
    public Vector2 Velocity = new Vector2();
    public override void _Ready() {
        Anim = GetNode<AnimatedSprite>("Anim");
        Anim.Playing = true;
    }

    public override void _PhysicsProcess(float delta) {
        Velocity += delta * VerticalAcceleration * Vector2.Down;
        Velocity = MoveAndSlide(Velocity);
    }
}
