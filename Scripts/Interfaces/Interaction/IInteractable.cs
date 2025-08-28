using Godot;
using Deprecated.States;
using System.Collections.Generic;

namespace Deprecated.Interaction
{
    // Here we have two enums. They represent how the 
    public enum DuringEnum {
        Wait,
        Idle
    }
    public enum AfterEnum {
        Idle
    }

    // IInteractable is an interface that defines that the object can be interacted with by IInteractor objects.
    public interface IInteractable {
        // What Interactors are currently interacting with this object that can be interacted with?
        public List<IInteractor> interactors { get; }

        // Method on the interactable thing that gets called when an interactor interacts with it
        public void OnInteraction (IInteractor interactingNode);

        // Method on the interactable thing that gets called when an interactor stops interacting with it
        public void AfterInteraction(IInteractor interactingNode);
    }

        /*
                 Person : IInteractor    Car..? : IInteractable
                (o_o)~hello              o__o>
                -OnInteracting-         -OnInteraction-
                 EnterThing()            TurnOnEngine()
                -AfterInteracting-      -AfterInteraction-
                 ExitThing()             TurnOffEngine()
            */
}