using System;
using Godot;

public class ScaleToViewport : CanvasLayer {
    public override void _Ready() {
        GlobalWindow.Instance.Connect(nameof(GlobalWindow.ScaleChanged), this, nameof(ScaleChanged));
    }

    public void ScaleChanged(int scale) {
        Scale = new Vector2(scale, scale);
    }
}
