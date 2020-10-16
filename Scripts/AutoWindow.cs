using System;
using Godot;

public class AutoWindow : Control {
    [Export] bool centerPosition = true;
    public override void _Ready() {
        GlobalWindow.Instance.Connect(nameof(GlobalWindow.ContainerResized), this, nameof(ContainerResized));
        ContainerResized(GlobalWindow.Instance.Rectangle);
    }

    public void ContainerResized(Rect2 rectangle) {
        RectPosition = centerPosition ? rectangle.Position : Vector2.Zero;
        RectSize = rectangle.Size;
    }
}