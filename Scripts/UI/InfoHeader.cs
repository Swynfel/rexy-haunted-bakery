using System;
using Godot;

public class InfoHeader : HBoxContainer {
    public static ColorRect Background;
    Label levelLabel;
    Label timeLabel;
    public override void _Ready() {
        timeLabel = GetNode<Label>("Time");
        levelLabel = GetNode<Label>("Level");
        Background = GetNode<ColorRect>("../Background");
    }

    private void UpdateLabel() {
        levelLabel.Text = Global.LevelFullName();
    }

    public static string TimeString(int time) {
        int minutes = (int) (time / 6000);
        time -= 6000 * minutes;
        int seconds = (int) (time / 100);
        time -= 100 * seconds;
        return $"{minutes}:{seconds:00}.{time:00}";
    }

    private void UpdateTime() {
        timeLabel.Text = TimeString((int) (100 * Global.Time));
    }

    public override void _PhysicsProcess(float delta) {
        UpdateTime();
        UpdateLabel();
    }
}
