using System;
using Godot;

public class ScoreButtons : VBoxContainer {
    [Export] NodePath menuButtonPath;
    [Export] NodePath retryButtonPath;
    [Export] NodePath continueButtonPath;
    public void ActivateButtons() {
        Button menuButton = GetNode<Button>(menuButtonPath);
        Button retryButton = GetNode<Button>(retryButtonPath);
        Button continueButton = GetNode<Button>(continueButtonPath);
        menuButton.Connect("pressed", this, nameof(Menu));
        retryButton.Connect("pressed", this, nameof(Retry));
        continueButton.Connect("pressed", this, nameof(Continue));
    }

    public void Menu() {
        GD.Print("[TODO]: Open Menu");
    }
    public void Retry() {
        Global.Level = 1;
        GetTree().ChangeScene(Global.CHAPTER_INTRO);
        ScoreBoard.Instance.QueueFree();
        Global.Time = 0;
    }
    public void Continue() {
        Global.Level = 1;
        Global.Chapter = Global.Chapter.Next();
        GetTree().ChangeScene(Global.CHAPTER_INTRO);
        ScoreBoard.Instance.QueueFree();
        Global.Time = 0;
    }

}
