using Deprecated.Interaction;
using Deprecated.States;
using Godot;
public class NautWaitState : State<Naut>, IInteractor {
    private Vector2I _originalDirection;
    public NautWaitState(Vector2I originalDirection){
        _originalDirection = originalDirection;
    }
    public void OnInteract(IInteractable interactableNode, DuringEnum during)
    {
        if(during == DuringEnum.Idle){
            Agent.SetState(new NautIdleState(_originalDirection));
        }
    }
}