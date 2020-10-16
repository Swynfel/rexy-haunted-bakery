using System;
using Godot;

public class ScoreBoard : CanvasLayer {
    [Export] NodePath animationPath;
    const float DURATION = 1f;
    public override void _Ready() {
        GetNode<AnimationPlayer>(animationPath).Play("appear");
    }

    public void Done() {
        GD.Print("Done");
    }
}
