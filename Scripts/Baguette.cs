using System;
using Godot;

public class Baguette : RigidBody2D {
    public static Baguette Instance() {
        return (Baguette) ResourceLoader.Load<PackedScene>("res://Nodes/Baguette.tscn").Instance();
    }
    [Export] public float Length;
    [Export] public float GrowDuration;
    [Signal] public delegate void GrowthStoped();
    public bool IsGrowing;
    private float growthTime;
    public AnimationPlayer Anim;
    public bool IsAnchoredLeft;
    public bool IsColliding => collisionCount > 0;
    private int collisionCount = 0;
    public override void _Ready() {
        Anim = GetNode<AnimationPlayer>("Anim");
        Connect("body_entered", this, nameof(BodyEntered));
        Connect("body_exited", this, nameof(BodyExited));
    }

    public void StartGrowth() {
        if (!IsGrowing) {
            IsGrowing = true;
            Anim.Play("length");
            GravityScale = 0f;
            CallDeferred("set", "mode", RigidBody2D.ModeEnum.Kinematic);
        }
    }

    public void BodyEntered(Node body) {
        if (!body.IsInGroup("Rexy")) {
            collisionCount++;
            StopGrowth();
        }
    }
    public void BodyExited(Node body) {
        if (!body.IsInGroup("Rexy")) {
            collisionCount--;
        }
    }

    public override void _PhysicsProcess(float delta) {
        base._PhysicsProcess(delta);
        if (IsGrowing) {
            growthTime += delta;
            if (growthTime >= GrowDuration) {
                Anim.Advance(Anim.CurrentAnimationPosition - delta);
                StopGrowth();
            }
        }
    }
    public void StopGrowth() {
        if (IsGrowing) {
            IsGrowing = false;
            LinearVelocity = new Vector2(0, 0);
            GravityScale = 1f;
            CallDeferred("set", "mode", RigidBody2D.ModeEnum.Rigid);
            Anim.Stop();
            EmitSignal(nameof(GrowthStoped));
        }
    }
}
