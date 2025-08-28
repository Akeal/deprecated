using Constants;
using Godot;
using Deprecated.Interaction;
// A partial class just means that we can split up the definition of a single class into multiple files.
// You can think of this as still being part of Naut.cs
// This is handy to keep sections of related logic out of the main file. Keeps things tidier so we don't have one massive file, but isn't required.
public partial class Terminal : Area2D, IInteractable {
    public bool Interact(){
        return Input.IsActionPressed(InputValue.INTERACT);
    }
}