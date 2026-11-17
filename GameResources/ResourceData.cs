using Godot;
using System;

public struct ResourceData { 
    public Texture2D sprite;
    public string name;
    public float baseValue;
    public string unit;
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
    public ShipType shipType;
    public ResourceData[] Inventory;
}
