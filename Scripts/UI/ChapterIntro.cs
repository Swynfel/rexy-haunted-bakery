using System;
using Godot;

public class ChapterIntro : AutoCanvasWindow {
    [Export] NodePath animationPath;
    [Export] NodePath chapterLabelPath;
    public override void _Ready() {
        GetNode<AnimationPlayer>(animationPath).Play("hide");
        GetNode<Label>(chapterLabelPath).Text = Global.LevelFullName();
        CallDeferred(nameof(LoadLevel));
        base._Ready();
        GetTree().Paused = true;
        GUI.Instance.HideMenuInstantly();
        Global.Time = 0;
    }

    Node level;
    public void LoadLevel() {
        level = ResourceLoader.Load<PackedScene>(Global.CurrentScene()).Instance();
        GetTree().Root.AddChild(level);
    }
    public void Done() {
        GetTree().Paused = false;
        GetTree().CurrentScene = level;
        QueueFree();
    }
}
