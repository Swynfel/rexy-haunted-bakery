public enum ChapterId {
    Tutorial,
    NONE,
    TOTAL = NONE,
}

public static class ChapterIdExtension {
    public static string Scene(this ChapterId chapter) {
        if (chapter >= ChapterId.TOTAL) {
            return $"res://Scenes/Levels/Level0-1.tscn"; // TODO: Menu
        }
        return $"res://Scenes/Levels/Level{(int) (chapter)}-1.tscn"; // TODO: First Chapter
    }

    public static ChapterId Next(this ChapterId chapter) {
        return chapter < ChapterId.TOTAL ? chapter + 1 : ChapterId.TOTAL;
    }
}