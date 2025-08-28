using Godot;
using System.Collections.Generic;
using Deprecated.States;

// Psst... if you just got here, this is the tutorial.
// I know you may want to forge ahead, 
// and you totally can whenever, 
// but if you want to potentially learn some things to help you get a footing follow along.
// We'll cover how I've set up the state machine and some Godot basics that are good to know.
// Do im proud
public partial class CryoPod : Node2D {
	// This is an export variable.
	// Exported variables can be set from the editor, from any Node that is a child of this Node.
	// So if you see an exported variable, 
	// you can rest assured it contains an instance of the specified class and by the time we get to our _Ready() function, it will not be NULL
	// ...Unless I forgot to set one in the editor
	[Export]
	public Naut Naut;
	[Export]
	public AnimatedSprite2D Lid;
	[Export]
	public AnimationCurve OopsAnimationCurve, HelmetRollAnimationCurve;

	// This is Godot's scene-entry event hook, _Ready(). 
	// It gets called when the object first -enters the scene- AKA: "When it enters the game"
	public override void _Ready()
	{
		base._Ready();
		this.SetState(new CryoPodClosedState());
	}

	// This is the _Process() function
	// It's a godot node event hook that occurs ONCE per rendered frame.
	public override void _Process(double delta)
	{
		base._Process(delta);
		this.CurrentState().OnProcess(delta);
	}

	// This is the _Physics_Process() function
	// It's a godot node event hook that occurs ~physics_ticks~ per rendered frame.
	// Helps to smooth out motion I think. TODO: more info here?
	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		this.CurrentState().OnPhysicsProcess(delta);
	}
}
