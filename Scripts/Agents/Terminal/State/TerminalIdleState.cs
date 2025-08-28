using Deprecated.States;
using Godot;
using Deprecated.Interaction;
using System.Collections.Generic;
public class TerminalIdleState<T> : State<T>, IInteractable
    where T : Terminal
{
    public List<IInteractor> interactors => throw new System.NotImplementedException();

    public void AfterInteraction(IInteractor interactingNode) { }

    public override void OnEnter()
    {
        base.OnEnter();
        Agent.Background.Visible = false;
    }

    public void OnInteraction(IInteractor interactingNode)
    {
        interactingNode.OnInteract(Agent, DuringEnum.Wait);
        Agent.SetState(Agent.GetOutputState<T>(Agent.GetTree()));
    }
}