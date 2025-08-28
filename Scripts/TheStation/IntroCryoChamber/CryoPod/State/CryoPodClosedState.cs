using Deprecated.States;
using Godot;
using System;
public class CryoPodClosedState : State<CryoPod> {
	public override void OnEnter()
	{
		base.OnEnter();
		Agent.PlayAnimation(CryoPod.Animations.CLOSED);
	}
	public override void OnProcess(double delta)
	{
		base.OnProcess(delta);
		if(Agent.Interact()){
			Agent.SetState(new CryoPodOpeningState());
		}
	}
}
