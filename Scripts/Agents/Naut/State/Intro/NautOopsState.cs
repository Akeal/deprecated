using Deprecated.States;
using Godot;
public class NautOopsState : State<Naut> {
	private const double FIVE_HOURS = 10.0;//18000.0
	private double waitTime = 0;
	private CryoPod _cryoPod;
	private bool ow = false;
	public NautOopsState(CryoPod cryoPod)
	{
		_cryoPod = cryoPod;
	}

    public override void OnEnter()
    {
        base.OnEnter();
		_cryoPod.OopsAnimationCurve.Play(Callable.From(() => { ow = true; }));
		_cryoPod.HelmetRollAnimationCurve.Visible = true;
		// We expect this animation to complete and we'll hide the animated sprite when ow == true, because Naut will be "wearing" it at that point.
		_cryoPod.HelmetRollAnimationCurve.Play();
    }


	public override void OnProcess(double delta)
	{
		base.OnProcess(delta);
		if(ow){
			waitTime += delta;
			_cryoPod.HelmetRollAnimationCurve.Visible = false;
			if(waitTime > FIVE_HOURS){
				Agent.ReparentStateMachineNode(_cryoPod.GetParent());
				Agent.GlobalPosition = TheStation.GetClosestGlobalTilePosition(Agent.GlobalPosition);
				Callable onStandAnimationComplete = Callable.From(() => {
					Agent.SetState(new NautIdleState(Vector2I.Left));
				});
				Agent.PlayAnimation(Naut.Animations.Common.STAND, 1, onStandAnimationComplete);
				ow = false; // Set it back to false so we don't trigger this logic more than once.
			}
		}
	}
}
