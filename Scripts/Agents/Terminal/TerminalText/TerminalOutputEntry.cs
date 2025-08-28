// TerminalEntry represents one block of text, of many, that we want to display in TerminalText
public class TerminalOutputEntry : TerminalEntry
{
    private readonly string _baseText;
    private readonly TerminalEntrySubstitution[] _terminalEntrySubstitutions;
    private readonly TerminalEntry _nextEntry;
    public TerminalEntry NextEntry { 
        get{
            return _nextEntry; 
        }
    }

    public TerminalOutputEntry(string baseText, TerminalEntry nextEntry, params TerminalEntrySubstitution[] terminalEntrySubstitutions)
    {
        _baseText = baseText;
        _terminalEntrySubstitutions = terminalEntrySubstitutions;
        _nextEntry = nextEntry;
    }

    public override string ToString()
    {
        string substitutedText = _baseText;
        foreach(TerminalEntrySubstitution keywordSubstitution in _terminalEntrySubstitutions){
            substitutedText = keywordSubstitution.GetSubstitutedText(substitutedText);
        }
        return substitutedText;
    }
}