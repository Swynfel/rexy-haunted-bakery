using System;
using Godot;

public class Baguette : Bread {
    public static Baguette Instance() {
        return (Baguette) ResourceLoader.Load<PackedScene>("res://Nodes/Gameplay/Bread/Baguette.tscn").Instance();
    }
    [Export] public float Length;
    [Export] public float GrowDuration;
    private float growthTime;

    protected override void PlaceInternal() {
        Anim.Play("length");
        GravityScale = 0f;
        CallDeferred("set", "mode", RigidBody2D.ModeEnum.Kinematic);
    }

    protected override void PlacePhysicsProcess(float delta) {
        growthTime += delta;
        if (growthTime >= GrowDuration || Area.GetOverlappingBodies().Count > 0) {
            StopPlace();
        }
    }
    protected override void StopPlaceInternal() {
        CallDeferred("set", "mode", RigidBody2D.ModeEnum.Rigid);
        GravityScale = 1f;
        Anim.Stop();
        LinearVelocity = new Vector2(0, 0);
    }
    public override Vector2 OffsetToRexy(bool isFacingLeft) {
        return new Vector2((isFacingLeft ? -0.5f : +0.5f) * Length, 0);
    }
}
