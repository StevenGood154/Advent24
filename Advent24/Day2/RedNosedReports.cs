namespace Advent24.Day2;

public static class RedNosedReports
{
    private static readonly string FilePath = Path.Combine(Environment.CurrentDirectory, "Day2/input.txt");

    private const int MinDifference = 1;
    private const int MaxDifference = 3;

    public static int Part1()
    {
        var reports = Parse();
        return reports.Where(IsReportSafeWithoutProblemDampener).Count();
    }

    public static int Part2()
    {
        var reports = Parse();
        return reports.Where(IsReportSafeWithProblemDampener).Count();
    }

    private static bool IsReportSafeWithoutProblemDampener(List<int> report)
    {
        var levelPairs = report.Zip(report.Skip(1), (first, second) => (first, second)).ToList();
        return AreLevelPairsGraduallyIncreasing(levelPairs) || AreLevelPairsGraduallyDecreasing(levelPairs); 
    }

    private static bool IsReportSafeWithProblemDampener(List<int> report)
    {
        var reportsWithOneLevelRemoved= Enumerable.Range(0, report.Count).Select(report.TakeAllButNth).Select(e => e.ToList());
        return IsReportSafeWithoutProblemDampener(report) || reportsWithOneLevelRemoved.Any(IsReportSafeWithoutProblemDampener);
    }

    private static IEnumerable<T> TakeAllButNth<T>(this ICollection<T> collection, int n) => collection.Take(n).Concat(collection.Skip(n + 1));

    private static bool AreLevelPairsGraduallyIncreasing(IEnumerable<(int first, int second)> levelPairs)
    {
        return levelPairs.All(pair =>
        {
            var difference = pair.second - pair.first;
            return difference is >= MinDifference and <= MaxDifference;
        }); 
    }

    private static bool AreLevelPairsGraduallyDecreasing(IEnumerable<(int first, int second)> levelPairs)
    {
        return levelPairs.All(pair =>
        {
            var difference = pair.second - pair.first;
            return difference is <= -MinDifference and >= -MaxDifference;
        }); 
    }

    private static List<List<int>> Parse()
    {
        var reports = new List<List<int>>();
        
        using var reader = new StreamReader(FilePath);
        while (reader.ReadLine() is { } line)
        {
            var levels = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            reports.Add(levels);
        }
        
        return reports;
    }
}

