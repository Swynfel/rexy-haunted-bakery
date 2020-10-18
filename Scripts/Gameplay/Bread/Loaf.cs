using System;
using Godot;

public class Loaf : Bread {
    protected override Id GetBreadId() {
        return Id.Loaf;
    }

    protected override void PlaceInternal() {
        Anim.Play("place");
    }

    protected override void PlacePhysicsProcess(float delta) {
        LinearVelocity = new Vector2(0, 0);
    }
    protected override void StopPlaceInternal() {
    }
}
