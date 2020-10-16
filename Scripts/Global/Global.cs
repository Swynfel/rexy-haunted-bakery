using System;
using Godot;

public class Global : Node {
    public static readonly string CHAPTER_INTRO = "res://Scenes/ChapterIntro.tscn";
    public static ChapterId Chapter { get; set; } = ChapterId.NONE;
    public static int Level { get; set; } = 99;
    public static float Time { get; set; } = 0;

    public static string LevelFullName() {
        return $"{Chapter}: Level {(int) Chapter}-{Level}";
    }
    private static string Scene(ChapterId chapter, int level) {
        if (chapter >= ChapterId.TOTAL) {
            return $"res://Scenes/Levels/Level1-1.tscn"; // TODO: Menu
        }
        return $"res://Scenes/Levels/Level{(int) (chapter)}-{level}.tscn"; // TODO: First Chapter
    }
    public static string CurrentScene() {
        return Scene(Chapter, Level);
    }

    public override void _PhysicsProcess(float delta) {
        if (!GetTree().Paused) {
            Time += delta;
        }
    }
}
