using Deprecated.Interaction;
using Godot;

namespace Deprecated.States {
    // An abstract class is one that CANNOT be instantiated.
    // That means you CAN'T have an object that is directly of type State.
    // But there's so much logic in here! What's the point of all of this if we can't use it?
    // Well we can use it, just not directly.
    // For us to use it, we need ANOTHER class to inherit from it. That class, if it itself is not also abstract, can be instantiated.
    // Then that object instance can use all of this logic defined here. It's a way to neatly define that all of this logic is intended to be the "core" functionality of whatever inherits from this.
    // On top of that, there are two property and method modifiers to help us out. Both properties (getters and setters) and methods can be marked as either -abstract- or -virtual-

    // A method that is marked -abstract- states "I don't know what I should do when this method is called but the thing that inherits from me will define what to do."
    // A property getter/setter that is marked -abstract- states "I don't know where this value comes from yet but the thing that inherits from me will."

    // A method that is marked -virtual- DOES have an implementation on the base, in this case abstract, class.
    // This means that something that inherits from this class DOESN'T have to implement it, like it would have to implement an abstract method.
    // However, the virtual flag enables us to override the base implementation.
    // This means that if the method is called on an object that inherits from this class, if there is no overridden implementation, the base logic will be called.
    // If the virtual method IS overridden on the class that inherits from this class, then the overridden logic will execute INSTEAD of the base logic.
    // If the method is overridden and you need to call BOTH the base logic and the overridden logic, the base logic can be called from the overridden logic with the base.MyMethodName() syntax.
    // Having a virtual method be overridden but still call the base logic allows us to EXTEND the functionality of the base class to include additional logic on top of the base, reducing code duplication.
    // This in turn allows us to structure our classes in a way that each time we need functionality for a given method to branch from the norm, we can override and call the base.
    // Put another way if every class that inherits from the Holdable class can be held by the player, but the player can hold many different kinds of things, 
    // each of those things can inherit from Holdable and focus on what the thing itself does while either not being held or while being held.
    // A property getter/setter that is marked -virtual- DOES have an implementation on the base, in this case abstract, class. 
    // It's the same principle as virtual methods but rather than being about what the virtual method does, it's about defining where the virtual property is populated from. Or at least that's how I use them typically.
    public abstract class State<T> : StateBase
    {
        private T _agent;
        public T Agent {
            get{
                return _agent;
            }
        }

        public void SetAgent(T agent){
            this._agent = agent;
        }

        public State(){}
    }


}