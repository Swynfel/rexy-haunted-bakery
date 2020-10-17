using System;
using Godot;

public class RankLine : HBoxContainer {
    public void SetLine(int rank, string name, int time) {
        GetNode<Label>("Rank").Text = rank.ToString();
        GetNode<Label>("Name").Text = name;
        GetNode<Label>("Time").Text = InfoHeader.TimeString(time);
    }
}
