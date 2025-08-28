using Godot;

namespace Deprecated.States {
	public class StateMachine<T> : StateMachineBase
		where T : Node
	{
		private T _agent;
		public T Agent
		{
			get
			{
				return _agent;
			}
		}

		public StateMachine(T node)
		{
			_agent = node;
		}

        protected override void SetAgent()
        {
			// This is where we bridge the gap from untyped state (StateBase) to typed state (State<T>).
			// It happens here because the only typed aspect of State<T> is that Agent is of type T.
            (CurrentState as State<T>).SetAgent(Agent);
        }

		// Maybe later
		// public void PushState(State<T> nextState)
		// {
		// 	if(stateStack.TryPop(out State<T> priorState))
		// 	{
		// 		priorState.OnExit(CurrentState);
		// 	}
		// 	nextState.SetAgent(Agent);
		// 	stateStack.Push(nextState);
		// 	nextState.OnEnter();
		// }

		// public State<T> PopState()
		// {
		// 	if(stateStack.TryPop(out State<T> priorState))
		// 	{
		// 		priorState.OnExit(CurrentState);
		// 	}
		// 	CurrentState.SetAgent(Agent);
		// 	CurrentState.OnEnter();
		// 	return CurrentState;
		// }



		public static implicit operator StateMachine<T>(StateMachine<Node> nodeStateMachine){
			return (StateMachine<T>)nodeStateMachine;
		}


    }
}
