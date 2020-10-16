using System;
using Godot;

public class Global : Node {
    public static ChapterId Chapter { get; set; } = ChapterId.NONE;
    public static int Level { get; set; } = 99;
    public static float Time { get; set; } = 0;

    public static string Scene(ChapterId chapter, int level) {
        if (chapter >= ChapterId.TOTAL) {
            return $"res://Scenes/Levels/Level0-0.tscn"; // TODO: Menu
        }
        return $"res://Scenes/Levels/Level{(int) (chapter)}-{level}.tscn"; // TODO: First Chapter
    }

    public override void _PhysicsProcess(float delta) {
        if (!GetTree().Paused) {
            Time += delta;
        }
    }
}
