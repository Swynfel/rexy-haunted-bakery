using System;
using Godot;

public class Level : Node2D {
    [Export] ChapterId chapter = ChapterId.Tutorial;
    [Export] int level = 99;

    public override void _Ready() {
        Global.Chapter = chapter;
        Global.Level = level;
        GetTree().Paused = false;
    }
    public void FinishLevel() {
        GetTree().ChangeScene(Global.Scene(chapter, level + 1));
    }

    public void FinishChapter() {
        ScoreBoard scoreBoard = (ScoreBoard) ResourceLoader.Load<PackedScene>("res://Scenes/Score.tscn").Instance();
        GetTree().Root.AddChild(scoreBoard);
        GetTree().Paused = true;
    }
}
