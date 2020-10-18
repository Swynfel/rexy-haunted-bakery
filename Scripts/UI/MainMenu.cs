using System;
using Godot;

public class MainMenu : AutoCanvasWindow {
    [Export] Palette basePalette;
    public static MainMenu Instance;
    public override void _Ready() {
        base._Ready();
        Instance = this;
        Show();
        GlobalTheme.Instance.ChangePalette(basePalette, 0.5f);
    }
    public void Show() {
        GUI.Instance.Off();
        GetTree().Paused = false;
    }
    public void Hide() {
    }
}
