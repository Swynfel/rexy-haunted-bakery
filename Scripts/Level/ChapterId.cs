public enum ChapterId {
    Tutorial,
    NONE,
    TOTAL = NONE,
}

public static class ChapterIdExtension {
    public static ChapterId Next(this ChapterId chapter) {
        return chapter < ChapterId.TOTAL ? chapter + 1 : ChapterId.TOTAL;
    }
}