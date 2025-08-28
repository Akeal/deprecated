// // TerminalEntry represents one block of text, of many, that we want to display in TerminalText
// using System;
// using System.Collections.Generic;
// using Godot;

// public class TerminalInputEntryConfirmation : TerminalInputEntry
// {

//     public TerminalInputEntryConfirmation() : base(null)
//     {

//     }

//     private string HandleSubmit(){
//         TerminalEntry nextEntry = _inputEntry.Submit();
//         if(nextEntry != null){
//             if(nextEntry is TerminalInputEntry terminalInput){
//                 Exit(Agent.GetInputState<T>(terminalInput));
//             }
//             else if(nextEntry is TerminalOutputEntry terminalOutput){
//                 Exit(Agent.GetOutputState<T>(terminalOutput));
//             }
//         }
//         // bool success = Submit(GetInput());
//         // if(success)
//         // {
//         //     Exit(new TerminalIdleState<T>());
//         //     return null;
//         // }
//         // AppendCharacter(NEWLINE);
//         Exit(Agent.GetInputState<T>(_inputEntry));
//         // Exit(Agent.CurrentState() as State<T>);
//         return null;
//     } 

//     public override void Append(string str)
//     {
//         base.Append(str);
//     }

//     public override void Backspace()
//     {
//         base.Backspace();
//     }

//     public override TerminalEntry Submit()
//     {
//         return base.Submit();
//     }
// }