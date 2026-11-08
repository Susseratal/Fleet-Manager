using Godot;
using System;

public class ResourceData  {
    [Export]
    public Sprite2D icon;
    [Export]
    public string name;
    [Export]
    public int baseValue;
}

public class Sugar : ResourceData {
    public Sugar() {
        icon = null;
        name = "Sugar";
        baseValue = 5;
    }
}

public partial class ShipData { 
    public enum ShipType { 
        Schooner,
        Sloop,
        Brigantine,
        Frigate,
        ManOfWar
    }

    public string ShipName;
    public ResourceData[] Inventory;

    // uhh
}
