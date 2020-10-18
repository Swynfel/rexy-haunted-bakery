using System;
using Godot;

public class Ghost : Area2D {

    [Export] float reactionTime;
    [Export] float speed;
    [Export] float timeDecay = 0.5f;
    float reactingTime;
    AudioStreamPlayer eatSFX;

    public override void _Ready() {
        eatSFX = GetNode<AudioStreamPlayer>("EatAudio");
    }

    Vector2 Velocity;
    Bread target;

    public override void _PhysicsProcess(float delta) {
        bool breadWasEaten = false;
        foreach (Node node in GetOverlappingBodies()) {
            if (node is Bread breadBody) {
                breadBody.QueueFree();
                breadWasEaten = true;
            }
        }
        foreach (Node node in GetOverlappingAreas()) {
            if (node is Bread breadBody) {
                breadBody.QueueFree();
                breadWasEaten = true;
            }
        }
        if (breadWasEaten) {
            eatSFX.Play();
        }
        FindTargetBread();
        if (target != null) {
            Vector2 direction = (target.GlobalPosition - GlobalPosition);
            direction = direction.Normalized();
            Vector2 targetVelocity = direction * speed;
            float kept = (float) Math.Exp(-delta / timeDecay);
            Velocity = kept * Velocity + (1 - kept) * targetVelocity;
            GlobalPosition += direction * speed * delta;
        }

    }
    public void FindTargetBread() {
        if (!IsInstanceValid(this.target)) {
            this.target = null;
        }
        float targetDistance = float.PositiveInfinity;
        if (this.target != null) {
            targetDistance = (target.GlobalPosition - GlobalPosition).Length();
            // Shorten distance to favor previous target
            targetDistance = 0.9f * targetDistance - 2f;
        }
        foreach (Bread bread in GetTree().GetNodesInGroup("Bread")) {
            float distance = (bread.GlobalPosition - GlobalPosition).Length();
            if (distance < targetDistance) {
                targetDistance = distance;
                target = bread;
            }
        }
    }
}
