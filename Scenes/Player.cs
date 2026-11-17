using Godot;
using System;
using System.Collections.Generic;

public partial class Player : Node3D {
    [Export]
    Vector2 minCamPosition;

    [Export]
    Vector2 maxCamPosition;

    public bool paused = false;

    Camera3D cam;
    float movementSpeed = 1;
    int zoomLevel = 0;
    Vector2 viewportSize, viewportCamPosition;
    bool mapScreenActive = false;

    public InteractiveObject objectHandle;
    public bool menuOpen = false;

    private Vector3 stashPosition;
    private Vector3 defaultRotation;

    private float piecesOfEight;
    private List<ShipData> fleet;
    private List<ResourceData> inventory;

    public void TogglePause() {
        return;
    }

    private void EvalCamFov(int zl) {
        switch (zl) {
            case 0:
                cam.Fov = 90.0f;
                movementSpeed = 0.8f;
                break;
            case 1:
                cam.Fov = 80.0f;
                movementSpeed = 0.6f;
                break;
            case 2:
                cam.Fov = 60.0f;
                movementSpeed = 0.4f;
                break;
            case 3:
                cam.Fov = 50.0f;
                movementSpeed = 0.3f;
                break;
            case 4:
                cam.Fov = 40.0f;
                movementSpeed = 0.2f;
                break;
            default:
                break;
        }

    }

    public Player() { }

    public override void _Ready() {
        base._Ready();
        cam = GetNode<Camera3D>("Camera3D");

        viewportSize.X = GetViewport().GetVisibleRect().Size.X;
        viewportSize.Y = GetViewport().GetVisibleRect().Size.Y;
        viewportCamPosition.X = viewportSize.X / 2;
        viewportCamPosition.Y = viewportSize.Y / 2;

        defaultRotation = Rotation;
        ShipData startShip = new ShipData();
        startShip.ShipName = Global.GetRandomShipName();
        startShip.shipType = ShipData.ShipType.Schooner;
        fleet.Add(startShip);

        piecesOfEight = 5.0f;
    }

    public override void _Input(InputEvent @event) {
        base._Input(@event);
    }

    public override void _PhysicsProcess(double delta) {
        if (Input.IsActionJustPressed("left_click")) {
            if (objectHandle != null) { objectHandle.Interact(this); }
        }

        if (Input.IsActionJustPressed("right_click")) {
            GD.Print("Right Click");
        }

        if (Input.IsActionJustPressed("zoom_out")) {
            if (menuOpen) { return; }
            zoomLevel = Math.Clamp(zoomLevel - 1, 0, 3);
            EvalCamFov(zoomLevel);
        }

        if (Input.IsActionJustPressed("zoom_in")) {
            if (menuOpen) { return; }
            zoomLevel = Math.Clamp(zoomLevel + 1, 0, 3);
            EvalCamFov(zoomLevel);
        }

        if (Input.IsActionJustPressed("game_speed_up")) {
            // GD.Print("game speed up");
        }

        if (Input.IsActionJustPressed("game_speed_down")) {
            GD.Print("game speed down");
        }

        if (Input.IsActionJustPressed("toggle_pause")) {
            Global.SetGamePaused(!Global.GamePaused);
        }

        if (Input.IsActionJustPressed("toggle_map")) {
            if (menuOpen) { return; }
            if (mapScreenActive) {
                mapScreenActive = false;
                Position = stashPosition;
                Rotation = defaultRotation;
                EvalCamFov(zoomLevel);
            }
            else {
                stashPosition = Position;
                Position = new Vector3(0, 75, 0);
                Rotation = new Vector3(0, 0, 0);
                mapScreenActive = true;
                movementSpeed = 0;
                cam.Fov = 120.0f;
            }

        }

        if (Input.IsActionJustPressed("focus_target")) {
            if (objectHandle != null && !menuOpen) {
                Vector3 objectPosition = new Vector3(objectHandle.GetObjectPosition().X, Position.Y, objectHandle.GetObjectPosition().Z);
                //while (!playerHandle.Position.IsEqualApprox(targetPosition)) { 
                //    // lerp player to this ship
                //    // playerHandle.Position = 
                //}
                Position = objectPosition;
            }
        }

        if (Input.IsActionJustPressed("move_home")) {
            if (menuOpen) { return; }
            Position = new Vector3(-15.0f, 20.0f, -25.0f);
        }

        Vector3 targetPosition = new Vector3(0,0,0);
        Vector3 direction = Vector3.Zero;
        Vector2 inputDir = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();

        targetPosition.X = Math.Clamp(Position.X + (direction.X * movementSpeed), minCamPosition.X, maxCamPosition.X);
        targetPosition.Y = Position.Y;
        targetPosition.Z = Math.Clamp(Position.Z + (direction.Z * movementSpeed), minCamPosition.Y, maxCamPosition.Y);

        if (menuOpen) { return; }
        Position = targetPosition;

    }
}
