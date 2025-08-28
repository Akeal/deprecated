using System.Net;
using System.Runtime.CompilerServices;
using Godot;

namespace Deprecated.States
{
    public static class StateNodeExtensions
    {
        const string STATE_MACHINE_STORAGE_PATH = "/root/StateMachineStorage";

        public static void ReparentStateMachineNode<T>(this T node, Node newParentNode) where T : Node{
            StateMachineStorage globalStateMachineStorage = node.GetNode<StateMachineStorage>(STATE_MACHINE_STORAGE_PATH);
            globalStateMachineStorage.ReparentStateMachineNode(node, newParentNode);
        }

        public static StateMachineBase StateMachine<T>(this T node) where T : Node {
            StateMachineStorage globalStateMachineStorage = node.GetNode<StateMachineStorage>(STATE_MACHINE_STORAGE_PATH);
            StateMachineBase stateMachineBase = globalStateMachineStorage.GetNodeStateMachine<T>(node);
            return stateMachineBase;
        }

        public static StateBase CurrentState<T>(this T node) where T : Node {
            return StateMachine(node).CurrentState;
        }

        public static void SetState<T>(this T node, State<T> state) where T : Node {
            StateMachine(node).ChangeState<T>(state);
        }
    }
}