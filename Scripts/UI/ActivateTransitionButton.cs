using System;
using Godot;
using Utils;

public class ActivateTransitionButton : GrabFocusButton {

    [Export] NodePath animator;
    [Export] string animation;
    [Export] bool isReversed;
    protected override void ButtonPressed() {
        GetNode<AnimationPlayer>(animator).Play(animation, customSpeed: isReversed ? -1f : 1f, fromEnd: isReversed);
    }
}