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
        menuButton.Connect("button_pressed", this, nameof(Menu));
        retryButton.Connect("button_pressed", this, nameof(Retry));
        continueButton.Connect("button_pressed", this, nameof(Continue));
    }

    public void Menu() {
        GD.Print("[TODO]: Open Menu");
    }
    public void Retry() {
        GetTree().ChangeScene(InfoHeader.Chapter.Scene());
    }
    public void Continue() {
        GetTree().ChangeScene(InfoHeader.Chapter.Next().Scene());
    }

}
