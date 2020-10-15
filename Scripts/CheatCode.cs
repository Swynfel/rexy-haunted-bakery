using System;
using System.Collections.Generic;
using Godot;

public class CheatCode : Position2D {
    private enum Key {
        LEFT,
        RIGHT,
        JUMP,
        USE,
    }


    private List<Key> currentSequence = new List<Key>();
    private float lastChange = 0f;
    private const float TIMEOUT = 0.5f;

    // Mwuhahahaha, well done, you have found the cheat sequence!
    // (Not that is was well hidden...)
    // Enjoy your new found power!
    private readonly List<Key> CHEAT_SEQUENCE = new List<Key> {
        Key.LEFT, Key.LEFT, Key.RIGHT, Key.JUMP, Key.RIGHT, Key.RIGHT, Key.LEFT, Key.USE
    };
    public override void _Process(float delta) {
        base._Process(delta);
        int lastLength = currentSequence.Count;
        if (Input.IsActionJustPressed("ui_left")) {
            currentSequence.Add(Key.LEFT);
        }
        if (Input.IsActionJustPressed("ui_right")) {
            currentSequence.Add(Key.RIGHT);
        }
        if (Input.IsActionJustPressed("jump")) {
            currentSequence.Add(Key.JUMP);
        }
        if (Input.IsActionJustPressed("use")) {
            currentSequence.Add(Key.USE);
        }
        if (lastLength != currentSequence.Count) {
            lastChange = 0f;
            int extra = currentSequence.Count - CHEAT_SEQUENCE.Count;
            if (extra >= 0) {
                if (extra != 0) {
                    currentSequence.RemoveRange(0, extra);
                }
                for (int index = 0 ; index < CHEAT_SEQUENCE.Count ; index++) {
                    if (currentSequence[index] != CHEAT_SEQUENCE[index]) {
                        return;
                    }
                }
                RemoveAllTheBread();
            }
        } else if (lastChange >= TIMEOUT) {
            lastChange = TIMEOUT;
            if (lastLength != 0) {
                currentSequence.Clear();
            }
        } else {
            lastChange += delta;
        }
    }

    // In case you're wondering what the cheat does, here is a function with an explicit name:
    private void RemoveAllTheBread() {
        GD.Print("CHEAT CODE ACTIVATED, REMOVING ALL THE BREAD!");
        foreach (Node bread in GetTree().GetNodesInGroup("Bread")) {
            bread.QueueFree();
        }
    }
}
