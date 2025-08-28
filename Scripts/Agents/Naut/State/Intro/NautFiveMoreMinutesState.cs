using Deprecated.States;
using Deprecated.Interaction;
using Godot;
using System;

public class NautFiveMoreMinutesState : State<Naut> {
	// These are const (constant) variables. Meaning they don't ever change from their original value.
	// It makes reading the code much easier to see stuff like 'GoToBed(SLEEP_TIME);' instead of 'GoToBed(5)';
	// If we use the value in multiple locations and want to update it, we also only have to update the variable initialization here to have the new value.
    private const double SLEEP_TIME = 5;
	private const float CAMERA_PAN_OFFSET = 10;


	// Some variables to track our camera movement------------
	private readonly double[] cameraPanDelays = [0, 0, 1];
	private readonly Vector2[] cameraPanPositions;
	private int cameraPanIndex = 0;
	private double cameraPanPercent = 0;
	// -------------------------------------------------------

	// We use this to keep track of how much time has passed
	private double currentWaitTotal = 0;


	// Splits this one state into two "states". Asleep and waking up.
	// We could physically make this two separate state files but sometimes you just want to capture an entire action in one file.
	// Most of the time you'll want to split it. But NautFiveMoreMinutesState.cs is used once(!!1!) in the game flow, meaning it's hyper-specific in what I want to achieve, and when
	// States you intend to reuse you'll want to be smaller and more flexible / reusable ----------------------
	private bool zzz = true;
	// --------------------------------------------------------------------------------------------------------

	// How fast do it be?
	private double cameraPanSpeed;
	private CryoPod _cryoPod;
	public NautFiveMoreMinutesState(Vector2 newGlobalPosition, CryoPod cryoPod){

		// We pan the camera to three positions during this state, in an order.
		cameraPanPositions = new Vector2[]
		{ 
			new Vector2(newGlobalPosition.X - CAMERA_PAN_OFFSET, newGlobalPosition.Y + CAMERA_PAN_OFFSET), // Position one
			new Vector2(newGlobalPosition.X, newGlobalPosition.Y + CAMERA_PAN_OFFSET), // Position two
			newGlobalPosition // Our final camera resting position, right on top of Naut
		};

		_cryoPod = cryoPod; // We want to remember the reference to the cryoPod for later.
	}

	public override void OnEnter()
	{
		base.OnEnter();
		cameraPanSpeed = GetCameraPanSpeed(cameraPanPositions[cameraPanIndex]);
		Agent.ReparentStateMachineNode(_cryoPod.GetParent());
	}

	public override void OnProcess(double delta)
	{
		base.OnProcess(delta);
		// We don't have to do it this way but we're panning the camera as a percentage. 0% (0.00) is original location -> 100% (1.00) is the destination
		cameraPanPercent += (delta * cameraPanSpeed);
		// Increment the amount of time we've been waiting by the amount of time that has passed
		currentWaitTotal += delta;
		if(zzz)
		{
			// Lerp the camera to the first spot. cameraPanIndex is 0 because we defaulted it to 0
			Agent.Camera.GlobalPosition = Agent.Camera.GlobalPosition.Lerp(cameraPanPositions[cameraPanIndex], (float)cameraPanPercent);
			
			if(currentWaitTotal >= SLEEP_TIME)
			{
				WakeUp();
			}
		}
		else
		{
			TotallyMVPEssentialFeature();
		}

		if(currentWaitTotal >= cameraPanDelays[cameraPanIndex] && // If we've waited as long as the current delay index.
		cameraPanIndex < cameraPanDelays.Length - 1 && // And we're not going to be exceeding the bounds of the array.
		cameraPanIndex != 0) // And we're not on the 0th index. We increment from the 0th to 1st index elsewhere in this file.
		{ // Then...
			cameraPanIndex++; // move the index to the next value
			cameraPanSpeed = GetCameraPanSpeed(cameraPanPositions[cameraPanIndex]); // Calculate the camera move speed
			cameraPanPercent = 0; // We're back to zero percent panned, to the next position.
		}
	}


	private void WakeUp()
	{
		// We're no longer zzz
		zzz = false;
		// We're going to use these variables again for the next part so zero them out.
		cameraPanPercent = 0;
		currentWaitTotal = 0;
		
		// We've completed the first pan so change the index from 0 to 1 so we can pan to the next index location
		cameraPanIndex = 1;

		// This callable enables us to execute logic not now, but -later-
		// In this case it's a block of code we want to run when the wake-up animation is complete.
		Callable onWakeAnimationComplete = Callable.From(
			() => // We're creating the callable from a lambda function. If that means nothing to you, don't worry about it for now.
		{
			Agent.SetState(new NautIdleState(Vector2I.Left));
			// We're beginning to play the stand animation. Set Naut's position to the final place we want to end up.
			// Our camera panning ends on the final location, so we're going to get it from there. Index 2.
			Agent.GlobalPosition = TheStation.GetClosestGlobalTilePosition(cameraPanPositions[2]);
			Agent.Camera.Position = Vector2.Zero;
		});
		
		// Play the animation where we scrunch up into a ball and then stand. Pass our callable in.
		Agent.PlayAnimation(Naut.Animations.Large.WAKE_STAND, 1, onWakeAnimationComplete);
	}

	private void TotallyMVPEssentialFeature()
	{
		// When we're on animation delay index 1 (we're scrunching up) we lerp the camera to the right 		>
		// When we're on animation delay index 2 (standing up) we lerp the camera to our final position, up ^
		Agent.Camera.GlobalPosition = Agent.Camera.GlobalPosition.Lerp(cameraPanPositions[cameraPanIndex], (float)cameraPanPercent);
	}

	// Hey this seems like a handy method!
	// Maybe I'll take it out of this file and put it somewhere I can get to it more easily, like on the camera script itself.
	// Next time I need to use this method somewhere else I'll do that.
	// Or maybe I'll add the logic there and leave this here as an example of how incremental development occurs (normally you'd refactor and move this)
	private double GetCameraPanSpeed(Vector2 panDestination)
	{
		// Get the distance from the camera to where we want the camera to go
		float distance = Agent.Camera.GlobalPosition.DistanceTo(panDestination);
		// Each time we move the camera we want to move it 1% closer to the destination. So cameraPanSpeed represents 1% of the distance.
		double speed = 1 / distance;
		// Let's return the speed so we can keep track of it. This speed should be smooth from any camera point A to point B
		return speed;
	}
}
