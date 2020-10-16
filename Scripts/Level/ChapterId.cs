public enum ChapterId {
    NONE,
    Tutorial,
    TOTAL,
}

public static class ChapterIdExtension {
    public static ChapterId Next(this ChapterId chapter) {
        return chapter < ChapterId.TOTAL ? chapter + 1 : ChapterId.NONE;
    }
}