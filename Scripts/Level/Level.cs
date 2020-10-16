using System;
using Godot;

public class Level : Node2D {
    [Export(PropertyHint.File)] string nextScene;
    [Export] bool isTimed;

    public void FinishLevel() {
        GetTree().ChangeScene(nextScene);
    }
}
