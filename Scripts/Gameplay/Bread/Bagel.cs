using System;
using Godot;

public class Bagel : Bread {
    protected override Id GetBreadId() {
        return Id.Bagel;
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
