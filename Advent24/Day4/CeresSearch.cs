namespace Advent24.Day4;

public static class CeresSearch
{
    private static readonly string FilePath = Path.Combine(Environment.CurrentDirectory, "Input/day4.txt");

    public static int Part1()
    {
        var characterMap = Parse();

        var xCoordinates = characterMap['X'];
        var wordCount = xCoordinates.Sum(coordinate =>
            Direction.All.Count(direction => IsWordThroughCharacterInDirection(characterMap, coordinate, 0,"XMAS", direction)));

        return wordCount;
    }

    public static int Part2()
    {
        var characterMap = Parse();

        var aCoordinates = characterMap['A'];
        var wordCount = aCoordinates.Count(c =>
            (IsWordThroughCharacterInDirection(characterMap, c, 1, "MAS", Direction.UpRight)
             || IsWordThroughCharacterInDirection(characterMap, c, 1, "MAS", Direction.DownLeft))
           && (IsWordThroughCharacterInDirection(characterMap, c, 1, "MAS", Direction.UpLeft)
             || IsWordThroughCharacterInDirection(characterMap, c, 1, "MAS", Direction.DownRight))
        );

        return wordCount;
    }

    private static bool IsWordThroughCharacterInDirection(
        Dictionary<char, HashSet<Coordinate>> characterMap,
        Coordinate characterCoordinate,
        int characterCoordinateIndexInWord,
        string word,
        Coordinate direction)
    {
        for (var charIndex = 0; charIndex < word.Length; charIndex++)
        {
            var character = word[charIndex];
            var offsetFromStartOfWord = direction * new Coordinate(charIndex - characterCoordinateIndexInWord, charIndex - characterCoordinateIndexInWord);
            if (!characterMap[character].Contains(characterCoordinate + offsetFromStartOfWord))
            {
                return false;
            }
        }

        return true;
    }

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

    private static class Direction
    {
        public static readonly Coordinate UpRight = new(1, 1);
        public static readonly Coordinate Up = new(1, 0);
        public static readonly Coordinate UpLeft = new(1, -1);
        public static readonly Coordinate DownRight = new(-1, 1);
        public static readonly Coordinate Down = new(-1, 0);
        public static readonly Coordinate DownLeft = new(-1, -1);
        public static readonly Coordinate Right = new(0, 1);
        public static readonly Coordinate Left = new(0, -1);
        public static HashSet<Coordinate> All { get; } =
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
    }
}
