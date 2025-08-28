// C# 13 doesn't quite yet support extension properties so we need to have a global singleton hold references to the state machines 
// and have an extension getter/setter access it
// Supposedly when C#14 comes out we should be able to delete this code and move each node's StateMachine into an extension property.
using System.Collections.Generic;
using Deprecated.States;
using Godot;
using System;

public partial class StateMachineStorage : Node {
    private Dictionary<ulong, StateMachineBase> stateMachines = new Dictionary<ulong, StateMachineBase>();
    public StateMachineBase GetNodeStateMachine<T>(T node)
        where T : Node {
        ulong nodeId = node.GetInstanceId();
        if(!stateMachines.ContainsKey(nodeId))
        {
            node.Connect(Node.SignalName.TreeExited, Callable.From(() => { RemoveStateMachine(nodeId); }));
            stateMachines.Add(nodeId, new StateMachine<T>(node));
        }
        return stateMachines[nodeId];
    }

    // It seems that when a node is reparented it gets a new instance id
    // So we'll use this method for our reparenting purposes to update the state machine storage as well
    // Again... wouldn't need this in C#14, probably
    public void ReparentStateMachineNode<T>(T node, Node newParentNode)
        where T : Node {
        ulong oldNodeId = node.GetInstanceId();
        StateMachineBase nodeStateMachine = stateMachines[oldNodeId];
        node.Reparent(newParentNode); // Reparent triggers Node.SignalName.TreeExited
        ulong newNodeId = node.GetInstanceId();
        
        stateMachines.Add(newNodeId, nodeStateMachine);
        node.Connect(Node.SignalName.TreeExited, Callable.From(() => { RemoveStateMachine(newNodeId); }));
    }

    private void RemoveStateMachine(ulong nodeId){
        if(stateMachines.ContainsKey(nodeId)){
            stateMachines.Remove(nodeId);
        }
    }
}