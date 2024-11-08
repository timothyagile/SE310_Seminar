using System;

public class Fan
{
    public void TurnOn()
    {
        Console.WriteLine("Fan is turned on.");
    }

    public void TurnOff()
    {
        Console.WriteLine("Fan is turned off.");
    }
}

public interface ICommand
{
    void Execute();
    void Undo();
}

class Remote
{
    private readonly ICommand turnOnCommand;
    private readonly ICommand turnOffCommand;

    public Remote(ICommand turnOnCommand, ICommand turnOffCommand)
    {
        this.turnOnCommand = turnOnCommand;
        this.turnOffCommand = turnOffCommand;
    }

    public void TurnOnButtonClick()
    {
        turnOnCommand.Execute();
    }

    public void TurnOffButtonClick()
    {
        turnOffCommand.Execute();
    }

    public void UndoTurnOn()
    {
        turnOnCommand.Undo();
    }

    public void UndoTurnOff()
    {
        turnOffCommand.Undo();
    }
}

class TurnOffCommand : ICommand
{
    private readonly Fan fan;

    public TurnOffCommand(Fan fan)
    {
        this.fan = fan;
    }

    public void Execute()
    {
        fan.TurnOff();
    }

    public void Undo()
    {
        fan.TurnOn();
    }
}

class TurnOnCommand : ICommand
{
    private readonly Fan fan;

    public TurnOnCommand(Fan fan)
    {
        this.fan = fan;
    }

    public void Execute()
    {
        fan.TurnOn();
    }

    public void Undo()
    {
        fan.TurnOff();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Fan fan = new Fan();
        ICommand turnOnCommand = new TurnOnCommand(fan);
        ICommand turnOffCommand = new TurnOffCommand(fan);
        
        Remote remote = new Remote(turnOnCommand, turnOffCommand);

        // Turn on the fan
        remote.TurnOnButtonClick();
        
        // Turn off the fan
        remote.TurnOffButtonClick();

        // Undo actions
        remote.UndoTurnOff();  // Should turn the fan back on
        remote.UndoTurnOn();   // Should turn the fan back off
    }
}