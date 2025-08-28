using Godot;
using Deprecated.Interaction;
using Deprecated.States;
using System.Collections.Generic;

public abstract partial class Terminal : Area2D, IInteractable
{
	[Export]
	public TextureRect Background;
	[Export]
	public RichTextLabel TerminalTextLabel;

    // public TerminalEntries TerminalText = new TerminalEntries();

    private List<IInteractor> _interactors = new List<IInteractor>();
    public List<IInteractor> interactors { get { return _interactors; } }

	// This is the _Process() function
	// It's a godot node event hook that occurs ONCE per rendered frame.
	public override void _Process(double delta)
	{
		base._Process(delta);
        // TerminalTextLabel.Text = TerminalText.ToString();
        this.CurrentState().OnProcess(delta);
	}

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
		if (@event is InputEventKey inputEventKey)
		{
			this.CurrentState().OnInputKey(inputEventKey);
		}
    }

    public void OnInteraction(IInteractor interactingNode)
    {
		if(this.CurrentState() is IInteractable interactableState){
			interactors.Add(interactingNode);
			interactableState.OnInteraction(interactingNode);
		}
    }

    public void AfterInteraction(IInteractor interactingNode)
    {
		if(this.CurrentState() is IInteractable interactableState){
			interactableState.AfterInteraction(interactingNode);
		}
    }

	// The below declares that every class that inherits from Terminal must have their own respective versions of these states.
	// These methods return the state class of T where T is a class that inherits from Terminal.
	// This allows us to set the state of the Terminal to the returned State<T>
	public abstract TerminalIdleState<T> GetIdleState<T>() where T : Terminal;
	public abstract TerminalReadInteractState<T> GetOutputState<T>(TerminalOutputEntry outputEntry) where T : Terminal;
	public abstract TerminalInputInteractState<T> GetInputState<T>(TerminalInputEntry inputEntry) where T : Terminal;
    public abstract TerminalOutputEntry GetTree();
}
