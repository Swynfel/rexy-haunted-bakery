using System;
using Godot;

public class Level : Node2D {
    [Export(PropertyHint.File)] string nextScene;
    [Export] bool isTimed;
    [Export] int level;

    public override void _Ready() {
        Global.Level = level;
        GetTree().Paused = false;
    }
    public void FinishLevel() {
        GetTree().ChangeScene(nextScene);
    }

    public void FinishChapter() {
        ScoreBoard scoreBoard = (ScoreBoard) ResourceLoader.Load<PackedScene>("res://Scenes/Score.tscn").Instance();
        GetTree().Root.AddChild(scoreBoard);
        GetTree().Paused = true;
    }
}
