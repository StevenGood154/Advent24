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
    
    public static int Part2()
    {
        var fileContents = Parse();

        var regex = MultiplyRegexWithEnable();
        var matches = regex.Matches(fileContents);

        var isMulEnabled = true;
        var sum = 0;
        foreach (Match match in matches)
        {
            if (match.Value == "do()")
            {
                isMulEnabled = true;
            }
            else if (match.Value == "don't()")
            {
                isMulEnabled = false;
            }
            else if (isMulEnabled)
            {
                var first = int.Parse(match.Groups[1].Value);
                var second = int.Parse(match.Groups[2].Value);
                sum += first * second;
            }
        }
        return sum;
    }

    [GeneratedRegex(@"mul\((\d+),(\d+)\)")]
    private static partial Regex MultiplyRegex();

    [GeneratedRegex(@"mul\((\d+),(\d+)\)|do\(\)|don't\(\)")]
    private static partial Regex MultiplyRegexWithEnable();

    private static string Parse()
    {
        return File.ReadAllText(FilePath);
    }
}

