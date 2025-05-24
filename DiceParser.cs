using System.Globalization;

public static class DiceParser
{
    public static List<Dice> ParseArguments(string[] args)
    {
        if (args.Length < 3)
        {
            throw new ArgumentException(
                "You must provide at least 3 dice. Example: 2,2,4,4,9,9 6,8,1,1,8,6 7,5,3,7,5,3");
        }

        var diceList = new List<Dice>();

        for (int i = 0; i < args.Length; i++)
        {
            string arg = args[i];

            if (string.IsNullOrWhiteSpace(arg))
            {
                throw new ArgumentException(
                    $"Argument {i + 1} is empty. Example: 2,2,4,4,9,9");
            }

            var faceStrings = arg.Split(',');

            var faces = new List<int>();

            foreach (var faceStr in faceStrings)
            {
                if (!int.TryParse(faceStr, NumberStyles.Integer, CultureInfo.InvariantCulture, out int face))
                {
                    throw new ArgumentException(
                        $"Invalid value \"{faceStr}\" in argument {i + 1}. Only INTEGERS are allowed. Example: 1,3,5,7,9,11");
                }

                faces.Add(face);
            }

            try
            {
                diceList.Add(new Dice(faces));
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(
                    $"Error in dice {i + 1}: {ex.Message}");
            }
        }

        return diceList;
    }
}

