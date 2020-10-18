using System;
using Godot;

public class PixelCamera : Node2D {
    public static PixelCamera Instance;
    public Vector2 Center;
    public Node2D Rexy;
    public float MaxX;
    public float MaxY;
    public override void _Ready() {
        Instance = this;
    }

    public override void _Process(float delta) {
        base._Process(delta);
        Vector2 OffsetToRexy = Rexy != null && IsInstanceValid(Rexy) ? (Position - Rexy.Position) : Vector2.Zero;
        Center = new Vector2(Math.Max(-MaxX, Math.Min(OffsetToRexy.x, 0)), Math.Max(0f, Math.Min(OffsetToRexy.y, MaxY)));
        UpdateViewport();
    }

    public void UpdateViewport() {
        Viewport viewport = GetViewport();
        Transform2D transform = viewport.CanvasTransform;
        transform.Scale = new Vector2(GlobalWindow.Instance.Scale, GlobalWindow.Instance.Scale);
        transform.origin = GlobalWindow.Instance.Offset + GlobalWindow.Instance.Scale * Center;
        viewport.CanvasTransform = transform;
    }
}
