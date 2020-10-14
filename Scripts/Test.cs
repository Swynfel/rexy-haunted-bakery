using System;
using Godot;

public class Test : Control {
    public override void _Ready() {
        TestAsync();
    }

    public async void TestAsync() {
        GD.Print("A ", Math.Abs(-1));
        GD.Print("B ", Math.Abs(2));
        GD.Print("C ", Math.Abs(-3));
    }
}
