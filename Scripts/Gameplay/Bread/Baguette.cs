using System;
using Godot;

public class Baguette : Bread {
    [Export] public float Length;

    protected override Id GetBreadId() {
        return Id.Baguette;
    }

    protected override void PlaceInternal() {
        Anim.Play("place");
        GravityScale = 0f;
        CallDeferred("set", "mode", RigidBody2D.ModeEnum.Kinematic);
    }

    protected override void PlacePhysicsProcess(float delta) {
        if (IsAreaOverlapping) {
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
