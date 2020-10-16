using System;
using Godot;

public class ScoreBoard : AutoCanvasWindow {
    public static ScoreBoard Instance;
    [Export] NodePath animationPath;
    public override void _Ready() {
        base._Ready();
        GetTree().Paused = true;
        Instance = this;
        GetNode<AnimationPlayer>(animationPath).Play("appear");
    }

    public void Done() {
        GD.Print("Done");
    }
}
