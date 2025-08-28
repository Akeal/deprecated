using Deprecated.States;
using Deprecated.Interaction;
using Godot;
using System;
public class NautWakeState : State<Naut> {
    private Vector2 endingPosition;
	private CryoPod _cryoPod;
	public NautWakeState(CryoPod cryoPod){
		_cryoPod = cryoPod;
	}

	public override void OnEnter()
	{
		base.OnEnter();
		// This animation is only used if the player releases the button early - so if we've entered this state we don't need it.
		_cryoPod.HelmetRollAnimationCurve.Visible = false;

		Callable onWakeComplete = Callable.From(() => {
			// We use a larger-than-usual animation for wake, where Naut's sprite animation itself doesn't move...
			// But he moves within the animation.
			// WAKE_STAND is once again centered to the middle of the animation so we need to move the animation position to compensate for the visual movement before.
			Agent.SetState(new NautFiveMoreMinutesState(endingPosition, _cryoPod));
		});

		Agent.PlayAnimation(Naut.Animations.Large.WAKE, 1, onWakeComplete);

		endingPosition = new Vector2(Agent.GlobalPosition.X - (int)(TheStation.TILE_SIZE * 1.5), Agent.GlobalPosition.Y);
	}

	public override void OnProcess(double delta)
	{
		base.OnProcess(delta);
		Vector2I movementDirection = Agent.GetInputDirection();
		if(movementDirection != Vector2I.Zero){
			Agent.SetState(new NautMoveState(movementDirection));
		}
		else if(Agent.InteractHeld()){
			if(Agent.InteractRayCast.IsColliding()){
				Node node = Agent.InteractRayCast.GetCollider() as Node;
                if(node is IInteractable interactableNode){
                    interactableNode.OnInteraction(Agent);
                }
				
			}
		}
	}
}
