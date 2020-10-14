using System;
using Godot;

public class Rexy : KinematicBody2D {

    [Export] public float VerticalAcceleration = 1000f;
    [Export] public float JumpVelocity = 300f;
    [Export] public float HorizontalSpeed = 100f;
    [Export] public float CoyoteTime = 0.1f;
    public AnimationTree Move;
    public AnimationTree Facing;
    public Vector2 Velocity = new Vector2();
    public override void _Ready() {
        Move = GetNode<AnimationTree>("Anim/Move");
        Facing = GetNode<AnimationTree>("Anim/Facing");
        Move.Active = true;
        Facing.Active = true;
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

    private void RefreshAnimationConditions() {
        // Running Horizontally
        int x = GetXDirection();
        bool running_right = x > 0;
        bool running_left = x < 0;
        bool running = running_left || running_right;
        Facing.Set("parameters/conditions/right", running_right);
        Facing.Set("parameters/conditions/left", running_left);
        Move.Set("parameters/conditions/running", running);
        Move.Set("parameters/conditions/not-running", !running);
        // On Ground
        Move.Set("parameters/conditions/grounded", IsOnGround);
        Move.Set("parameters/conditions/airborn", !IsOnGround);
        // Jump / Fall
        bool moving_up = Velocity.y > 0;
        Move.Set("parameters/airborn/conditions/up", moving_up);
        Move.Set("parameters/airborn/conditions/down", !moving_up);
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
        RefreshAnimationConditions();
    }
}
