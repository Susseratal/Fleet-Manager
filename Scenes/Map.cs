using Godot;
using System;
using System.Collections.Generic;

public partial class Map : Node3D {
    Godot.Collections.Array<Node> cities;
    Timer gameClock;

    public List<ResourceData> LootTable;

    public void PopulateLootTable() {
        LootTable = new List<ResourceData>();
        using var file = FileAccess.Open("res://Assets/LootTable.csv", FileAccess.ModeFlags.Read);
        while (file.GetPosition() < file.GetLength()) { 
            string[] content = file.GetCsvLine();
            ResourceData newResource = new ResourceData();
            newResource.name = content[0];
            newResource.baseValue = float.Parse(content[1]);
            newResource.unit = content[2];
            newResource.sprite = GD.Load<Texture2D>(content[3]);
            LootTable.Add(newResource);
        }
    }

    public override void _Ready() { 
        base._Ready();

        gameClock = GetNode<Timer>("GameClock");
        cities = GetNode<Node3D>("Cities").FindChildren("*", "", false, true);

        PopulateLootTable();

        Random rand = new Random();

        foreach (City c in cities) {
            int prodIndex = rand.Next(LootTable.Count);
            int reqIndex = prodIndex;
            while (reqIndex == prodIndex) { 
                reqIndex = rand.Next(LootTable.Count);
            }

            c.cityMenu.PopulateInfo(c.Name, LootTable[prodIndex], LootTable[reqIndex], c);
        }
    }

    public void OnClockTimeout() {
        GD.Print("one minute elapsed");
    }
}
