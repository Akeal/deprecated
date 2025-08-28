using Deprecated.States;
using Godot;
using Deprecated.Interaction;
public class NautIdleState : State<Naut>, IInteractor {

	// If the player was holding the interact button when we entered this state,
	// then we want to wait until they release the button before processing the next interact input.
	private bool interactHasBeenReleased;
	private Vector2I _priorMoveDirection;

	public NautIdleState(Vector2I priorMoveDirection){
		_priorMoveDirection = priorMoveDirection;
	}

    public override void OnEnter()
	{
		base.OnEnter();
		// Sanity check to set us back on the nearest tile
		interactHasBeenReleased = !Agent.InteractHeld();
		Agent.PlayAnimation(GetIdleAnimation());
	}

    public override void OnProcess(double delta)
	{
		base.OnProcess(delta);

		interactHasBeenReleased = interactHasBeenReleased || !Agent.InteractHeld();

		Vector2I movementDirection = Agent.GetInputDirection();
		if(movementDirection != Vector2I.Zero){
			Agent.SetState(new NautMoveState(movementDirection));
		}
		else if(interactHasBeenReleased && Agent.InteractHeld()){
			if(Agent.InteractRayCast.IsColliding()){
				Node node = Agent.InteractRayCast.GetCollider() as Node;
                if(node is IInteractable interactableNode)
				{
					interactableNode.OnInteraction(Agent);
                }
			}
		}
	}

    public void OnInteract(IInteractable interactableNode, DuringEnum during)
    {
        if(during == DuringEnum.Wait){
            Agent.SetState(new NautWaitState(_priorMoveDirection));
        }
    }

	private string GetIdleAnimation()
	{
		// If the direction is to the right, mirror the sprite to face right instead of left
        // I would say this is generally "bad practice", and would be considered a "side effect"
        // The intent of the method is to return the name of the animation to play, but this method itself does not play it.
        // By flipping the sprite we're setting a value before we might be ready to actually run the animation.
        // Example:
        // If we were to be calling...
        // string animationToPlay = GetWalkAnimation();
        // ... do other stuff for a while, maybe taking multiple frames ... 
        // ^ during this period of time the sprite would still be flipped horizontally but the animation would not be playing until...
        // PlayAnimation(animationToPlay); is called
        // I'm leaving this in as an example but better would be to have this line just before or after PlayAnimation(animationToPlay);
        // So that the animation begins and we flip at the same time.
        // Having it this way, despite being bad practice, is fine for my purposes though as I'm directly using the string like...
        // PlayAnimation(GetWalkAnimation());
        // Best practices are designed to keep yourself out of trouble, but they're not law.
        // If you're writing non-production, low-stakes, code I'd recommend not worrying about best-practice. Experience is the best teacher.
		Agent.FlipH = _priorMoveDirection.X > 0;

		if(_priorMoveDirection == Vector2I.Up){
			return Naut.Animations.Common.IDLE_UP;
		}
		else if(_priorMoveDirection == Vector2I.Down){
			return Naut.Animations.Common.IDLE_DOWN;
		}
		else{
			return Naut.Animations.Common.IDLE_LEFT_RIGHT;
		}
	}
}
