using ConsoleTables;
using System;

public static class TableRenderer
{
    public static void ShowProbabilityTable(List<Dice> diceList)
    {
        Console.WriteLine("\nProbability of the win for the user:");
       
        var headers = new List<string> { "User vs" };
        headers.AddRange(diceList.Select(d => d.ToString()));

        var table = new ConsoleTable(headers.ToArray());

        for (int i = 0; i < diceList.Count; i++)
        {
            var row = new List<string> { diceList[i].ToString() };

            for (int j = 0; j < diceList.Count; j++)
            {
                if (i == j)
                {
                    row.Add("-"); 
                }
                else
                {
                    double p = ProbabilityCalculator.CalculateWinProbability(diceList[i], diceList[j]);
                    row.Add(p.ToString("0.0000"));
                }
            }

            table.AddRow(row.ToArray());
        }

        table.Write(Format.Minimal);
        Console.WriteLine();
    }
}

