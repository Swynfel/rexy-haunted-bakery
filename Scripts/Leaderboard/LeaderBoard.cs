using System;
using Godot;
using Utils;

public class LeaderBoard : Control {
    [Export(PropertyHint.File)] string lineTemplate;

    public void ShowNotAllowed() {
        this.QueueFreeChildren();
        Label message = new Label();
        message.Autowrap = true;
        message.Text = "You have not allowed online features\n\nIf you want to see the leaderboard, go to the main menu, then 'Leaderboard / Online Config' and accept 'Fetch Leaderboard'";
        AddChild(message);
    }

    public void UpdateLeaderBoard(Scores.ChapterScore score) {
        if (score == null) {
            return;
        }
        this.QueueFreeChildren();
        PackedScene template = ResourceLoader.Load<PackedScene>(lineTemplate);
        foreach (Scores.ScoreLine values in score.Scores) {
            RankLine line = (RankLine) template.Instance();
            AddChild(line);
            line.SetLine(values.Rank, values.Name, values.Time);
        }
    }
}
