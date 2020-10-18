using System;
using Godot;

public abstract class Bread : RigidBody2D {
    [Export] NodePath areaPath;
    [Signal] public delegate void Placed();
    public AnimationPlayer Anim;
    public Area2D Area;
    public bool IsPlaced;
    public bool IsBeingPlaced;
    public override void _Ready() {
        Anim = GetNode<AnimationPlayer>("Anim");
        Area = GetNode<Area2D>(areaPath);
    }

    private bool? cachedIsAreaOverlapping;
    public bool IsAreaOverlapping {
        get {
            if (!cachedIsAreaOverlapping.HasValue) {
                cachedIsAreaOverlapping = (Area.GetOverlappingBodies().Count > 0);
            }
            return cachedIsAreaOverlapping.Value;
        }
    }
    public bool IsTangible { get; private set; } = true;
    private uint cachedMask;
    private uint cachedLayer;
    protected abstract void PlaceInternal();
    protected abstract void PlacePhysicsProcess(float delta);
    protected abstract void StopPlaceInternal();
    private const float BASE_OFFSET = 32f;
    public virtual Vector2 OffsetToRexy(bool isFacingLeft) {
        return new Vector2((isFacingLeft ? -1f : +1f) * BASE_OFFSET, 0);
    }

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
    public virtual void Place() {
        if (!IsBeingPlaced) {
            IsBeingPlaced = true;
            PlaceInternal();
        }
    }
    public override void _PhysicsProcess(float delta) {
        base._PhysicsProcess(delta);
        cachedIsAreaOverlapping = null;
        if (IsBeingPlaced) {
            PlacePhysicsProcess(delta);
        }
    }
    public void StopPlace() {
        if (IsBeingPlaced) {
            IsBeingPlaced = false;
            IsPlaced = true;
            StopPlaceInternal();
            EmitSignal(nameof(Placed));
        }
    }
}
