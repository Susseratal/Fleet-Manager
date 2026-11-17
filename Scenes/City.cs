using Godot;
using System;

public partial class City : Node3D, InteractiveObject {
    [Export]
    public string cityName;

    [Export]
    Material outlineMaterial;

    // [Export]
    public ResourceData produces;

    // [Export]
    public ResourceData requires;

    public InteractiveObject.Info info;
    public bool selected;
    public CityMenu cityMenu;

    private MeshInstance3D mesh;
    private HoveroverLabel nametag;
    public Player playerHandle;

    public Vector3 GetObjectPosition() {
        return Position;
    }

    public InteractiveObject.Info GetInfo() {
        return info;
    }

    public override void _Ready() {
        base._Ready();

        nametag = GetNode<HoveroverLabel>("HoveroverLabel");
        playerHandle = GetNode<Player>("../../Player");
        mesh = GetNode<MeshInstance3D>("Mesh");
        cityMenu = GetNode<CityMenu>("CityMenu");
        nametag.SetTagText(cityName);
    }

    public void Interact(Player _playerHandle) {
        cityMenu.Visible = true;
        _playerHandle.menuOpen = true;
    }

    public void OnStaticBodyMouseEntered() {
        mesh.MaterialOverlay = outlineMaterial;
        playerHandle.objectHandle = this;
        nametag.Visible = true;
        // shipMenu.Visible = true;
    }

    public void OnStaticBodyMouseExited() {
        if (!selected) { 
            mesh.MaterialOverlay = null;
            playerHandle.objectHandle = null;
            nametag.Visible = false;
            // shipMenu.Visible = false;
        }
    }
}
