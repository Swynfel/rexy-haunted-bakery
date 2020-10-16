using System;
using Godot;

public class PixelCamera : Node2D {
    public static PixelCamera Instance;
    private int scale;
    public override void _Ready() {
        Instance = this;
    }

    public override void _Process(float delta) {
        base._Process(delta);
        UpdateViewport();
    }

    public void UpdateViewport() {
        Viewport viewport = GetViewport();
        Transform2D transform = viewport.CanvasTransform;
        transform.Scale = new Vector2(GlobalWindow.Instance.Scale, GlobalWindow.Instance.Scale);
        transform.origin = GlobalWindow.Instance.Offset;
        viewport.CanvasTransform = transform;
    }
}
