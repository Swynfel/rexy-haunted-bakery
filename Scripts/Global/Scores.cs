using System;
using System.Collections.Generic;
using Godot;

public class Scores : Node {
    public Node SilentWolf { get; private set; }
    public Godot.Object SWS { get; private set; }
    public override void _Ready() {
        SilentWolf = GetNode("/root/SilentWolf");
        SWS = (Godot.Object) SilentWolf.Get("Scores");
        SilentWolf.Call("configure", Utils.Json.DictFromFile("silent_wolf_config.json"));
    }

    public async void FetchChapter(ChapterId chapter) {
        GD.Print("Scores Asked");
        await ToSignal((Godot.Object) SWS.Call("get_high_scores", 10), "sw_scores_received");
        GD.Print("Scores Received");
        GD.Print(SWS.Get("scores"));
    }
}
