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
        Global.LoadMenu();
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
