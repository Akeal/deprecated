using Deprecated.States;
using Godot;

public class DoorOpeningState : State<Door> {
    public override void OnEnter()
    {
        base.OnEnter();
        Agent.Collision.Disabled = false;
        //TODO: Play animation
        Agent.SetState(new DoorOpenState());
    }
}