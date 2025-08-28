using Deprecated.Gate;
using Deprecated.States;
using Godot;

// This is the terminal you see in the room that you start in
// It inherits functionality from the Terminal class
// That means it can do everything a Terminal can do AND it can do whatever else we define here, in addition.
// This allows us reuse the Terminal class for each terminal while defining custom logic in cases we need it.
public partial class IntroCryoChamberTerminal : Terminal, IKey<Terminal, Door>
{
	[Export]
	public Door IntroDoor;

	public ILock<Door> Lock { 
		get { 
			return IntroDoor; 
		}
	}

	public override void _Ready()
	{
		base._Ready();
		this.SetState(GetIdleState<IntroCryoChamberTerminal>());
	}

	public override TerminalIdleState<T> GetIdleState<T>()
	{
		return new TerminalIdleState<T>();
	}

	public override TerminalInputInteractState<T> GetInputState<T>(TerminalInputEntry input)
	{
		return new TerminalInputInteractState<T>(input);
	}

	public override TerminalReadInteractState<T> GetOutputState<T>(TerminalOutputEntry output)
	{
		return new TerminalReadInteractState<T>(output);
	}

    public override TerminalOutputEntry GetTree()
    {
        return IntroCryoChamberTerminalTree.GetRoot();
    }
}
