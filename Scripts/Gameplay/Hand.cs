using System;
using Godot;

public class Hand : Position2D {
    [Export] public bool IsFacingLeft = false;
    public bool LockDirection => placingBread;
    private bool holdingBread = false;
    private bool placingBread = false;
    private bool readyForPlacement;
    private const float readyCooldown = 0.05f;
    AudioStreamPlayer takeSFX;
    AudioStreamPlayer placeSFX;

    Bread bread = null;

    Color placableColor = new Color(1.0f, 1.0f, 1.0f, 0.5f);
    Color notPlacableColor = new Color(1.0f, 0.5f, 0.5f, 0.5f);

    public override void _Ready() {
        takeSFX = GetNode<AudioStreamPlayer>("TakeSFX");
        placeSFX = GetNode<AudioStreamPlayer>("PlaceSFX");
    }

    public override void _Process(float delta) {
        base._Process(delta);
        if (NearbyOven != null && Input.IsActionJustPressed("get")) {
            HoldBread(NearbyOven.Bread);
        } else if (holdingBread) {
            if (!IsInstanceValid(bread)) {
                holdingBread = false;
                placingBread = false;
                bread = null;
                return;
            }
            readyForPlacement = !bread.IsAreaOverlapping;
            bread.Modulate = placingBread ? Colors.White : readyForPlacement ? placableColor : notPlacableColor;
            if (Input.IsActionJustPressed("use") && !placingBread && readyForPlacement) {
                placingBread = true;
                bread.Place();
            }
            if (Input.IsActionJustReleased("use") && placingBread) {
                bread.StopPlace();
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

    public void HoldBread(Bread.Id breadId) {
        if (holdingBread) {
            bread.QueueFree();
        } else {
            holdingBread = true;
        }
        takeSFX.Play();
        bread = Bread.Instance(breadId);
        GetNode("../../").AddChild(bread);
        bread.Connect(nameof(Bread.Placed), this, nameof(Placed));
        bread.Intangible();
        RepositionBread();
    }

    private void RepositionBread() {
        if (bread != null) {
            bread.GlobalPosition = GlobalPosition + bread.OffsetToRexy(IsFacingLeft);
        }
    }

    public override void _PhysicsProcess(float delta) {
        base._PhysicsProcess(delta);
        if (bread != null && !IsInstanceValid(bread)) {
            holdingBread = false;
            placingBread = false;
            bread = null;
            return;
        }
        RepositionBread();
    }

    public void Placed() {
        holdingBread = false;
        placingBread = false;
        placeSFX.Play();
        bread.Disconnect(nameof(Baguette.Placed), this, nameof(Placed));
        bread.Tangible();
        bread = null;
    }
}
