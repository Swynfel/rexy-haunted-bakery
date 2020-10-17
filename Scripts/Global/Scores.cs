using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using Godot;

public class Scores : Node {
    public struct ScoreLine {
        public string Name;
        public int Time;
    }
    public class ChapterScore {
        public ChapterId Chapter;
        public DateTime TimeStamp;
        public List<ScoreLine> Scores = new List<ScoreLine>();
    }
    public static Scores Instance;

    public Dictionary<ChapterId, ChapterScore> cachedScores = new Dictionary<ChapterId, ChapterScore>();

    public Node SilentWolf { get; private set; }
    public Godot.Object SWS { get; private set; }
    public override void _Ready() {
        Instance = this;
        SilentWolf = GetNode("/root/SilentWolf");
        SWS = (Godot.Object) SilentWolf.Get("Scores");
        SilentWolf.Call("configure", Utils.Json.DictFromFile("silent_wolf_config.json"));
    }

    [Signal] public delegate void RefreshChapterScore(ChapterId chapter);

    public ChapterScore GetCachedChapterScore(ChapterId chapter) {
        return cachedScores.ContainsKey(chapter) ? cachedScores[chapter] : new ChapterScore { Chapter = chapter };
    }
    public async Task<ChapterScore> GetChapterScore(ChapterId chapter, float cacheTime = 15f) {
        if (!cachedScores.ContainsKey(chapter) || (cachedScores[chapter].TimeStamp - DateTime.Now).Seconds > cacheTime) {
            await FetchChapter(chapter);
        }
        return cachedScores[chapter];
    }

    public async Task AddScoreLine(ChapterId chapter, string player, int time) {
        await ToSignal((Godot.Object) SWS.Call("persist_score", player, -time, $"chapter-{(int) chapter}"), "sw_score_posted");
    }

    private int requestKey = 0;
    private async Task FetchChapter(ChapterId chapter) {
        requestKey++;
        int thisRequest = requestKey;
        await ToSignal((Godot.Object) SWS.Call("get_high_scores", 10, $"chapter-{(int) chapter}"), "sw_scores_received");
        if (thisRequest != requestKey) {
            GD.PrintErr("Fetched Multiple Chapters at the same time");
        }
        ChapterScore chapterScore = new ChapterScore { Chapter = chapter, TimeStamp = DateTime.Now };
        foreach (Godot.Collections.Dictionary line in (Godot.Collections.Array) SWS.Get("scores")) {
            // GD.Print(line["player_name"].GetType(), " ", line["player_name"]);
            // GD.Print(line["score"].GetType(), " ", line["score"]);
            chapterScore.Scores.Add(new ScoreLine { Name = (string) line["player_name"], Time = (int) -((float) line["score"]) });
        }
        cachedScores[chapter] = chapterScore;
        EmitSignal(nameof(RefreshChapterScore), chapter);
    }
}
