using System;
using Godot;

public class AutoAnim : KinematicBody2D {
    [Export] NodePath Path;
    [Export] string Animation;
    public override void _Ready() {
        GetNode<AnimationPlayer>(Path).Play(Animation);
    }
}
