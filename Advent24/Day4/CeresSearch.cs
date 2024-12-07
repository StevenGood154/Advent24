namespace Advent24.Day4;

public static class CeresSearch
{
    private static readonly string FilePath = Path.Combine(Environment.CurrentDirectory, "Input/day4.txt");

    public static int Part1()
    {
        var characterMap = Parse();

        var xCoordinates = characterMap['X'];
        var wordCount = xCoordinates.Sum(c => CountWordInDirectionsFromStart(characterMap, c, "XMAS", ValidDirections));

        return wordCount;
    }

    private static int CountWordInDirectionsFromStart(Dictionary<char, HashSet<Coordinate>> characterMap, Coordinate staringCoordinate, string word, IEnumerable<Coordinate> directions)
    {
        return directions.Count(direction => IsWordInDirectionFromStart(characterMap, staringCoordinate, word, direction));
    }

    private static bool IsWordInDirectionFromStart(Dictionary<char, HashSet<Coordinate>> characterMap, Coordinate staringCoordinate, string word, Coordinate direction)
    {
        for (var charIndex = 0; charIndex < word.Length; charIndex++)
        {
            var character = word[charIndex];
            var offsetFromStartOfWord = direction * new Coordinate(charIndex, charIndex);
            if (!characterMap[character].Contains(staringCoordinate + offsetFromStartOfWord))
            {
                return false;
            }
        }

        return true;
    }

    private static HashSet<Coordinate> ValidDirections { get; } =
    [
        new Coordinate(1, 1),
        new Coordinate(1, 0),
        new Coordinate(1, -1),
        new Coordinate(-1, 1),
        new Coordinate(-1, 0),
        new Coordinate(-1, -1),
        new Coordinate(0, 1),
        new Coordinate(0, -1),
    ];

    private static Dictionary<char, HashSet<Coordinate>> Parse()
    {
        var characterMap = new Dictionary<char, HashSet<Coordinate>>();

        using var reader = new StreamReader(FilePath);
        var currentRow = 0;
        while (reader.ReadLine() is { } line)
        {
            var charArray = line.ToCharArray();
            for (var currentColumn = 0; currentColumn < charArray.Length; currentColumn++)
            {
                var character = charArray[currentColumn];
                if (!characterMap.ContainsKey(character))
                {
                    characterMap.Add(character, []);
                }
                characterMap[character].Add(new Coordinate(currentRow, currentColumn));
            }

            currentRow++;
        }

        return characterMap;
    }

    private record Coordinate(int Row, int Column)
    {
        public static Coordinate operator +(Coordinate left, Coordinate right) => new(left.Row + right.Row, left.Column + right.Column);
        public static Coordinate operator -(Coordinate left, Coordinate right) => new(left.Row - right.Row, left.Column - right.Column);
        public static Coordinate operator *(Coordinate left, Coordinate right) => new(left.Row * right.Row, left.Column * right.Column);
    }
}

