using System;
using Godot;

public class PausedMenu : Control {
    [Export] NodePath resumeButtonPath;
    [Export] NodePath retryLevelButtonPath;
    [Export] NodePath retryChapterButtonPath;
    [Export] NodePath onlineButtonPath;
    [Export] NodePath menuButtonPath;
    public override void _Ready() {
        Button resumeButton = GetNode<Button>(resumeButtonPath);
        Button retryLevelButton = GetNode<Button>(retryLevelButtonPath);
        Button retryChapterButton = GetNode<Button>(retryChapterButtonPath);
        Button onlineButton = GetNode<Button>(onlineButtonPath);
        Button menuButton = GetNode<Button>(menuButtonPath);
        resumeButton.Connect("pressed", this, nameof(Resume));
        retryLevelButton.Connect("pressed", this, nameof(RetryLevel));
        retryChapterButton.Connect("pressed", this, nameof(RetryChapter));
        onlineButton.Connect("pressed", this, nameof(OnlineOptions));
        menuButton.Connect("pressed", this, nameof(Menu));
    }

    public void Resume() {
        GUI.Instance.HideMenu();
    }
    public void RetryLevel() {
        GetTree().ChangeScene(Global.CurrentScene());
        GUI.Instance.HideMenu();
    }
    public void RetryChapter() {
        Global.Level = 1;
        GetTree().ChangeScene(Global.CHAPTER_INTRO);
        GUI.Instance.HideMenuInstantly();
        Global.Time = 0;
    }
    public void OnlineOptions() {
        GD.Print("[TODO]: Open Online Options");
    }
    public void Menu() {
        Global.Level = 1;
        Global.Chapter = Global.Chapter.Next();
        GetTree().ChangeScene(Global.CHAPTER_INTRO);
        ScoreBoard.Instance.QueueFree();
        Global.Time = 0;
    }
}
