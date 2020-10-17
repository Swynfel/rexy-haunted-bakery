using System;
using Godot;

public class ScoreBoard : AutoCanvasWindow {
    public static ScoreBoard Instance;
    [Export] NodePath animationPath;
    [Export] NodePath leaderboardPath;
    [Export] NodePath namePath;
    [Export] NodePath timePath;
    [Export] NodePath bestPath;
    [Export] NodePath rankPath;
    public Control Leaderboard;
    public Label NameField;
    public Label TimeField;
    public Label BestField;
    public Label RankField;
    public override void _Ready() {
        base._Ready();
        GetTree().Paused = true;
        GUI.Instance.Off();
        Instance = this;
        GetNode<AnimationPlayer>(animationPath).Play("appear");
        CallDeferred(nameof(Setup));
    }

    public async void Setup() {
        Leaderboard = GetNode<Control>(leaderboardPath);
        NameField = GetNode<Label>(namePath);
        TimeField = GetNode<Label>(timePath);
        BestField = GetNode<Label>(bestPath);
        RankField = GetNode<Label>(rankPath);
        // Exact
        int centiseconds = (int) (100 * Global.Time);
        InfoHeader.TimeString(centiseconds);
        NameField.Text = Global.PlayerName;
        TimeField.Text = InfoHeader.TimeString(centiseconds);
        BestField.Text = "?:??.??";
        RankField.Text = "??";
        // Optimistic Leaderboard
        UpdateLeaderBoard(Scores.Instance.GetCachedChapterScore(Global.Chapter));
        // Delayed Leaderboard
        await Scores.Instance.AddScoreLine(Global.Chapter, Global.PlayerName, centiseconds);
        UpdateLeaderBoard(await Scores.Instance.GetChapterScore(Global.Chapter, 0));
    }

    public void UpdateLeaderBoard(Scores.ChapterScore score) {
        GD.Print("[TODO]: Update Leaderboard");
    }

    public void Done() {
        GD.Print("Done");
    }
}
