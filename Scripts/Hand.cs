using System;
using Godot;

public class Hand : Position2D {
    [Export] public bool IsFacingLeft = false;
    public bool LockDirection => placingBread;
    private bool holdingBread = false;
    private bool placingBread = false;
    private bool readyForPlacement;
    private const float readyCooldown = 0.05f;

    Baguette baguette = null;

    Color placableColor = new Color(1.0f, 1.0f, 1.0f, 0.5f);
    Color notPlacableColor = new Color(1.0f, 0.5f, 0.5f, 0.5f);

    public override void _Process(float delta) {
        base._Process(delta);
        if (NearbyOven != null && Input.IsActionJustPressed("get")) {
            HoldBaguette();
        } else if (holdingBread) {
            if (!IsInstanceValid(baguette)) {
                holdingBread = false;
                placingBread = false;
                baguette = null;
                return;
            }
            readyForPlacement = baguette.Area.GetOverlappingBodies().Count == 0;
            baguette.Modulate = placingBread ? Colors.White : readyForPlacement ? placableColor : notPlacableColor;
            if (Input.IsActionJustPressed("use") && !placingBread && readyForPlacement) {
                placingBread = true;
                baguette.StartGrowth();
            }
            if (Input.IsActionJustReleased("use") && placingBread) {
                baguette.StopGrowth();
            }
        }
    }

    public Oven NearbyOven;

    public void NextToOven(Oven oven) {
        if (NearbyOven != null) {
            NearbyOven.UnSelect();
        }
        NearbyOven = oven;
        NearbyOven.Select();
    }

    public void LeaveOven(Oven oven) {
        if (oven == NearbyOven) {
            NearbyOven.UnSelect();
            NearbyOven = null;
        }
    }

    public void HoldBaguette() {
        if (holdingBread) {
            baguette.QueueFree();
        } else {
            holdingBread = true;
        }
        baguette = Baguette.Instance();
        GetNode("../../").AddChild(baguette);
        baguette.Connect(nameof(Baguette.GrowthStoped), this, nameof(ReleaseBaguette));
        baguette.Intangible();
        PlaceBaguette();
    }

    private void PlaceBaguette() {
        if (baguette != null) {
            baguette.GlobalPosition = GlobalPosition + new Vector2((IsFacingLeft ? -0.5f : +0.5f) * baguette.Length, 0);
        }
    }

    public override void _PhysicsProcess(float delta) {
        base._PhysicsProcess(delta);
        if (baguette != null && !IsInstanceValid(baguette)) {
            holdingBread = false;
            placingBread = false;
            baguette = null;
            return;
        }
        PlaceBaguette();
    }

    public void ReleaseBaguette() {
        holdingBread = false;
        placingBread = false;
        baguette.Disconnect(nameof(Baguette.GrowthStoped), this, nameof(ReleaseBaguette));
        baguette.Tangible();
        baguette = null;

    }
}
