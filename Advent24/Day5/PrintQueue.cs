namespace Advent24.Day5;

public static class PrintQueue
{
    private static readonly string FilePath = Path.Combine(Environment.CurrentDirectory, "Input/day5.txt");

    public static int Part1()
    {
        var (rules, updates) = Parse();
        
        var middlePages = updates.Where(u => IsUpdateValid(u, rules)).Select(GetMiddlePage);
        return middlePages.Sum();
    }

    public static int Part2()
    {
        var (rules, updates) = Parse();

        var rulesComparer = new RulesComparer(rules);
        var middlePages = updates.Where(update => !IsUpdateValid(update, rules))
            .Select(update => update.OrderBy(u => u, rulesComparer).ToList())
            .Select(GetMiddlePage);
        return middlePages.Sum();
    }

    private static bool IsUpdateValid(List<int> update, HashSet<(int Left, int Right)> rules)
    {
        return rules.All(rule => IsUpdateValidOnRule(update, rule));
    }
    
    private static bool IsUpdateValidOnRule(List<int> update, (int Left, int Right) rule)
    {
        var leftIndex = update.FindIndex(p => p == rule.Left);
        var rightIndex = update.FindIndex(p => p == rule.Right);
        
        if (leftIndex == -1 || rightIndex == -1)
        {
            return true;
        }
        return leftIndex < rightIndex;
    }

    private static int GetMiddlePage(List<int> update)
    {
        var middleIndex = (update.Count - 1) / 2;
        return update[middleIndex];
    }

    private static (HashSet<(int Left, int Right)> Rules, List<List<int>> Updates) Parse()
    {
        var rules = new HashSet<(int, int)>();
        var updates = new List<List<int>>();

        var isRuleSection = true;
        using var reader = new StreamReader(FilePath);
        while (reader.ReadLine() is { } line)
        {
            if (line == string.Empty)
            {
                isRuleSection = false;
            }
            else if (isRuleSection)
            {
                var parts = line.Split("|");
                rules.Add((int.Parse(parts[0]), int.Parse(parts[1])));
            }
            else
            {
                var parts = line.Split(",");
                updates.Add(parts.Select(int.Parse).ToList());
            }
        }
        
        return (rules, updates);
    }

    private class RulesComparer(HashSet<(int Left, int Right)> rules) : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            if (rules.Contains((x, y)))
            {
                return -1;
            };
            if (rules.Contains((y, x)))
            {
                return 1;
            };
            return 0;
        }
    }
}
