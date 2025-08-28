using Godot;
using Deprecated.States;
using Deprecated.Interaction;
using Deprecated.Gate;

public partial class Door : AnimatedSprite2D, ILock<Door> {
    [Export]
    public bool DefaultOpen = false;
    [Export]
    public CollisionShape2D Collision;

    public override void _Ready()
    {
        base._Ready();
        if(DefaultOpen){
            this.SetState(new DoorOpenState());
        }
        else{
            this.SetState(new DoorClosedState());
        }
    }

    public void Lock()
    {
        if(this.CurrentState() is DoorOpenState doorOpenState){
            doorOpenState.Close();
        }
    }

    public void Unlock()
    {
        if(this.CurrentState() is DoorClosedState doorClosedState){
            doorClosedState.Open();
        }
    }
}