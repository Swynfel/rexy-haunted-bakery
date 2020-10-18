using System;
using Godot;
using Utils;

public class ChapterButton : GrabFocusButton {
    [Export] ChapterId chapter;
    [Export] Palette palette;
    [Export] bool useWallColor;

    public override void _Ready() {
        base._Ready();
        GetNode<Label>("Chapter").Text = $"Chapter {(int) chapter}";
        GetNode<Label>("Name").Text = chapter.ToString();
        if (Global.Chapter == chapter) {
            GrabFocus();
        }
        StyleBoxFlat normal = (StyleBoxFlat) Get("custom_styles/normal");
        StyleBoxFlat focused = (StyleBoxFlat) Get("custom_styles/focus");
        Color bg = useWallColor ? palette.wallInner : palette.backgroundInner;
        Color border = useWallColor ? palette.wallOuter : palette.backgroundOuter;
        Color textA = useWallColor ? palette.backgroundInner : palette.wallInner;
        Color textB = useWallColor ? palette.backgroundOuter : palette.wallOuter;
        normal.BgColor = bg;
        normal.BorderColor = border;
        focused.BgColor = bg;
        focused.BorderColor = border;
        GetNode("Name").Set("custom_colors/font_color", textA);
        GetNode("Chapter").Set("custom_colors/font_color", textB);
    }

    protected override void ButtonPressed() {
        Global.LoadChapter(chapter);
    }
}