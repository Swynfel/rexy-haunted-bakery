using System;
using System.Collections.Generic;
using Godot;

public class SoundHandler : AudioStreamPlayer {
    public static SoundHandler Instance;
    [Export(PropertyHint.File)] string[] musicNames;
    // [Export(PropertyHint.File)] string[] sfxNames;
    Dictionary<string, AudioStream> musics = new Dictionary<string, AudioStream>();
    // Dictionary<string, AudioStream> sfxs;
    public override void _Ready() {
        Instance = this;
        foreach (string name in musicNames) {
            musics[name] = ResourceLoader.Load<AudioStream>($"res://Assets/Sounds/Music/{name}.ogg");
        }
        // foreach (string name in sfxNames) {
        //     sfxs[name] = ResourceLoader.Load<AudioStream>($"res://Assets/Sounds/SFX/{name}.wav");
        // }
        Stream = musics["raining_croissants"];
        Play();
    }
}
