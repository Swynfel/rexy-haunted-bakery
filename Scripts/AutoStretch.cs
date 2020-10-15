using System;
using Godot;

public class AutoStretch : Node {
    public override void _Ready() {
        GetTree().Connect("screen_resized", this, nameof(ScreenResized));
        ScreenResized();
    }

    public void ScreenResized() {
        var windowSize = OS.WindowSize;
        Viewport viewport = GetViewport();

        int xScale = (int) Math.Floor(windowSize.x / viewport.Size.x);
        int yScale = (int) Math.Floor(windowSize.y / viewport.Size.y);
        int scale = Math.Max(1, Math.Min(xScale, yScale));

        // Vector2 diff = windowSize - scale * viewport.Size;
        // Vector2 pixelDiff = diff / scale;
        // if ((int) (pixelDiff.x) % 2 == 1) {
        //     pixelDiff.x++;
        // }
        // if ((int) (pixelDiff.y) % 2 == 1) {
        //     pixelDiff.y++;
        // }

        // viewport.Size = viewport.Size + pixelDiff;

        Vector2 diff = windowSize - scale * viewport.Size;
        Vector2 halfDiff = diff / 2;

        viewport.SetAttachToScreenRect(new Rect2(halfDiff, scale * viewport.Size));

    }
}