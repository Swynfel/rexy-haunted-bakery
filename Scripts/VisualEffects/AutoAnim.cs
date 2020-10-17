using System;
using Godot;

public class AutoAnim : Node {
    [Export] NodePath Path;
    [Export] string Animation;
    public override void _Ready() {
        GetNode<AnimationPlayer>(Path).Play(Animation);
    }
}
