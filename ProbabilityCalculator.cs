public static class ProbabilityCalculator
{
    public static double CalculateWinProbability(Dice diceA, Dice diceB)
    {
        int wins = 0;
        int total = 0;

        foreach (var a in diceA.Faces)
        {
            foreach (var b in diceB.Faces)
            {
                if (a > b) wins++;
                total++;
            }
        }

        return total > 0 ? (double)wins / total : 0;
    }
}

