using Godot;
using Deprecated.States;
using System.Collections.Generic;

namespace Deprecated.Gate
{
    // IKey is an interface that defines that the object can be used to "unlock" something else that implements the ILock interface
    public interface IKey<K, L> 
        where K : Node 
        where L : Node {
        // A property that enforces that something that implements the IKey interface has a reference to the thing it is a key for.
        public ILock<L> Lock { get; }
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