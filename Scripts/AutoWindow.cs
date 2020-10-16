using System;
using Godot;

public class AutoWindow : Control {
    public override void _Ready() {
        GlobalWindow.Instance.Connect(nameof(GlobalWindow.ContainerResized), this, nameof(ContainerResized));
        ContainerResized(GlobalWindow.Instance.Rectangle);
    }

    public void ContainerResized(Rect2 rectangle) {
        RectPosition = rectangle.Position;
        RectSize = rectangle.Size;
    }
}