using Deprecated.Interaction;
using Deprecated.States;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
public class TerminalProcessInputState<T> : State<T> 
    where T : Terminal {
    string _input;
    public TerminalProcessInputState(string input)
    {
        _input = input;
    }
}