using System;
using Godot;

public class TargetArea : Area2D {
    [Export] bool endOfChapter = false;
    public override void _Ready() {
        Connect("body_entered", this, nameof(BodyEntered));
        GetNode<AnimationPlayer>("Anim").Play("base");
    }

    public void BodyEntered(PhysicsBody2D body) {
        if (body is Rexy rexy) {
            if (endOfChapter) {
                GetParent<Level>().FinishChapter();
            } else {
                GetParent<Level>().FinishLevel();
            }
        }
    }
}
