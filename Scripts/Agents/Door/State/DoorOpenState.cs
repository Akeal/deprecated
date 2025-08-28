using Deprecated.States;
using Godot;

public class DoorOpenState : State<Door> {
    public override void OnEnter()
    {
        base.OnEnter();
        Agent.Visible = false;
        Agent.Collision.Disabled = true;
    }

    public void Close(){
        Agent.SetState(new DoorClosingState());
    }
}