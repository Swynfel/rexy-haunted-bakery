using System;
using Godot;

public class Level : Node2D {
    [Export] Palette customPalette;
    private bool postInit = false;
    private bool ready = true;
    public override void _Ready() {
        if (Global.Chapter == ChapterId.NONE) {
            DisplayDebugLevel();
        }
        postInit = true;
        if (customPalette != null) {
            GlobalTheme.Instance.ChangePalette(customPalette);
        }
    }
    private void DisplayDebugLevel() {
        string[] parts = Name.Substring("Level".Length).Split('-');
        Global.Chapter = (ChapterId) parts[0].ToInt();
        Global.Level = parts[1].ToInt();
    }
    public void FinishLevel() {
        if (ready) {
            ready = false;
            Global.Level++;
            GetTree().ChangeScene(Global.CurrentScene());
        }
    }

    public void FinishChapter() {
        if (ready) {
            ready = false;
            ScoreBoard scoreBoard = (ScoreBoard) ResourceLoader.Load<PackedScene>("res://Scenes/Score.tscn").Instance();
            GetTree().Root.AddChild(scoreBoard);
        }
    }

    public override void _Process(float delta) {
        if (postInit) {
            postInit = false;
            GUI.Instance.On();
        }
    }
}
