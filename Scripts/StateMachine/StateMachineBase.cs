using Deprecated.Interaction;
using Godot;
using System;
using System.Collections.Generic;

namespace Deprecated.States {
    public abstract class StateMachineBase { 
		protected Stack<StateBase> stateStack = new Stack<StateBase>();
		public virtual StateBase CurrentState
		{
			get
			{
				if(stateStack.Count == 0){
					throw new Exception($"Please set a state.");
				}
				
				return stateStack.Peek();
			}
		}

        public void ChangeState<T>(StateBase nextState)
		{
			if(stateStack.TryPop(out StateBase priorState))
			{
				priorState.OnExit(stateStack.Count != 0 ? CurrentState : null);
			}
            
            stateStack.Push(nextState);
            SetAgent();
			nextState.OnEnter();
		}

        protected abstract void SetAgent();

        public void OnProcess(double delta)
		{
			CurrentState?.OnProcess(delta);
		}

		public void OnPhysicsProcess(double delta)
		{
			CurrentState?.OnPhysicsProcess(delta);
		}

		protected virtual void OnMouseEntered(){
			CurrentState?.OnMouseEntered();
		}

		protected virtual void OnMouseExited(){
			CurrentState?.OnMouseExited();
		}

		public virtual void OnInteract<InteractorType>(InteractorType interactingNode) where InteractorType : IInteractor{
			if(CurrentState is IInteractable interactableState){
				interactableState.OnInteraction(interactingNode);
			}
		}

		public void Input(InputEvent @event)
		{
			if(@event.GetType() == typeof(InputEventKey))
			{
				CurrentState?.OnInputKey(@event as InputEventKey);
			}
			else if(@event.GetType() == typeof(InputEventMouseButton)){
				CurrentState?.OnInputMouseButton(@event as InputEventMouseButton);
			}
			else if(@event.GetType() == typeof(InputEventMouseMotion)){
				CurrentState?.OnInputMouseMotion(@event as InputEventMouseMotion);
			}
			else{
				CurrentState?.OnInput(@event);
			}
			
		}

     }
}