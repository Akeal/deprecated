// using System;
// using System.Collections.Generic;
// using System.Text;

// // TerminalEntries represents ALL the entries currently displayed in the terminal
// // It allows us to separate the collection of entries from the terminal itself.
// // This helps us to separate our concerns into "things the terminal does" and "things the terminal holds"
// // Most terminals behave the same way but "contain" different text elements
// public class TerminalEntries
// {
//     private List<TerminalEntry> _entries;
//     public TerminalEntries(){}

//     public override string ToString()
//     {
//         StringBuilder stringBuilder = new StringBuilder();
//         foreach(TerminalEntry entry in _entries){
//             stringBuilder.AppendLine(entry.ToString());
//         }
//         return stringBuilder.ToString();
//     }
// }