using System;
using Godot;

public class Hand : Position2D {
    [Export] public bool IsFacingLeft = false;
    public bool LockDirection => baguette != null;
    public override void _Ready() {

    }

    Baguette baguette = null;

    public override void _Process(float delta) {
        base._Process(delta);
        if (Input.IsActionJustPressed("use")) {
            baguette = Baguette.Instance();
            GetNode("../../").AddChild(baguette);
            baguette.Connect(nameof(Baguette.GrowthStoped), this, nameof(ReleaseBaguette));
            PlaceBaguette();
        }
        if (Input.IsActionJustReleased("use")) {
            baguette?.StopGrowth();
        }
    }

    private void PlaceBaguette() {
        if (baguette != null) {
            baguette.GlobalPosition = GlobalPosition + new Vector2((IsFacingLeft ? -0.5f : +0.5f) * baguette.Length, 0);
        }
    }

    public override void _PhysicsProcess(float delta) {
        base._PhysicsProcess(delta);
        PlaceBaguette();
    }

    public void ReleaseBaguette() {
        baguette.Disconnect(nameof(Baguette.GrowthStoped), this, nameof(ReleaseBaguette));
        baguette = null;
    }
}
