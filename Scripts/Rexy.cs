using System;
using Godot;

public class Rexy : KinematicBody2D {

    [Export] public float VerticalAcceleration = 1000f;
    [Export] public float JumpVelocity = 300f;
    [Export] public float HorizontalSpeed = 100f;
    [Export] public float CoyoteTime = 0.1f;
    [Export] public float Push = 1f;
    private AnimationTree moveAnimation;
    private AnimationTree facingAnimation;
    private Hand hand;
    public Vector2 Velocity = new Vector2();
    public override void _Ready() {
        moveAnimation = GetNode<AnimationTree>("Anim/Move");
        facingAnimation = GetNode<AnimationTree>("Anim/Facing");
        hand = GetNode<Hand>("Hand");
        moveAnimation.Active = true;
        facingAnimation.Active = true;
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
        if (!hand.LockDirection) {
            facingAnimation.Set("parameters/conditions/right", running_right);
            facingAnimation.Set("parameters/conditions/left", running_left);
        }
        moveAnimation.Set("parameters/conditions/running", running);
        moveAnimation.Set("parameters/conditions/not-running", !running);
        // On Ground
        moveAnimation.Set("parameters/conditions/grounded", IsOnGround);
        moveAnimation.Set("parameters/conditions/airborn", !IsOnGround);
        // Jump / Fall
        bool moving_down = Velocity.y > 0;
        moveAnimation.Set("parameters/airborn/conditions/up", !moving_down);
        moveAnimation.Set("parameters/airborn/conditions/down", moving_down);
    }
    public override void _PhysicsProcess(float delta) {
        RefreshPhysicsState(delta);
        Velocity.x = HorizontalSpeed * GetXDirection();
        if (Input.IsActionJustPressed("jump") && IsOnGround) {
            Velocity.y = -JumpVelocity;
            Jumped = true;
        } else if (Input.IsActionJustReleased("jump")) {
            if (Velocity.y < 0) {
                Velocity.y *= 0.4f;
            }
            Jumped = false;
        } else {
            Velocity += delta * VerticalAcceleration * Vector2.Down;
            if (Velocity.y > 0) {
                Jumped = false;
            }
        }
        Velocity = MoveAndSlide(Velocity, upDirection: Vector2.Up, stopOnSlope: false, infiniteInertia: false);
        RefreshAnimationConditions();
        for (int collisionIndex = 0 ; collisionIndex < GetSlideCount() ; collisionIndex++) {
            KinematicCollision2D collision = GetSlideCollision(collisionIndex);
            if (collision.Collider is RigidBody2D body) {
                body.ApplyImpulse(collision.Position - body.Position, -collision.Normal * Push);
            }
        }
    }
}
