using Godot;
using System;

public partial class Ship : CharacterBody3D, InteractiveObject {
    const float SPEED = 5.0f;

    [Export]
    public ShipData.ShipType shipType;

    [Export]
    Material outlineMaterial;

    public bool selected;

    private MeshInstance3D mesh;
    private Player playerHandle;
    // private ShipMenu shipMenu;
    private HoveroverLabel nametag;
    private ShipData shipData = new ShipData();
    private Node3D root;
    private Vector3 targetLocation;
    private bool hasTargetLocation;


    public InteractiveObject.Info info;

    public Ship() {
        // Constructor
    }

    public Vector3 GetObjectPosition() {
        return Position;
    }

    public InteractiveObject.Info GetInfo() {
        return info;
    }

    public void SetTargetLocation(Vector3 _targetLocation) { 
        targetLocation = _targetLocation;
        hasTargetLocation = true;
    }


    public override void _Ready() {
        base._Ready();
        shipData.ShipName = Global.GetRandomShipName();

        playerHandle = GetNode<Player>("../Player");
        mesh = GetNode<MeshInstance3D>("Mesh");
        nametag = GetNode<HoveroverLabel>("HoveroverLabel");
        nametag.SetTagText(shipData.ShipName);

        // shipMenu = GetNode<ShipMenu>("ShipMenu");
        // shipMenu.PopulateInfo(shipData.ShipName, shipType, this);

        selected = false;
        targetLocation = new Vector3(5, 0, 0);
        hasTargetLocation = false;
    }

    public override void _Process(double delta) {
        base._Process(delta);
    }

    public override void _PhysicsProcess(double delta) {
        base._PhysicsProcess(delta);
        if (hasTargetLocation && !Global.GamePaused) { 
            // TODO: create target location and move towards it 
            Velocity = new Vector3(0, 0, 0);
            MoveAndSlide();
        }
    }
    public void Interact() {
        selected = !selected;
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
