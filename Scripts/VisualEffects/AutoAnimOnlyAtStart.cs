using System;
using Godot;

public class AutoAnimOnlyAtStart : Node {
    private static bool start = true;
    [Export] NodePath Path;
    [Export] string Animation;
    [Export] string OtherAnimation;
    public override void _Ready() {
        if (start) {
            start = false;
            GetNode<AnimationPlayer>(Path).Play(Animation);
        } else {
            if (OtherAnimation != "") {
                GetNode<AnimationPlayer>(Path).Play(OtherAnimation);
            }
        }
    }
}
