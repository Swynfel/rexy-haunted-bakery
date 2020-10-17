using System;
using Godot;

public class GUI : AutoCanvasWindow {
    public static GUI Instance;
    [Export] NodePath animationPath;
    AnimationPlayer animationPlayer;
    public override void _Ready() {
        base._Ready();
        Instance = this;
        animationPlayer = GetNode<AnimationPlayer>(animationPath);
        // Off();
    }

    public void Off() {
        SetProcessInput(false);
    }
    public void On() {
        SetProcessInput(true);
    }

    public void ShowPauseMenu() {
        Off();
        GetTree().Paused = true;
        animationPlayer.Play("show-paused");
    }

    public void HideMenu() {
        animationPlayer.Play("hide");
    }
    public void HideMenuInstantly() {
        GetNode<Control>("Window/Container").Hide();
        GetNode<Control>("Window/Filter").Hide();
    }
    public void Resume() {
        On();
        GetTree().Paused = false;
    }

    public override void _Input(InputEvent @event) {
        if (@event.IsActionPressed("ui_cancel")) {
            ShowPauseMenu();
            GetTree().SetInputAsHandled();
        }
    }
}
