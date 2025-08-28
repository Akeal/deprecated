using Godot;
using Deprecated.States;

public class CryoPodReleaseState : State<CryoPod> {
	private const double ANIMATION_LENGTH = 5.0;
	private double _heldTimeLength = 0;
	public CryoPodReleaseState(double heldTimeLength){
		_heldTimeLength = heldTimeLength;
	}

	public override void OnEnter()
	{
		base.OnEnter();
		Agent.PlayAnimation(CryoPod.Animations.OPEN);
		State<Naut> nothingCouldGoWrong = AFineBeginningForOurHero(_heldTimeLength);
		
		Agent.Naut.SetState(nothingCouldGoWrong);
	}

	private State<Naut> AFineBeginningForOurHero(double heldLength){
		if(heldLength >= ANIMATION_LENGTH){
			return new NautWakeState(Agent);
		}
		else{
			return new NautOopsState(Agent);
		}
	}
}
