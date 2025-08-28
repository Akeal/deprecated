using Deprecated.States;
using Godot;

public class DoorClosedState : State<Door> {
    public override void OnEnter()
    {
        base.OnEnter();
    }

    public void Open(){
        Agent.SetState(new DoorOpeningState());
    }
}