using Constants;
using Godot;
// A partial class just means that we can split up the definition of a single class into multiple files.
// You can think of this as still being part of CryoPod.cs
// This is handy to keep sections of related logic out of the main file. Keeps things tidier so we don't have one massive file, but isn't required.
public partial class CryoPod : Node2D {
    public bool Interact(){
        return Input.IsActionPressed(InputValue.INTERACT);
    }
}