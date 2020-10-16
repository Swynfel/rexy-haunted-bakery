using System;
using Godot;

public class ScoreBoard : AutoCanvasWindow {
    [Export] NodePath animationPath;
    const float DURATION = 1f;
    public override void _Ready() {
        base._Ready();
        GetNode<AnimationPlayer>(animationPath).Play("appear");
    }

    public void Done() {
        GD.Print("Done");
    }
}
