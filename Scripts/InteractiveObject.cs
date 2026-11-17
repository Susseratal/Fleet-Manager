using Godot;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public interface InteractiveObject {
    struct Info {
        public string Name;
    }

    public abstract void Interact(Player _playerHandle);
    public abstract Info GetInfo();
    public abstract Vector3 GetObjectPosition();
}

public class Inventory {
    private ResourceData[] Contents;

    public ResourceData[] GetContents() {
        return Contents;
    }

    public void AddItem(ResourceData _item) { 
        // pass
    }

    public Inventory() { 
        // pass
    }
}
