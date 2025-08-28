// using Godot;

// public class IntroCryoTerminalInputInteractState : TerminalInputInteractState<IntroCryoChamberTerminal>
// {
//     const string PASSWORD = "password123";

//     // When we instantiate this class it takes the current dialogue and passes it to the TerminalInputInteractState through " : base(dialogue)"
//     // Meaning we call the base constructor logic in addition to what is defined here.
//     // The base constructor logic is executed before the constructor logic defined here.
//     public IntroCryoTerminalInputInteractState(TerminalInputEntry dialogue) : base(dialogue)
//     {
//     }

//     // The IntroCryoTerminal has its own logic it wants to run when a submit occurs.
//     // In this case, if the password provided is "objectorientedprogramming", then we open the door.
//     public override bool Submit(string playerInput)
//     {
//         if(playerInput == PASSWORD){
//             Agent.IntroDoor.Unlock();
//             return true;
//         }
//         return false;
//     }
// }