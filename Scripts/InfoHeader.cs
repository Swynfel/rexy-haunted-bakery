using System;
using Godot;

public class InfoHeader : HBoxContainer {
    Label levelLabel;
    Label timeLabel;
    public override void _Ready() {
        timeLabel = GetNode<Label>("Time");
        levelLabel = GetNode<Label>("Level");
    }

    private void UpdateLabel() {
        levelLabel.Text = $"{Global.Chapter} - {Global.Level}";
    }

    private void UpdateTime() {
        float time = Global.Time;
        int minutes = (int) (time / 60);
        time -= 60 * minutes;
        int seconds = (int) (time);
        time -= seconds;
        int dec = (int) (time * 100);
        timeLabel.Text = $"{minutes}:{seconds:00}.{dec:00}";
    }

    public override void _PhysicsProcess(float delta) {
        UpdateTime();
        UpdateLabel();
    }
}
