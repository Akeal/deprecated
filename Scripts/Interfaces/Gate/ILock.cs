using Godot;
using Deprecated.States;
using System.Collections.Generic;

namespace Deprecated.Gate
{
    // ILock is an interface that defines that the object has both "locked" and "unlocked" states
    public interface ILock<L> where L : Node {

        // Method on the lock thing that gets called when we want to lock it
        public void Lock ();

        // Method on the lock thing that gets called when we want to unlock it
        public void Unlock();
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