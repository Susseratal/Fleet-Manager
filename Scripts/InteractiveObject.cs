using Godot;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public interface InteractiveObject {
    struct Info {
        public string Name;
    }

    public abstract void Interact();
    public abstract Info GetInfo();
    public abstract Vector3 GetObjectPosition();
}
