using Godot;
using System;

public static class Global {
    public static bool GamePaused;

    public static void SetGamePaused(bool _gamePaused) {
        GamePaused = _gamePaused;
    }

    public static string GetRandomShipName() {

        // NOTE: Collected from https://www.fantasynamegenerators.com/ship-names.php
        // & https://www.fantasynamegenerators.com/pirate-ship-names.php
        string[] names = {
            "HMS James",
            "The Austere",
            "Sultan of Cuba",
            "Yucatan's Bane",
            "HMS Ipswich",
            "The Interceptor",
            "The Nesbitt",
            "Princess' Deceit",
            "The Ranger",
            "The Druid",
            "Salamine",
            "Poseidon's Plunder",
            "The Minnow",
            "Kraken's Plague",
            "HMS Boston",
            "Wells' Pride",
            "Aldeburgh's Sapphire",
            "Fisher's Shack",
            "The Dunwich Horror",
            "Orford's Secret",
            "Whitby's Trawler",
            "Guardian de Santiago",
            "Adaleide",
            "Queen Anne",
            "Augustine",
            "Angeline",
            "San Jose"
        };

        Random rand = new Random();
        string selectedName = names[rand.Next(names.Length - 1)];

        return selectedName;
    }
}
