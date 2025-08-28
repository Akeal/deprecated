using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class Naut : AnimatedSprite2D {
	// Note: This is not a comprehensive list of animations that occur in-game.
	// Some animations do not require a string constant because they are driven by node export variables (like for AnimationCurve)
	public static class Animations {
		public static class Common {
			public const string IDLE_LEFT_RIGHT = "IdleLeftRight";
			public const string IDLE_UP = "IdleUp";
			public const string IDLE_DOWN = "IdleDown";
			public const string WALK_LEFT_RIGHT = "WalkLeftRight";
			public const string WALK_UP = "WalkUp";
			public const string WALK_DOWN = "WalkDown";
			public const string STAND = "Stand";
		}
		
		public static class Large {
			 // I wanted the large animations to appear at the bottom in the editor so there's a "z" prepended.
			 // I mean, aha! It's zee large wake!
			public const string WAKE = "zLargeWake";
			public const string WAKE_STAND = "zLargeWakeStand";
		}
	}

	public void PlayAnimation(string animation, float speed = 1, Callable? onComplete = null)
	{
		// Don't worry too much bout this here.
		// What we're doing is getting all the signals that Naut is currently connected to (see below for signal info)
		Godot.Collections.Array<Godot.Collections.Dictionary> callableDict = GetSignalConnectionList(AnimatedSprite2D.SignalName.AnimationFinished);
		if(callableDict.Count >= 1 && callableDict.First().ContainsKey("callable")){
			Callable existingCallable = (Callable)GetSignalConnectionList(AnimatedSprite2D.SignalName.AnimationFinished).First()["callable"];
			// If we're already connected to AnimationFinished, disconnect from the existing one.
			Disconnect(AnimatedSprite2D.SignalName.AnimationFinished, existingCallable);
		}

		// We only do this if we provided a callable in the method call.
		if(onComplete != null)
		{
			// This connects our Callable to the "AnimationFinished" signal.
			// When the Godot node AnimatedSprite2D completes the last frame of its animation, it emits the AnimationFinished signal.
			// Here we're connecting Naut's node (he is a node, he inherits from a node class) to the callable
			// This tells Godot to call our logic when then Naut emits the signal.
			Connect(AnimatedSprite2D.SignalName.AnimationFinished, onComplete.Value);
		}

		// Actually start the animation
		Play(animation, speed);
	}
}