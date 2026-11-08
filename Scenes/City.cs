using Godot;
using System;

public partial class City : Node3D, InteractiveObject {
    [Export]
    public string cityName;

    [Export]
    Material outlineMaterial;

    public InteractiveObject.Info info;
    public bool selected;

    private MeshInstance3D mesh;
    private HoveroverLabel nametag;
    private Player playerHandle;

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
        nametag.SetTagText(cityName);
    }

    public void Interact() { 
        // asdf
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
