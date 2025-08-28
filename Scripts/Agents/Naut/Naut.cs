using Godot;
using Deprecated.States;
using Deprecated.Interaction;

// Psst... if you just got here, this is the tutorial.
// I know you may want to forge ahead, 
// and you totally can whenever, 
// but if you want to potentially learn some things to help you get a footing follow along.
// We'll cover how I've set up the state machine and some Godot basics that are good to know.
// Do im proud
public partial class Naut : AnimatedSprite2D, IInteractor {
	[Export]
	public RayCast2D CollisionRayCast, InteractRayCast;

	[Export]
	public Camera2D Camera;

	// This is Godot's scene-entry event hook. So it gets called when the object first -enters the scene- TLDR: "When it enters the game"
	// See - Footnote - below which applies to all of _Ready(), _Process(), and _Physics_Process().
	public override void _Ready()
	{
		// If you follow the inheritance chain up to where we inherit from one of Godot's node classes...
		// So Naut -> StateMachineAnimatedSprite2DNode<T> -> StateMachineNode<T> -> Node
		// When we reference "base" that means the thing we inherited from.
		// So when we call base._Ready() what that means is that we're calling the logic defined for _Ready() in the parent class
		// Having base._Ready() at the start of our CURRENT _Ready() call means we first execute the _Ready() logic defined on the parent class.
		// Then AFTER base.Ready() has executed, we begin our _Ready() logic for Naut himself
		base._Ready();
		this.SetState(new NautCryoState());
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

    public void OnInteract(IInteractable interactableNode, DuringEnum during)
    {
		// If the state Naut is currently in accepts interact commands, pass it to the current state.
		if(this.CurrentState() is IInteractor interactableState){
			interactableState.OnInteract(interactableNode, during);
		}
    }

    // public void AfterInteract(IInteractable interactableNode)
    // {
    //     switch(after){
	// 		case AfterEnum.Idle:
	// 			this.SetState(new NautIdleState());
	// 			break;
	// 	}
    // }

    // - Footnote -
    // So there are a lot of nodes in the scene, yeah? What if we need a different node to perform its logic before ours?
    // Like say... Naut here. As a hypothetical, what if we wanted him to be able to play the violin (trust me you don't)?
    // But let's say the violin has all sorts of properties or methods we want to get access to.
    // That's where the order of operation of each node comes in. So what order do they take? In the scene tree bottom-most to top-most, and for each item along that, innermost to outermost.
    // Innermost to outermost means that, if a node needs a reference to its child, parent nodes will always have access to parent nodes AFTER that parent node has executed its own _Ready(), _Process(), or _PhysicsProcess().
    // If the Violin he's holding has a method like InitializeSongsLibraryOrSomethingIdk(), and the violin's _Ready() calls InitializeSongsLibraryOrSomethingIdk(),
    // then you can feel safe knowing that here in Naut's _Ready() function, the Violin will have initialized the songs library or something idk. Because the Naut node is the parent of the violin node
    // That means that if Naut's _Ready() function called violin.Play(), unfortunately everything would be initialized, and that would mean that he would play.
    // Which leads to the saying "Call down, signal up". 
    // Look it up for more info, but basically if you want to invoke logic, one way is typically cleaner than the other. Due to the order in which nodes take their turns invoking a given method.
    // 

    // Hey down here! Once you're set here, let's head to StateMachineAnimatedSprite2DNode.cs
    // Maybe keep a tab of this file so you can reference back to it as we go though.
}
