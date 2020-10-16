using System;
using Godot;

public class Baguette : RigidBody2D {
    public static Baguette Instance() {
        return (Baguette) ResourceLoader.Load<PackedScene>("res://Nodes/Gameplay/Bread/Baguette.tscn").Instance();
    }
    [Export] public float Length;
    [Export] public float GrowDuration;
    [Export] NodePath areaPath;
    [Signal] public delegate void GrowthStoped();
    public Area2D Area;
    public bool IsGrowing;
    private float growthTime;
    public AnimationPlayer Anim;
    public bool IsAnchoredLeft;
    public bool IsColliding => collisionCount > 0;
    private int collisionCount = 0;
    public override void _Ready() {
        Anim = GetNode<AnimationPlayer>("Anim");
        Area = GetNode<Area2D>(areaPath);
    }

    public bool IsTangible { get; private set; } = true;

    private uint cachedMask;
    private uint cachedLayer;
    public void Intangible() {
        if (IsTangible) {
            IsTangible = false;
            cachedMask = CollisionMask;
            cachedLayer = CollisionLayer;
            CollisionMask = 0;
            CollisionLayer = 0;
        }
    }

    public void Tangible() {
        if (!IsTangible) {
            IsTangible = true;
            CollisionMask = cachedMask;
            CollisionLayer = cachedLayer;
        }
    }

    public void StartGrowth() {
        if (!IsGrowing) {
            IsGrowing = true;
            Anim.Play("length");
            GravityScale = 0f;
            CallDeferred("set", "mode", RigidBody2D.ModeEnum.Kinematic);
        }
    }

    public override void _PhysicsProcess(float delta) {
        base._PhysicsProcess(delta);
        if (IsGrowing) {
            growthTime += delta;
            if (growthTime >= GrowDuration || Area.GetOverlappingBodies().Count > 0) {
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
