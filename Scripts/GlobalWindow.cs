using System;
using Godot;

public class GlobalWindow : Container {
    public static GlobalWindow Instance;
    [Signal] public delegate void ScaleChanged(int scale);
    [Signal] public delegate void ContainerResized(Rect2 rectangle);
    [Export] public float WIDTH = 16 * 16;
    [Export] public float HEIGHT = 12 * 16;
    private int oldScale = -1;
    public override void _Ready() {
        Instance = this;
    }
    public override void _Notification(int notification) {
        if (notification == NotificationSortChildren || notification == NotificationDraw) {
            ScreenResized();
        }
    }

    // public override void _Process(float delta) {
    //     base._Process(delta);
    //     ScreenResized();
    // }

    public int Scale;
    public Vector2 Offset;

    public Rect2 Rectangle;

    public void ScreenResized() {
        Vector2 windowSize = OS.WindowSize;

        Vector2 size = new Vector2(WIDTH, HEIGHT);

        int xScale = (int) Math.Floor(windowSize.x / size.x);
        int yScale = (int) Math.Floor(windowSize.y / size.y);
        Scale = Math.Max(1, Math.Min(xScale, yScale));

        Offset = 0.5f * (windowSize - Scale * size);

        Rectangle = new Rect2(Vector2.Zero, size);

        foreach (Node _child in GetChildren()) {
            if (_child is Control child) {
                FitChildInRect(child, Rectangle);
                // child.RectScale = new Vector2(1f, 1f);
            }
        }

        EmitSignal(nameof(ContainerResized), Rectangle);

        if (oldScale != Scale) {
            oldScale = Scale;
            EmitSignal(nameof(ScaleChanged), Scale);
        }
    }
}