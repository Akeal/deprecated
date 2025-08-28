// TerminalEntrySubstitution represents a keyword within a TerminalEntry that we want to change the value of depending on the current state of the game.
using System;

public class TerminalEntrySubstitution
{
    private readonly string _keyword;
    private readonly Func<string> _substitutionMethod;
    public TerminalEntrySubstitution(string keyword, Func<string> substitutionMethod)
    {
        // We're defining our own rule around how a keyword appears before substitution.
        // What this means is that the keyword, before substition, would be "{tomato}" not "tomato"
        // This prevents unwanted substitution if the keyword also exists in the base text but we want to display it normally.
        // Imagine substitution applied to: "I would be sad if someone replaced my tomato, but I would not be sad if someone replaced my {tomato}."
        _keyword = "{" + keyword + "}";
        _substitutionMethod = substitutionMethod;
    }

    public string GetSubstitutedText(string baseText)
    {
        // The _substitutionMethod is a reference to the method that returns the string we want to substitute the keyword with.
        // So here we invoke the method to get the string value.
        string currentSubstitutionValue = _substitutionMethod.Invoke();
        // Now replace the keyword with the string value.
        return baseText.Replace(_keyword, currentSubstitutionValue);
    }
}