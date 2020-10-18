using System;
using Godot;
using Utils;

public class Rexy : KinematicBody2D {
    [Export] public float VerticalAcceleration = 1000f;
    [Export] public float JumpVelocity = 300f;
    [Export] public float JumpTime = 0.4f;
    [Export] public float JumpFloatFactor = 0.9f;
    [Export] public float JumpInteruptFactor = 0.35f;
    [Export] public float HorizontalSpeed = 100f;
    [Export] public float CoyoteTime = 0.1f;
    [Export] public float Push = 1f;
    private AnimationTree moveAnimation;
    private AnimationTree facingAnimation;
    AudioStreamPlayer dieSFK => SoundHandler.Instance.GetNode<AudioStreamPlayer>("DieSFX");
    public Hand Claw;
    private Position2D feet;
    public Vector2 Velocity = new Vector2();
    public override void _Ready() {
        moveAnimation = GetNode<AnimationTree>("Anim/Move");
        facingAnimation = GetNode<AnimationTree>("Anim/Facing");
        Claw = GetNode<Hand>("Hand");
        feet = GetNode<Position2D>("Feet");
        moveAnimation.Active = true;
        facingAnimation.Active = true;
        PixelCamera.Instance.Rexy = this;
    }

    public bool IsOnGround => !Jumped && timeSinceLeftFloor <= CoyoteTime;
    public bool Jumped = false;
    private float timeSinceLeftFloor;
    private float timeSinceJumped;

    public void Kill() {
        dieSFK.Play();
        GetTree().ChangeScene(Global.CurrentScene());
    }

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
            Godot.Collections.Dictionary intersections = GetWorld2d().DirectSpaceState
                .IntersectRay(feet.GlobalPosition, feet.GlobalPosition + new Vector2(0, 2), exclude: Extensions.ArrayFrom(this));
            if (intersections.Count > 1) {
                // intersections["collider"];
                timeSinceLeftFloor = 0f;
            } else {
                timeSinceLeftFloor += delta;
            }
        }
        if (Jumped) {
            timeSinceJumped += delta;
        }
    }

    private bool lastAirborn;
    private void RefreshAnimationConditions() {
        // Running Horizontally
        int x = GetXDirection();
        bool running_right = x > +0.1f;
        bool running_left = x < -0.1f;
        bool running = running_left || running_right;
        if (!Claw.LockDirection) {
            facingAnimation.Set("parameters/conditions/right", running_right);
            facingAnimation.Set("parameters/conditions/left", running_left);
        }
        moveAnimation.Set("parameters/conditions/running", running);
        moveAnimation.Set("parameters/conditions/not-running", !running);
        // On Ground
        bool grounded = IsOnGround && !Jumped;
        bool airborn = !grounded && (Jumped || Math.Abs(Velocity.y) > 1);
        moveAnimation.Set("parameters/conditions/grounded", grounded);
        moveAnimation.Set("parameters/conditions/airborn", airborn);
        if (lastAirborn != airborn) {
            lastAirborn = airborn;
        }
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
            timeSinceJumped = 0;
            timeSinceLeftFloor = float.PositiveInfinity;
        } else {
            if (Jumped) {
                if (Input.IsActionJustReleased("jump")) {
                    if (Velocity.y < 0) {
                        Velocity.y *= 0.35f;
                    }
                    Jumped = false;
                } else if (Velocity.y > 0 || timeSinceJumped >= JumpTime) {
                    Jumped = false;
                }
            }
            float factor = Jumped ? JumpFloatFactor : 1.0f;
            Velocity += delta * factor * VerticalAcceleration * Vector2.Down;
        }
        Vector2 targetVelocity = Velocity;
        Velocity = MoveAndSlide(Velocity, upDirection: Vector2.Up, stopOnSlope: !Jumped && IsOnGround, infiniteInertia: false);
        if (Velocity.y > targetVelocity.y) {
            Velocity.y = targetVelocity.y + 4f * delta * (Velocity.y - targetVelocity.y);
        }
        RefreshAnimationConditions();
        for (int collisionIndex = 0 ; collisionIndex < GetSlideCount() ; collisionIndex++) {
            KinematicCollision2D collision = GetSlideCollision(collisionIndex);
            if (collision.Collider is RigidBody2D body) {
                float dot = collision.Remainder.Dot(collision.Normal);
                body.ApplyImpulse(collision.Position - body.Position, dot * collision.Normal * Push);
            }
        }
    }
}
