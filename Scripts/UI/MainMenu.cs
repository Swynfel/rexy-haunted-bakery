using System;
using Godot;

public class MainMenu : AutoCanvasWindow {
    public static MainMenu Instance;
    public override void _Ready() {
        base._Ready();
        Instance = this;
        Show();
    }
    public void Show() {
        GUI.Instance.Off();
        GetTree().Paused = false;
    }
    public void Hide() {
    }
}
