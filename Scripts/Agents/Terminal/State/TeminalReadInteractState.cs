using Deprecated.States;
using Godot;
using System;
using System.Linq;
using System.Text;
public class TerminalReadInteractState<T> : State<T>
    where T : Terminal
{
    private const double delay = 0.05;
    private int currentCharacterIndex = 0;
    private double deltaTotal = 0;
    private int totalCharacters;
    private StringBuilder writtenOutput;
    private TerminalOutputEntry _output;
    private string outputText;

    public TerminalReadInteractState(TerminalOutputEntry output){
        _output = output;
        outputText = _output.ToString();
        writtenOutput = new StringBuilder();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        totalCharacters = outputText.Length;
        Agent.Background.Visible = true;
    }

    public override void OnExit(StateBase nextState = null)
    {
        base.OnExit(nextState);
        Agent.TerminalTextLabel.Text += "\n";
    }

    public override void OnProcess(double delta)
    {
        base.OnProcess(delta);
        if(currentCharacterIndex == totalCharacters){
            TerminalEntry nextEntry = _output.NextEntry;
            if (nextEntry != null)
            {
                if (nextEntry is TerminalInputEntry nextInputEntry)
                {
                    Agent.SetState(Agent.GetInputState<T>(nextInputEntry));
                }
                else if (nextEntry is TerminalOutputEntry nextOutputEntry)
                {
                    Agent.SetState(Agent.GetOutputState<T>(nextOutputEntry));
                }
                else
                {
                    throw new NotImplementedException($"TerminalEntry of type {nextEntry.GetType()} has no corresponding terminal state!");
                }
            }
            else
            {
                Agent.SetState(Agent.GetIdleState<T>());
            }
        }
        else if(deltaTotal >= delay)
        {
            writtenOutput.Append(outputText[currentCharacterIndex]);
            currentCharacterIndex++;
            deltaTotal = 0;
        }
        deltaTotal += delta;
        Agent.TerminalTextLabel.Text = writtenOutput.ToString();
    }

}