using System;
using Godot;

public class ScoreBoard : AutoCanvasWindow {
    public static ScoreBoard Instance;
    [Export] NodePath chapterNamePath;
    [Export] NodePath animationPath;
    [Export] NodePath leaderboardPath;
    [Export] NodePath namePath;
    [Export] NodePath timePath;
    [Export] NodePath bestPath;
    [Export] NodePath rankPath;
    public LeaderBoard Leaderboard;
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
        Leaderboard = GetNode<LeaderBoard>(leaderboardPath);
        NameField = GetNode<Label>(namePath);
        TimeField = GetNode<Label>(timePath);
        BestField = GetNode<Label>(bestPath);
        RankField = GetNode<Label>(rankPath);
        // Exact
        GetNode<Label>(chapterNamePath).Text = $"Chapter {(int) Global.Chapter} - {Global.Chapter.ToString()}";
        int centiseconds = (int) (100 * Global.Time);
        InfoHeader.TimeString(centiseconds);
        NameField.Text = Global.PlayerName;
        TimeField.Text = InfoHeader.TimeString(centiseconds);
        BestField.Text = "?:??.??";
        RankField.Text = "??";
        // Optimistic Leaderboard
        Scores.ChapterScore score = Scores.Instance.GetCachedChapterScore(Global.Chapter);
        score?.ForceInsertLine(Global.PlayerName, centiseconds);
        UpdateScoreBoard(score);
        // Delayed Leaderboard
        if (centiseconds >= 100) {
            // No chapter can be finished in less than one second
            // We can assume we are actually debugging, and so it should not be sent
            await Scores.Instance.AddScoreLine(Global.Chapter, Global.PlayerName, centiseconds);
        }
        UpdateScoreBoard(await Scores.Instance.GetChapterScore(Global.Chapter, 0));
    }
    public async void ClearScore() {
        await Scores.Instance.WipeChapter(Global.Chapter);
        GD.Print("Force refresh");
        UpdateScoreBoard(await Scores.Instance.GetChapterScore(Global.Chapter, 0));
        GD.Print("Force refresh done");
    }

    public void UpdateScoreBoard(Scores.ChapterScore score) {
        Scores.ScoreLine line = score?.FindPlayer(Global.PlayerName);
        if (line != null) {
            BestField.Text = InfoHeader.TimeString(line.Time);
            RankField.Text = line.Rank.ToString();
        }
        Leaderboard.UpdateLeaderBoard(score);
    }

    public void Done() {
        GD.Print("Done");
    }
}
