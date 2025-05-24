using System;

public class PlayerOrderResolver
{
    public bool DetermineFirstPlayer()
    {
        Console.WriteLine("\nLet's determine who makes the first move.");

        var protocol = new FairRandomProtocol(2);
        protocol.PrepareComputerMove();

        Console.WriteLine($"I selected a random value in the range 0..1 (HMAC={protocol.Hmac})");

        int userGuess = -1;
        while (true)
        {
            Console.Write("Your selection (0 or 1, X = exit, ? = help): ");
            var input = Console.ReadLine()?.Trim();

            if (input == "?")
            {
                Console.WriteLine("Guess 0 or 1. If you match my secret number, you choose dice first.");
                continue;
            }
            if (input == "X") Environment.Exit(0);
            if (int.TryParse(input, out int g) && (g == 0 || g == 1))
            {
                userGuess = g;
                break;
            }

            Console.WriteLine("Invalid input.");
        }

        Console.WriteLine($"My selection: {protocol.ComputerNumber} (KEY={CryptoUtils.BytesToHex(protocol.SecretKey)})");

        if (userGuess == protocol.ComputerNumber)
        {
            Console.WriteLine("You guessed right! You go first.");
            return true;
        }
        else
        {
            Console.WriteLine("I go first.");
            return false;
        }
    }
}

