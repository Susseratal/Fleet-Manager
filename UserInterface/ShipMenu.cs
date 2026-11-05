using Godot;
using System;

public partial class ShipMenu : Control {
    Ship parentHandle;
    Label shipName;
    Label shipType;

    public override void _Input(InputEvent @event) {
        base._Input(@event);
        if (@event is InputEventMouseMotion eventMouseMotion && !parentHandle.selected) {
            Position = eventMouseMotion.Position;
        }
    }

    public override void _Ready() {
        base._Ready();
        shipName = GetNode<Label>("Border/VerticalBox/ShipName");
        shipType = GetNode<Label>("Border/VerticalBox/ShipType");
    }

    public void PopulateInfo(string _ShipName, ResourceData.ShipType _ShipType, Ship _ParentHandle) { 
        shipName.Text = _ShipName;
        shipType.Text = _ShipType.ToString();
        parentHandle = _ParentHandle;
    }
}
