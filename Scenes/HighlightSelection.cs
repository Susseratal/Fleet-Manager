using Godot;
using System;

public partial class HighlightSelection : MeshInstance3D {
    [Export]
    Material outlineMaterial;

    MeshInstance3D mesh;
    Player playerHandle;

    public override void _Ready() {
        base._Ready();
        // mesh = GetNode<MeshInstance3D>("Mesh");
    }

    public void OnStaticBodyMouseEntered() { 
        this.MaterialOverlay = outlineMaterial;
        playerHandle.objectHandle = null;
    }

    public void OnStaticBodyMouseExited() {
        this.MaterialOverlay = null;
    }
}
