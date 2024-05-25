namespace WordleHelper;

internal static class WordUtils
{
    public static bool HasLetter(this string word, char letter, int times = 1) => word.Count(c => c == letter) >= times;
}