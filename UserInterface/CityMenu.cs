using Godot;
using System;

public partial class CityMenu : Control {
    private City parentHandle;
    private Label cityName;
    private Label prodLabel;
    private Label reqLabel;

    public ResourceData Produces;
    public ResourceData Requires;

    public override void _Input(InputEvent @event) {
        base._Input(@event);
    }

    public override void _Ready() {
        base._Ready();
        cityName = GetNode<Label>("Border/VBoxContainer/VerticalBox/CityName");
        prodLabel = GetNode<Label>("Border/VBoxContainer/VerticalBox/CityProduces");
        reqLabel = GetNode<Label>("Border/VBoxContainer/VerticalBox/CityRequires");
    }

    public void PopulateInfo(string _CityName, ResourceData _produces, ResourceData _requires, City _parentHandle) { 
        parentHandle = _parentHandle;
        cityName.Text = _CityName;
        Produces = _produces;
        Requires = _requires;

        prodLabel.Text = new string ("Producing: " + Produces.name);
        reqLabel.Text = new string ("Requires: " + Requires.name);
    }

    public void ButtonClicked() {
        parentHandle.cityMenu.Visible = false;
        parentHandle.playerHandle.menuOpen = false;
    }
}
