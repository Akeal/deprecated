// Terminal dialogue trees need to be held -somewhere-
// I'm making the decision for it to live in code rather than exported variables so that the player can navigate it.

using System;
using System.Collections.Generic;

public class IntroCryoChamberTerminalTree
{
    public static TerminalOutputEntry GetRoot()
    {
        TerminalOutputEntry greeting = new TerminalOutputEntry("Welcome to the station.\nPlease enter the password.", GetCommands());
        return greeting;
    }

    private static TerminalInputEntry GetCommands(){
        Dictionary<string, Func<TerminalEntry>> commands = new Dictionary<string, Func<TerminalEntry>>()
        {
            {"password123", () => { return new TerminalOutputEntry($"Correct!", null); }}
        };
        return new TerminalInputEntry(commands);
    }
}