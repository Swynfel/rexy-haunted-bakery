using System;
using Godot;

public class InfoHeader : HBoxContainer {
    public static ChapterId Chapter { get; private set; } = ChapterId.NONE;
    public static int Level { get; private set; } = 99;
    public static InfoHeader Instance;
    public float Time;
    Label levelLabel;
    Label timeLabel;
    public override void _Ready() {
        Instance = this;
        timeLabel = GetNode<Label>("Time");
        levelLabel = GetNode<Label>("Level");
    }

    private void UpdateLabel() {
        levelLabel.Text = $"{Chapter} - {Level}";
    }

    private void UpdateTime() {
        float time = Time;
        int minutes = (int) (time / 60);
        time -= 60 * minutes;
        int seconds = (int) (time);
        time -= seconds;
        int dec = (int) (time * 100);
        timeLabel.Text = $"{minutes}:{seconds:00}.{dec:00}";
    }

    public override void _PhysicsProcess(float delta) {
        if (!GetTree().Paused) {
            Time += delta;
            UpdateTime();
        }
        UpdateLabel();
    }
}
