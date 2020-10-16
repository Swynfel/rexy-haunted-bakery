using System;
using Godot;

public class Level : Node2D {
    [Export(PropertyHint.File)] string nextScene;
    [Export] bool isTimed;

    public override void _Ready() {
        InfoHeader.Instance.SetLevelName(Name);
    }
    public void FinishLevel() {
        GetTree().ChangeScene(nextScene);
    }

    public void FinishChapter() {
        ScoreBoard scoreBoard = (ScoreBoard) ResourceLoader.Load<PackedScene>("res://Scenes/Score.tscn").Instance();
    }
}
