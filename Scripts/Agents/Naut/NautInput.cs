using Constants;
using Godot;
// A partial class just means that we can split up the definition of a single class into multiple files.
// You can think of this as still being part of Naut.cs
// This is handy to keep sections of related logic out of the main file. Keeps things tidier so we don't have one massive file, but isn't required.
public partial class Naut : AnimatedSprite2D {
    public Vector2I GetInputDirection(){
        Vector2I inputDirection = Vector2I.Zero;
        if(Input.IsActionPressed(InputValue.LEFT)){
            inputDirection = new Vector2I(-1, 0);
        }
        else if(Input.IsActionPressed(InputValue.RIGHT)){
            inputDirection = new Vector2I(1, 0);
        }
        else if(Input.IsActionPressed(InputValue.UP)){
            inputDirection = new Vector2I(0, -1);
        }
        else if(Input.IsActionPressed(InputValue.DOWN)){
            inputDirection = new Vector2I(0, 1);
        }
        return inputDirection;
    }

    public bool InteractHeld(){
        return Input.IsActionPressed(InputValue.INTERACT);
    }
}