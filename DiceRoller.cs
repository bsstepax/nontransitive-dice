using System;

public class DiceRoller
{
    public int Roll(string playerName, Dice dice)
    {
        Console.WriteLine($"\nIt's time for {playerName}'s roll.");

        var protocol = new FairRandomProtocol(dice.Faces.Count);
        protocol.PrepareComputerMove();

        Console.WriteLine($"I selected a random value in the range 0..{dice.Faces.Count - 1} (HMAC={protocol.Hmac})");

        int userInput = protocol.GetUserInput();
        int index = protocol.FinalizeResult(userInput);
        int value = dice.Roll(index);

        Console.WriteLine($"{playerName}'s roll result: {value}");
        return value;
    }
}

