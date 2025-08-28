using Deprecated.States;
using Godot;
using System;
public class CryoPodOpeningState : State<CryoPod> {
	private double _totalDelta;
	private readonly string _animation;

	public CryoPodOpeningState(){
		_totalDelta = 0;
		_animation = CryoPod.Animations.OPENING;
	}

	public CryoPodOpeningState(string animation, double delta){
		_totalDelta = delta;
		_animation = animation;
	}


	public override void OnEnter()
	{
		base.OnEnter();
		Callable onAnimationComplete = Callable.From(() => {
			Agent.SetState(new CryoPodOpeningState(CryoPod.Animations.OPEN, _totalDelta));
		});
		Agent.PlayAnimation(_animation, 1, onAnimationComplete);
	}

	public override void OnProcess(double delta)
	{
		base.OnProcess(delta);
		_totalDelta += delta;
		if(!Agent.Interact()){
			Agent.SetState(new CryoPodReleaseState(_totalDelta));
		}
	}
}
