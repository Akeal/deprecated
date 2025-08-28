using Deprecated.Interaction;
using Godot;
using System.Collections.Generic;

namespace Deprecated.States {
    public partial class StateBase {
        public virtual void OnEnter(){ }
        public virtual void OnExit(StateBase nextState = null){ }
        public virtual void OnProcess(double delta){ }
        public virtual void OnPhysicsProcess(double delta){}
        public virtual void OnMouseEntered(){}
        public virtual void OnMouseExited(){}
        public virtual void OnInput(InputEvent @event){}
        public virtual void OnInputKey(InputEventKey @event){}
        public virtual void OnInputMouseButton(InputEventMouseButton @event){}
        public virtual void OnInputMouseMotion(InputEventMouseMotion @event){}

     }

        // Here we have more custom state hooks for when the state machine belongs to a node that can either...
    // Interact with interactable nodes
    // Be interacted with by a node that can interact with interactable nodes
    namespace Interaction {
        public partial class StateBase{
            // IInteractor is an interface that defines that the object can interact with IInteractable objects.
            // "Does the thing have logic to run when something starts or stops interacting with it?"
            public virtual void OnInteracting(IInteractor interactingThing) { }
            public virtual void AfterInteracting(IInteractor interactingThing) { }

            // IInteractable is an interface that defines that the object can be interacted with by IInteractor objects.
            // "Does a thing that can interact with other interactable things have logic to run when it starts or stops interacting with that thing?"
            public virtual void OnInteraction(IInteractable interactedThing) { }
            public virtual void AfterInteraction(IInteractable interactedThing) { }


        /*
                 Person : IInteractor    Car..? : IInteractable
                  (o_o) hello                 o__o>
                -OnInteracting-         -OnInteraction-
                 EnterThing()            TurnOnEngine()
                -AfterInteracting-      -AfterInteraction-
                 ExitThing()             TurnOffEngine()
            */
        }
    }
}