using System;
using Godot;

public class GlobalTheme : Node {
    public static GlobalTheme Instance;

    [Export] string wallPath;
    // [Export] string wallPalettePath;
    [Export] string backgroundPath;
    ShaderMaterial wallShader;
    // ShaderMaterial wallPaletteShader;
    ShaderMaterial backgroundShader;
    Tween tween;
    [Export] private Gradient gradient;
    [Export] private StyleBoxFlat[] flatBoxes;
    [Export] private float[] flatBoxesBgColor;
    [Export] private float[] flatBoxesBorderColor;
    [Export] private StyleBoxLine[] lines;
    [Export] private float[] lineColor;
    [Export] private ShaderMaterial rexy;

    public override void _Ready() {
        Instance = this;
        wallShader = (ShaderMaterial) GlobalWindow.Instance.GetNode<CanvasItem>(wallPath).Material;
        // wallPaletteShader = (ShaderMaterial) GlobalWindow.Instance.GetNode<CanvasItem>(wallPalettePath).Material;
        backgroundShader = (ShaderMaterial) GlobalWindow.Instance.GetNode<CanvasItem>(backgroundPath).Material;
    }
    public void SetBg(Color bg) {
        gradient.SetColor(0, bg);
        UpdateColors();
    }
    public void SetBorder(Color border) {
        gradient.SetColor(1, border);
        UpdateColors();
    }

    public void UpdateColors() {
        for (int i = 0 ; i < flatBoxes.Length ; i++) {
            flatBoxes[i].BgColor = gradient.Interpolate(flatBoxesBgColor[i]);
            flatBoxes[i].BorderColor = gradient.Interpolate(flatBoxesBorderColor[i]);
        }
        for (int i = 0 ; i < lines.Length ; i++) {
            lines[i].Color = gradient.Interpolate(lineColor[i]);
        }
    }

    public void ChangePalette(Palette palette, float duration = 2f) {
        if (tween == null) {
            tween = new Tween();
            AddChild(tween);
        } else {
            tween.StopAll();
        }
        // Inner wall
        Color startInnerWallColor = (Color) wallShader.GetShaderParam("main_color");
        tween.InterpolateProperty(wallShader, "shader_param/main_color", startInnerWallColor, palette.wallInner, duration);
        // tween.InterpolateProperty(wallPaletteShader, "shader_param/main_color", startInnerWallColor, palette.wallInner, duration);
        tween.InterpolateProperty(InfoHeader.Background, "color", startInnerWallColor, palette.wallInner, duration);
        tween.InterpolateMethod(this, nameof(SetBg), startInnerWallColor, palette.wallInner, duration);
        // Outer wall
        Color startOuterWallColor = (Color) wallShader.GetShaderParam("outline_color");
        tween.InterpolateProperty(wallShader, "shader_param/outline_color", startOuterWallColor, palette.wallOuter, duration);
        // tween.InterpolateProperty(wallPaletteShader, "shader_param/outline_color", startOuterWallColor, palette.wallOuter, duration);
        tween.InterpolateMethod(this, nameof(SetBorder), startOuterWallColor, palette.wallOuter, duration);
        // Background
        tween.InterpolateProperty(backgroundShader, "shader_param/main_color", backgroundShader.GetShaderParam("main_color"), palette.backgroundInner, duration);
        tween.InterpolateProperty(backgroundShader, "shader_param/outline_color", backgroundShader.GetShaderParam("outline_color"), palette.backgroundOuter, duration);
        // Rexy
        tween.InterpolateProperty(rexy, "shader_param/new_color", rexy.GetShaderParam("new_color"), palette.rexy, duration);
        tween.Start();
    }
}