using System;
using Godot;

public class Global : Node {
    private static readonly string CHAPTER_INTRO = "res://Scenes/ChapterIntro.tscn";
    private static readonly string MAIN_MENU = "res://Scenes/MainMenu.tscn";
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

    public static void LoadChapter(ChapterId chapter) {
        Chapter = chapter;
        Level = 1;
        Instance.GetTree().ChangeScene(CHAPTER_INTRO);
    }
    public static void LoadMenu() {
        Instance.GetTree().ChangeScene(MAIN_MENU);
    }
    public static Global Instance;

    public override void _Ready() {
        Instance = this;
    }
    public override void _PhysicsProcess(float delta) {
        if (!GetTree().Paused) {
            Time += delta;
        }
    }
}