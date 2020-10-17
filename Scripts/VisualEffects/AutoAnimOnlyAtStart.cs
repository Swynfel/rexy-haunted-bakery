using System;
using Godot;

public class AutoAnimOnlyAtStart : Node {
    private static bool start = true;
    [Export] NodePath Path;
    [Export] string Animation;
    public override void _Ready() {
        if (start) {
            start = false;
            GetNode<AnimationPlayer>(Path).Play(Animation);
        }
    }
}
