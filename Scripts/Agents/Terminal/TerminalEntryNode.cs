// using Godot;
// using Godot.Collections;

// public partial class TerminalEntryNode : Node {
// 	[Export(PropertyHint.MultilineText)]
// 	public string Text;

//     [Export]
//     public bool WaitForInput = false;
//     public string Response;

//     private int currentChildIndex = 0;
//     private Array<TerminalEntryNode> childDialogueNodes = new Array<TerminalEntryNode>();

//     public override void _Ready()
//     {
//         base._Ready();
//         Array<Node> children = GetChildren();
//         foreach(Node child in children){
//             if(child is TerminalEntryNode childDialogue){
//                 childDialogueNodes.Add(childDialogue);
//             }
//         }
//     }

//     public TerminalEntryNode GetNext(){
//         return currentChildIndex >= childDialogueNodes.Count - 2 ? null : childDialogueNodes[currentChildIndex++];
//     }
// }
