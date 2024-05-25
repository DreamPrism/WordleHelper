namespace WordleHelper;

internal record WordClue
{
    public WordClueType ClueType { get; init; }
    public int Position { get; init; }
}

internal enum WordClueType
{
    Gray = 0,
    Yellow = 1,
    Green = 2
}