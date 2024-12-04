namespace Advent24.Day1;

public static class HistorianHysteria
{
    // private const string FilePath = "input.txt";
    private static readonly string FilePath = Path.Combine(Environment.CurrentDirectory, "Day1/input.txt");

    public static int Solve()
    {
        var lists = Parse();
        
        var orderedList1 = lists.List1.OrderBy(x => x).ToList();
        var orderedList2 = lists.List2.OrderBy(x => x).ToList();
        
        var differences = orderedList1.Zip(orderedList2, (first, second) => Math.Abs(first - second));
        var totalDifferences = differences.Sum();
        
        return totalDifferences;
    }
    
    private static (List<int> List1, List<int> List2) Parse()
    {
        var list1 = new List<int>();
        var list2 = new List<int>();
        
        using var reader = new StreamReader(FilePath);
        while (reader.ReadLine() is { } line)
        {
            var parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            
            if (parts.Length == 2 &&
                int.TryParse(parts[0], out var number1) &&
                int.TryParse(parts[1], out var number2))
            {
                list1.Add(number1);
                list2.Add(number2);
            }
            else
            {
                throw new Exception("Unable to parse line: " + line);
            }
        }
        
        return (list1, list2);
    }
}

