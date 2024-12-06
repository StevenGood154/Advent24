using System.Text.RegularExpressions;

namespace Advent24.Day3;

public static partial class MullItOver
{
    private static readonly string FilePath = Path.Combine(Environment.CurrentDirectory, "Input/day3.txt");

    public static int Part1()
    {
        var fileContents = Parse();
        
        var regex = MultiplyRegex();
        var matches = regex.Matches(fileContents);
        
        var products = matches.Select(match => int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value));
        return products.Sum();
    }
    
    [GeneratedRegex(@"mul\((\d+),(\d+)\)")]
    private static partial Regex MultiplyRegex();

    private static string Parse()
    {
        return File.ReadAllText(FilePath);
    }
}

