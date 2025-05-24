using System;
using System.Collections.Generic;

public class DiceSelector
{
    private readonly List<Dice> diceList;

    public DiceSelector(List<Dice> diceList)
    {
        this.diceList = diceList ?? throw new ArgumentNullException(nameof(diceList));
    }

    public (int userIndex, int compIndex) SelectDice(bool userFirst)
    {
        int userIndex, compIndex;

        if (userFirst)
        {
            userIndex = AskDiceSelection("Choose your dice:", null);
            compIndex = FirstAvailableIndexExcept(userIndex);
            Console.WriteLine($"I choose the dice: {diceList[compIndex]}");
        }
        else
        {
            compIndex = 0;
            Console.WriteLine($"I choose the dice: {diceList[compIndex]}");
            userIndex = AskDiceSelection("Choose your dice (not mine):", compIndex);
        }

        return (userIndex, compIndex);
    }

    private int AskDiceSelection(string prompt, int? forbiddenIndex)
    {
        Console.WriteLine("Type '?' to show the probability table.");
        while (true)
        {
            Console.WriteLine("\n" + prompt);
            for (int i = 0; i < diceList.Count; i++)
            {
                if (i != forbiddenIndex)
                    Console.WriteLine($"{i} - {diceList[i]}");
            }

            Console.Write("Your selection: ");
            string? input = Console.ReadLine()?.Trim();

            if (input == "X") Environment.Exit(0);
            if (input == "?")
            {
                TableRenderer.ShowProbabilityTable(diceList);
                continue;
            }

            if (int.TryParse(input, out int index) &&
                index >= 0 &&
                index < diceList.Count &&
                index != forbiddenIndex)
            {
                Console.WriteLine($"You selected: {diceList[index]}");
                return index;
            }

            Console.WriteLine("Invalid input.");
        }
    }

    private int FirstAvailableIndexExcept(int excluded)
    {
        for (int i = 0; i < diceList.Count; i++)
            if (i != excluded) return i;

        throw new InvalidOperationException("No alternative dice available.");
    }
}

