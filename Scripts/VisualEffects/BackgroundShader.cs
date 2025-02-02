using System;
using Godot;

public class BackgroundShader : ColorRect {
    ShaderMaterial shader;
    public override void _Ready() {
        shader = (ShaderMaterial) Material;
        GlobalWindow.Instance.Connect(nameof(GlobalWindow.ScaleChanged), this, nameof(ScaleChanged));
    }

    public void ScaleChanged(int scale) {
        shader.SetShaderParam("scale", scale);
    }
}
