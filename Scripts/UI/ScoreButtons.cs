using System;
using Godot;

public class ScoreButtons : VBoxContainer {
    [Export] NodePath menuButtonPath;
    [Export] NodePath retryButtonPath;
    [Export] NodePath continueButtonPath;
    [Export] NodePath clearButtonPath;
    public void ActivateButtons() {
        Button clearButton = GetNode<Button>(clearButtonPath);
        Button menuButton = GetNode<Button>(menuButtonPath);
        Button retryButton = GetNode<Button>(retryButtonPath);
        Button continueButton = GetNode<Button>(continueButtonPath);
        if (OS.IsDebugBuild()) {
            clearButton.Connect("pressed", this, nameof(ClearScore));
            clearButton.Show();
        } else {
            clearButton.QueueFree();
        }
        menuButton.Connect("pressed", this, nameof(Menu));
        retryButton.Connect("pressed", this, nameof(Retry));
        continueButton.Connect("pressed", this, nameof(Continue));
    }

    public void ClearScore() {
        ScoreBoard.Instance.ClearScore();
    }
    public void Menu() {
        Global.LoadMenu();
        ScoreBoard.Instance.QueueFree();
    }
    public void Retry() {
        Global.LoadChapter(Global.Chapter);
        ScoreBoard.Instance.QueueFree();
    }
    public void Continue() {
        Global.LoadChapter(Global.Chapter.Next());
        ScoreBoard.Instance.QueueFree();
    }

}
