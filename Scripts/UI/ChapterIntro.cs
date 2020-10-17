using System;
using System.Threading.Tasks;
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
        Global.Time = 0;
        GUI.Instance.HideMenuInstantly();
        // Requests leaderboard in advance
        Task task = Scores.Instance.GetChapterScore(Global.Chapter);
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
