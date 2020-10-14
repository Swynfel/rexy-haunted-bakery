using System;
using Godot;

public class Rexy : KinematicBody2D {

    [Export] public float VerticalAcceleration = 1000f;
    [Export] public float JumpVelocity = 300f;
    [Export] public float HorizontalSpeed = 100f;
    [Export] public float CoyoteTime = 0.1f;
    public AnimatedSprite Anim;
    public Vector2 Velocity = new Vector2();
    public override void _Ready() {
        Anim = GetNode<AnimatedSprite>("Anim");
        Anim.Playing = true;
    }

    public bool IsOnGround => !Jumped && timeSinceLeftFloor <= CoyoteTime;
    public bool Jumped = false;
    private float timeSinceLeftFloor;

    public int GetXDirection() {
        int x = 0;
        if (Input.IsActionPressed("ui_left")) {
            x--;
        }
        if (Input.IsActionPressed("ui_right")) {
            x++;
        }
        return x;
    }
    private void RefreshPhysicsState(float delta) {
        if (IsOnFloor()) {
            timeSinceLeftFloor = 0f;
        } else {
            timeSinceLeftFloor += delta;
        }
    }
    public override void _PhysicsProcess(float delta) {
        RefreshPhysicsState(delta);
        Velocity.x = HorizontalSpeed * GetXDirection();
        if (Input.IsActionJustPressed("jump") && IsOnGround) {
            Velocity.y = -JumpVelocity;
            Jumped = true;
        } else if (Input.IsActionJustReleased("jump")) {
            if (Velocity.y < 0) {
                Velocity.y /= 2;
            }
            Jumped = false;
        } else {
            Velocity += delta * VerticalAcceleration * Vector2.Down;
        }
        Velocity = MoveAndSlide(Velocity, upDirection: Vector2.Up);
    }
}
