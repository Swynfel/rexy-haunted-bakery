using System;
using Godot;
using Utils;

public class ChapterButton : GrabFocusButton {
    [Export] ChapterId chapter;

    public override void _Ready() {
        base._Ready();
        GetNode<Label>("Chapter").Text = $"Chapter {(int) chapter}";
        GetNode<Label>("Name").Text = chapter.ToString();
        if (Global.Chapter == chapter) {
            GrabFocus();
        }
    }

    protected override void ButtonPressed() {
        Global.LoadChapter(chapter);
    }
}