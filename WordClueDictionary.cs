namespace WordleHelper;

internal class WordClueDictionary
{
    private readonly Dictionary<char, List<WordClue>> _clues = new();
    public int WordLength { get; set; }

    public WordClueDictionary()
    {
        for (var ch = 'a'; ch <= 'z'; ch++) _clues[ch] = new List<WordClue>();
    }

    private void ClearAllClues()
    {
        foreach (var (_, clues) in _clues) clues.Clear();
    }

    public void TryAddClue(char ch, WordClue clue)
    {
        var clues = _clues[ch];

        // 检查是否已有包含性线索
        var containsClues = clues.Where(c => c.ClueType != WordClueType.Gray).ToList();
        if (containsClues.Count > 0 && clue.ClueType == WordClueType.Gray) return;

        // 如果没有，添加一条新线索
        clues.Add(clue);
    }

    public List<string> FilterWords(List<string> words)
    {
        words = words.FindAll(w => w.Length == WordLength);
        foreach (var (ch, clues) in _clues)
        {
            if (clues.Count == 0) continue;
            // 先依据排除性线索筛选单词
            var grayClue = clues.FirstOrDefault(c => c.ClueType == WordClueType.Gray);
            if (grayClue != null)
                words = words.FindAll(w => !w.Contains(ch));

            // 再依据包含性线索筛选单词
            var containsClues = clues.Where(c => c.ClueType != WordClueType.Gray).ToList();
            var yellowClues = containsClues.Where(c => c.ClueType == WordClueType.Yellow).ToList();
            var greenClues = containsClues.Where(c => c.ClueType == WordClueType.Green).ToList();
            if (containsClues.Count > 0)
                words = words.FindAll(w => w.HasLetter(ch, containsClues.Count));

            if (yellowClues.Count > 0)
                words = yellowClues.Aggregate(words, (current, clue) => current.FindAll(w => w[clue.Position] != ch));

            if (greenClues.Count > 0)
                words = greenClues.Aggregate(words, (current, clue) => current.FindAll(w => w[clue.Position] == ch));
        }

        ClearAllClues();
        return words.ToList();
    }

    public int GetClueCount(char ch) => _clues[ch].Count;
    public List<WordClue> GetClues(char ch) => _clues[ch];
    public List<WordClue> GetGreenClues(char ch) => _clues[ch].Where(c => c.ClueType == WordClueType.Green).ToList();
    public List<WordClue> GetYellowClues(char ch) => _clues[ch].Where(c => c.ClueType == WordClueType.Yellow).ToList();
}