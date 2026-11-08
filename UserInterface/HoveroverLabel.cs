using Godot;
using System;

public partial class HoveroverLabel : Control {
    public Label nameTag;

    public override void _Ready() {
        base._Ready();
        nameTag = GetNode<Label>("Label");
    }

    public override void _Input(InputEvent @event) {
        base._Input(@event);
        if (@event is InputEventMouseMotion eventMouseMotion) {
            Position = eventMouseMotion.Position;
        }
    }

    public void SetTagText(string _text) {
        nameTag.Text = _text;
    }
}
