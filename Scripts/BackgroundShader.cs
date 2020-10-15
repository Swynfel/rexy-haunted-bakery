using System;
using Godot;

public class BackgroundShader : ColorRect {
    ShaderMaterial shader;
    [Export] NodePath GlobalScalePath;
    public override void _Ready() {
        shader = (ShaderMaterial) Material;
        GetNode(GlobalScalePath).Connect(nameof(GlobalWindow.ScaleChanged), this, nameof(ScaleChanged));
    }

    public void ScaleChanged(int scale) {
        shader.SetShaderParam("scale", scale);
    }
}
