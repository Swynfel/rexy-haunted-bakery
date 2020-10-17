using System;
using System.Text.RegularExpressions;
using Godot;

public class Username : LineEdit {
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready() {
        Connect("text_entered", this, nameof(TextChanged));
        Connect("text_changed", this, nameof(TextChanged));
        Text = Global.PlayerName;
    }

    private static readonly string REGEX = @"[^a-zA-Z0-9_\-\ \']";
    public void TextChanged(string newText) {
        if (Regex.IsMatch(newText, REGEX)) {
            for (int i = 0 ; i < newText.Length ; i++) {
                if (Regex.IsMatch(newText[i].ToString(), REGEX)) {
                    DeleteText(i, i + 1);
                    newText = newText.Remove(i, 1);
                    i--;
                }
            }
            Update();
        } else {
            Global.PlayerName = newText;
        }
    }
}
