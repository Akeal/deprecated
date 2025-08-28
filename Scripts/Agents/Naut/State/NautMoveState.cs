using Deprecated.States;
using Godot;
public class NautMoveState : State<Naut> {
    private const float moveSpeed = 3;
    private Vector2I _movementDirection;
    private Vector2 startingPosition, endingPosition;
    private double percentMoved = 0;
    public NautMoveState(Vector2I movementDirection){
        _movementDirection = movementDirection;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        startingPosition = Agent.GlobalPosition;
        endingPosition = startingPosition + (_movementDirection * TheStation.TILE_SIZE);
        if(!CanMove()){
            Agent.SetState(new NautIdleState(_movementDirection));
            return;
        }
        
        Agent.PlayAnimation(GetWalkAnimation());
    }

    public override void OnPhysicsProcess(double delta)
    {
        base.OnPhysicsProcess(delta);
        percentMoved += (delta * moveSpeed);

        Agent.GlobalPosition = startingPosition.Lerp(endingPosition, (float)percentMoved);

        if(percentMoved >= 1)
        {
            Vector2I nextMoveDirection = Agent.GetInputDirection();
            Agent.SetState(nextMoveDirection != Vector2I.Zero ? new NautMoveState(nextMoveDirection) : new NautIdleState(_movementDirection));
        }
    }

    private bool CanMove(){
        Agent.CollisionRayCast.TargetPosition = (_movementDirection * TheStation.TILE_SIZE);
        Agent.CollisionRayCast.ForceUpdateTransform();
        Agent.CollisionRayCast.ForceRaycastUpdate();

        Agent.InteractRayCast.TargetPosition = (_movementDirection * (int)(TheStation.TILE_SIZE * 1.5));
        Agent.InteractRayCast.ForceUpdateTransform();
        Agent.InteractRayCast.ForceRaycastUpdate();

        return !Agent.CollisionRayCast.IsColliding();
    }

    private string GetWalkAnimation()
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
		Agent.FlipH = _movementDirection.X > 0;

		if(_movementDirection == Vector2I.Up){
			return Naut.Animations.Common.WALK_UP;
		}
		else if(_movementDirection == Vector2I.Down){
			return Naut.Animations.Common.WALK_DOWN;
		}
		else{
			return Naut.Animations.Common.WALK_LEFT_RIGHT;
		}
	}
}