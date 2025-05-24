using System;

public class FairRandomProtocol
{
    public int MaxValue { get; }
    public int ComputerNumber { get; private set; }
    public byte[] SecretKey { get; private set; }
    public string Hmac { get; private set; }

    public FairRandomProtocol(int maxValue)
    {
        if (maxValue <= 0)
            throw new ArgumentException("Max value must be greater than 0.");

        MaxValue = maxValue;
    }

    public void PrepareComputerMove()
    {
        ComputerNumber = CryptoUtils.GenerateSecureInt(MaxValue);
        SecretKey = CryptoUtils.GenerateRandomKey(32); // 256 бит
        Hmac = CryptoUtils.ComputeHMAC_SHA3(SecretKey, ComputerNumber);
    }

    public int GetUserInput()
    {
        while (true)
        {
            Console.WriteLine($"Add your number modulo {MaxValue}:");

            for (int i = 0; i < MaxValue; i++)
                Console.WriteLine($"{i} - {i}");

            Console.WriteLine("X - exit");
            Console.Write("? - help\n");

            Console.Write("Your selection: ");
            string? input = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Please enter a value.");
                continue;
            }

            if (input.Equals("X", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Exiting.");
                Environment.Exit(0);
            }

            if (input == "?")
            {
                Console.WriteLine("Select a number from 0 to " + (MaxValue - 1) + " to complete the fair random protocol.");
                continue;
            }

            if (int.TryParse(input, out int userValue) && userValue >= 0 && userValue < MaxValue)
            {
                return userValue;
            }

            Console.WriteLine("Invalid input. Try again.");
        }
    }

    public int FinalizeResult(int userValue)
    {
        int result = (ComputerNumber + userValue) % MaxValue;

        Console.WriteLine($"My number is {ComputerNumber} (KEY={CryptoUtils.BytesToHex(SecretKey)})");
        Console.WriteLine($"The fair number generation result is {ComputerNumber} + {userValue} = {result} (mod {MaxValue})");

        return result;
    }

    public void RunProtocol()
    {
        PrepareComputerMove();
        Console.WriteLine($"I selected a random value in the range 0..{MaxValue - 1} (HMAC={Hmac})");

        int userValue = GetUserInput();
        FinalizeResult(userValue);
    }
}
