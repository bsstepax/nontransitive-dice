using System;
using System.Collections.Generic;

public class DiceGame
{
    private List<Dice> diceList;

    public void Start(string[] args)
    {
        try
        {
            diceList = DiceParser.ParseArguments(args);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("Input Error: " + ex.Message);
            return;
        }

        Console.WriteLine("Welcome to the Non-Transitive Dice Game!");

        var orderResolver = new PlayerOrderResolver();
        bool userGoesFirst = orderResolver.DetermineFirstPlayer();

        var selector = new DiceSelector(diceList);
        (int userIndex, int compIndex) = selector.SelectDice(userGoesFirst);

        var roller = new DiceRoller();
        PlayRound(userIndex, compIndex, roller);
    }

    private void PlayRound(int userIndex, int compIndex, DiceRoller roller)
    {
        var userDice = diceList[userIndex];
        var compDice = diceList[compIndex];

        int compValue = roller.Roll("my", compDice);
        int userValue = roller.Roll("your", userDice);

        if (userValue > compValue)
            Console.WriteLine("You win!");
        else if (userValue < compValue)
            Console.WriteLine("You lose :(");
        else
            Console.WriteLine("It's a draw!");
    }
}


