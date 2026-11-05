using Godot;
using System;

public partial class Ship : Node3D, InteractiveObject {
    const float SPEED = 2.5f;

    [Export]
    public ResourceData.ShipType shipType;

    [Export]
    public string shipName;

    [Export]
    Material outlineMaterial;

    public bool selected;

    private MeshInstance3D mesh;
    private Player playerHandle;
    private ShipMenu shipMenu;
    private Node3D root;


    public InteractiveObject.Info info = new InteractiveObject.Info();

    public Ship() {
        // Constructor
    }

    public Vector3 GetObjectPosition() {
        return Position;
    }

    public InteractiveObject.Info GetInfo() {
        return info;
    }


    public override void _Ready() {
        base._Ready();
        playerHandle = GetNode<Player>("../Player");
        mesh = GetNode<MeshInstance3D>("Mesh");
        shipMenu = GetNode<ShipMenu>("ShipMenu");
        shipMenu.PopulateInfo(shipName, shipType, this);
        selected = false;
    }

    public override void _Process(double delta) {
        base._Process(delta);
        // TODO: figure out how to make this bih move
    }

    public override void _PhysicsProcess(double delta) {
        base._PhysicsProcess(delta);
        // TODO: Ditto
    }
    public void Interact() {
        selected = !selected;
    }

    public void OnStaticBodyMouseEntered() {
        mesh.MaterialOverlay = outlineMaterial;
        playerHandle.objectHandle = this;
        shipMenu.Visible = true;
    }

    public void OnStaticBodyMouseExited() {
        if (!selected) { 
            mesh.MaterialOverlay = null;
            playerHandle.objectHandle = null;
            shipMenu.Visible = false;
        }
    }
}
