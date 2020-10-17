using System;
using Godot;

public class Level : Node2D {
    private bool postInit = false;
    public override void _Ready() {
        if (Global.Chapter == ChapterId.NONE) {
            DisplayDebugLevel();
        }
        postInit = true;
    }
    private void DisplayDebugLevel() {
        string[] parts = Name.Substring("Level".Length).Split('-');
        Global.Chapter = (ChapterId) parts[0].ToInt();
        Global.Level = parts[1].ToInt();
    }
    public void FinishLevel() {
        Global.Level++;
        GetTree().ChangeScene(Global.CurrentScene());
    }

    public void FinishChapter() {
        ScoreBoard scoreBoard = (ScoreBoard) ResourceLoader.Load<PackedScene>("res://Scenes/Score.tscn").Instance();
        GetTree().Root.AddChild(scoreBoard);
    }

    public override void _Process(float delta) {
        if (postInit) {
            postInit = false;
            GUI.Instance.On();
        }
    }
}
