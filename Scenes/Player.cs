using Godot;
using System;

public partial class Player : Node3D {
    public bool paused = false;

    Camera3D cam;
    float movementSpeed = 1;
    int zoomLevel = 0;
    Vector2 viewportSize, viewportCamPosition;
    public InteractiveObject objectHandle;

    public void TogglePause() {
        return;
    }

    private void EvalCamFov(int zl) {
        switch (zl) {
            case 0:
                cam.Fov = 90.0f;
                movementSpeed = 1;
                break;
            case 1:
                cam.Fov = 80.0f;
                movementSpeed = 0.8f;
                break;
            case 2:
                cam.Fov = 60.0f;
                movementSpeed = 0.6f;
                break;
            case 3:
                cam.Fov = 40.0f;
                movementSpeed = 0.4f;
                break;
            case 4:
                cam.Fov = 20.0f;
                movementSpeed = 0.2f;
                break;
            default:
                break;
        }

    }

    private void DrawLine(Vector3 pos1, Vector3 pos2, Color color) { 
        MeshInstance3D meshInstance = new MeshInstance3D();
        ImmediateMesh immediateMesh = new ImmediateMesh();
        // Material material = new Material();
        StandardMaterial3D material = new StandardMaterial3D();

        meshInstance.Mesh = immediateMesh;
        meshInstance.CastShadow = GeometryInstance3D.ShadowCastingSetting.Off;
        immediateMesh.SurfaceBegin(Mesh.PrimitiveType.Lines, material);
        immediateMesh.SurfaceAddVertex(pos1);
        immediateMesh.SurfaceAddVertex(pos2);
        immediateMesh.SurfaceEnd();

        material.ShadingMode = BaseMaterial3D.ShadingModeEnum.Unshaded;
        material.AlbedoColor = color;
        GetTree().GetRoot().AddChild(meshInstance);
    }

    private Godot.Collections.Dictionary LineTrace(Vector3 pos1, Vector3 pos2) { 
        PhysicsDirectSpaceState3D worldSpace = GetWorld3D().DirectSpaceState;
        PhysicsRayQueryParameters3D rayCastParams = PhysicsRayQueryParameters3D.Create(pos1, pos2);
        rayCastParams.CollideWithAreas = true;
        Godot.Collections.Dictionary collisions = worldSpace.IntersectRay(rayCastParams);
        return collisions;
    }

    public Player() { }

    public override void _Ready() {
        base._Ready();
        cam = GetNode<Camera3D>("Camera3D");

        viewportSize.X = GetViewport().GetVisibleRect().Size.X;
        viewportSize.Y = GetViewport().GetVisibleRect().Size.Y;
        viewportCamPosition.X = viewportSize.X / 2;
        viewportCamPosition.Y = viewportSize.Y / 2;
    }

    public override void _Input(InputEvent @event) {
        base._Input(@event);
    }

    public override void _PhysicsProcess(double delta) {
        if (Input.IsActionJustPressed("left_click")) {
            if (objectHandle != null) { objectHandle.Interact(); }
        }

        if (Input.IsActionJustPressed("right_click")) {
            GD.Print("Right Click");
        }

        if (Input.IsActionJustPressed("zoom_out")) {
            zoomLevel = Math.Clamp(zoomLevel - 1, 0, 4);
            EvalCamFov(zoomLevel);
        }

        if (Input.IsActionJustPressed("zoom_in")) {
            zoomLevel = Math.Clamp(zoomLevel + 1, 0, 4);
            EvalCamFov(zoomLevel);
        }

        if (Input.IsActionJustPressed("game_speed_up")) {
            GD.Print("game speed up");
        }

        if (Input.IsActionJustPressed("game_speed_down")) {
            GD.Print("game speed down");
        }

        if (Input.IsActionJustPressed("toggle_pause")) {
            GD.Print("pause/unpause");
        }

        if (Input.IsActionJustPressed("focus_target")) {
            if (objectHandle != null) {
                Vector3 objectPosition = new Vector3(objectHandle.GetObjectPosition().X, Position.Y, objectHandle.GetObjectPosition().Z);
                //while (!playerHandle.Position.IsEqualApprox(targetPosition)) { 
                //    // lerp player to this ship
                //    // playerHandle.Position = 
                //}
                Position = objectPosition;
            }
        }

        Vector3 targetPosition = new Vector3(0,0,0);
        Vector3 direction = Vector3.Zero;
        Vector2 inputDir = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();

        targetPosition.X = Math.Clamp(Position.X + (direction.X * movementSpeed), -35.0f, 35.0f);
        targetPosition.Y = Position.Y;
        targetPosition.Z = Math.Clamp(Position.Z + (direction.Z * movementSpeed), -35.0f, 35.0f);

        Position = targetPosition;

        // Velocity = _targetVelocity;
        // MoveAndSlide();

    }
}
