using System;
using Godot;

public class InfoHeader : HBoxContainer {
    public static InfoHeader Instance;
    public float Time;
    Label levelLabel;
    Label timeLabel;
    public override void _Ready() {
        Instance = this;
        timeLabel = GetNode<Label>("Time");
        levelLabel = GetNode<Label>("Level");
    }

    public void SetLevelName(string name) {
        levelLabel.Text = name;
    }

    public override void _PhysicsProcess(float delta) {
        Time += delta;
        float time = Time;
        int minutes = (int) (time / 60);
        time -= 60 * minutes;
        int seconds = (int) (time);
        time -= seconds;
        int dec = (int) (time * 100);
        timeLabel.Text = $"{minutes}:{seconds:00}.{dec:00}";
    }
}
