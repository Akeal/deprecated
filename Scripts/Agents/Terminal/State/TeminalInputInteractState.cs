using Deprecated.Interaction;
using Deprecated.States;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// An abstract class is one that CANNOT be instantiated.
// That means you CAN'T have an object that is directly of type TerminalInputInteractState.
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
public class TerminalInputInteractState<T> : State<T>
    where T : Terminal
{
    private const double delay = 0.25;
    private const string INPUT_NEXT_CHARACTER = "_";
    private const string NEWLINE = "\n", TAB = "    ", SPACE = " ";
    private double deltaTotal = 0;
    private TerminalInputEntry _inputEntry;
    private bool displayingNextInputCharacter = true;
    private bool shiftOn = false;
    private bool capsOn = false;

    private bool useCaps {
        get{
            return shiftOn || capsOn;
        }
    }

    public TerminalInputInteractState(TerminalInputEntry terminalInputEntry)
    {
        _inputEntry = terminalInputEntry;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        //_dialogue.Response = string.Empty;
        // displayText = new StringBuilder($"{_inputEntry}{INPUT_LINE_BEGIN}");
        // writtenResponse = new StringBuilder(INPUT_NEXT_CHARACTER);
    }

    public override void OnProcess(double delta)
    {
        base.OnProcess(delta);
        GD.Print("hmm1");
        if(deltaTotal >= delay)
        {
            ToggleNextInputCharacter();
            deltaTotal = 0;
        }
        deltaTotal += delta;
        string underscore = displayingNextInputCharacter ? INPUT_NEXT_CHARACTER : string.Empty;
        Agent.TerminalTextLabel.Text = $"{_inputEntry}{underscore}";
    }

    public override void OnInputKey(InputEventKey @event)
    {
        base.OnInputKey(@event);
        if(@event.Keycode == Key.Shift)
        {
            shiftOn = @event.Pressed;
        }
        else if(@event.Pressed)
        {
            string character = GetKey(@event.Keycode);
            if(character != null){
                _inputEntry.Append(character);
            }
        }
    }

    // public string GetInput(){
    //     if(displayingNextInputCharacter){
    //         RemoveLastCharacter();
    //     }
    //     return writtenResponse.ToString();
    // }

    private void ToggleNextInputCharacter(){
        displayingNextInputCharacter = !displayingNextInputCharacter;
    }

    // private void AppendCharacter(string inputChar){
    //     if(displayingNextInputCharacter){
    //         RemoveLastCharacter();
    //         displayingNextInputCharacter = false;
    //     }

    //     writtenResponse.Append(inputChar);

    //     ToggleNextInputCharacter();
    // }

    // private void Backspace(){
    //     if(displayingNextInputCharacter){
    //         RemoveLastCharacter();
    //         displayingNextInputCharacter = false;
    //     }

    //     if(writtenResponse.Length > 0){
    //         RemoveLastCharacter();
    //     }

    //     ToggleNextInputCharacter();
    // }

    // private void RemoveLastCharacter(){
    //     if (writtenResponse.Length >= 1)
    //     {
    //         writtenResponse.Remove(writtenResponse.Length - 1, 1);
    //     }
    // }

    private void Exit(State<T> nextState){
        // AppendCharacter(NEWLINE);
        Agent.SetState(nextState);
        IInteractor interactor = Agent.interactors.First();
        Agent.interactors.Remove(interactor);
        Agent.AfterInteraction(interactor);
        interactor.OnInteract(Agent, DuringEnum.Idle);
    }

    // Not all keys are implemented. We'll add as we go.
    private string GetKey(Key keycode)
    {
        switch(keycode){
            case Key.Enter:
                if(shiftOn){
                    return NEWLINE; // If we're holding shift, enter write a newline to the terminal
                }
                else{
                    // If we're not holding shift we execute the submit logic for this terminal.
                    return HandleSubmit();
                }
            case Key.Backspace:
                HandleBackspace();
                break;
            case Key.Space:
                return SPACE;
            case Key.Tab:
                return TAB;
            case Key.Escape:
                HandleEscape();
                break;
            default:
                return HandleNewCharacter(keycode);
        }
        return null;
    }

    private string HandleNewCharacter(Key keycode){
        string keycodeString = OS.GetKeycodeString(keycode);
        if(keycodeString.Length == 1){
            return shiftOn ? keycodeString : keycodeString.ToLower();
        }
        return null;
    }

    private void HandleEscape(){
        Exit(new TerminalIdleState<T>());
    }

    private void HandleBackspace(){
        _inputEntry.Backspace();
    }

    private string HandleSubmit(){
        TerminalEntry nextEntry = _inputEntry.Submit();
        if(nextEntry != null){
            if(nextEntry is TerminalInputEntry terminalInput){
                Exit(Agent.GetInputState<T>(terminalInput));
            }
            else if(nextEntry is TerminalOutputEntry terminalOutput){
                Exit(Agent.GetOutputState<T>(terminalOutput));
            }
        }
        // bool success = Submit(GetInput());
        // if(success)
        // {
        //     Exit(new TerminalIdleState<T>());
        //     return null;
        // }
        // AppendCharacter(NEWLINE);
        Exit(Agent.GetInputState<T>(_inputEntry));
        // Exit(Agent.CurrentState() as State<T>);
        return null;
    }

    // This Submit() method being abstract means that when we submit the terminal input from the user we don't know what to do!
    // Here at least.
    // The class that inherits from TerminalInputInteractState will be required to override Submit() and provide its own logic to execute.
    // This allows us to use all the same Terminal input logic for that class but handle input from the user differently from one to another.
    // Last minute changes made here. Virtual not abstract
    public virtual bool Submit(string playerInput) { throw new NotImplementedException(); }
}