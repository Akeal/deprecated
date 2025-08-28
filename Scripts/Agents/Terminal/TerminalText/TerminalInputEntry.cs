using System;
using System.Collections.Generic;

public class TerminalInputEntry : TerminalEntry {

    private const string INPUT_LINE_BEGIN = ">";
    private string userInput;
    // This is the collection of string commands that can be entered at this input.
    // We determine which path we will branch down here.
    private Dictionary<string, Func<TerminalEntry>> _commands;
    public TerminalInputEntry(Dictionary<string, Func<TerminalEntry>> commands)
    {
        _commands = commands;
    }

    public virtual void Append(string str){
        userInput += str;
    }

    public virtual void Backspace(){
        if (userInput.Length >= 1)
        {
            userInput.Remove(userInput.Length - 1, 1);
        }
    }

    public virtual TerminalEntry Submit(){
        if(_commands.ContainsKey(userInput)){
            TerminalEntry nextEntry = _commands[userInput].Invoke();
            return nextEntry;
        }
        return null;
    }

    public override string ToString()
    {
        return $"{INPUT_LINE_BEGIN}{userInput}";
    }
}