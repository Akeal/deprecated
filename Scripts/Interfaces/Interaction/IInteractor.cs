using Godot;

namespace Deprecated.Interaction
{
    public interface IInteractor {
        public void OnInteract (IInteractable interactableNode, DuringEnum during);

        /*
                Person : IInteractor    Car..? : IInteractable
            (o_o)~hello              o__o>
            -OnInteracting-         -OnInteraction-
                EnterThing()            TurnOnEngine()
            -AfterInteracting-      -AfterInteraction-
                ExitThing()             TurnOffEngine()
        */
    }
}