using System;
using Godot;

public class AutoCanvasWindow : CanvasLayer {
    public override void _Ready() {
        GlobalWindow.Instance.Connect(nameof(GlobalWindow.ContainerResized), this, nameof(ContainerResized));
        ContainerResized(GlobalWindow.Instance.Rectangle);
    }

    public void ContainerResized(Rect2 rectangle) {
        Offset = GlobalWindow.Instance.Offset;
        int scale = Math.Max(1, GlobalWindow.Instance.Scale);
        Scale = new Vector2(scale, scale);
    }
}