using System.Reflection;
using WordleHelper;

// 读取词库
Console.Title = "Wordle爆破器";
Console.WriteLine("是否使用自定义词库? 词库应为txt文件，每行一个单词");
Console.WriteLine("输入自定义词库的完整路径，或按回车以使用自带词库");
var path = Console.ReadLine() ?? "";
var words = new List<string>();
try
{
    var lines = File.ReadAllLines(path);
    words.AddRange(lines.Select(s => s.Trim().Trim('\r').ToLower()));
    Console.WriteLine($"已使用自定义词库！单词总量: {words.Count}");
}
catch
{
    Console.WriteLine("词库路径无效, 将使用默认词库");
}

if (words.Count == 0)
{
    var assembly = Assembly.GetExecutingAssembly();
    using var stream = assembly.GetManifestResourceStream("WordleHelper.words_alpha.txt") ??
                       throw new Exception("未找到词库");
    using var reader = new StreamReader(stream);
    words.AddRange(reader.ReadToEnd().Split('\n').Select(s => s.Trim().Trim('\r').ToLower()));
    Console.WriteLine("默认词库已加载");
}

var wordClues = new WordClueDictionary();

while (true)
{
    Console.Clear();
    var allWords = words.ToList();
    // 开始一轮猜测
    Console.Write("请输入你的第一轮猜测:");
    var guess = Console.ReadLine();
    if (guess == null)
    {
        Console.WriteLine("无效的输入");
        continue;
    }

    if (guess == "END")
    {
        Console.WriteLine("猜测结束");
        break;
    }

    Console.WriteLine(new string('-', 40));
    Console.WriteLine("结果输入格式，灰色:0 黄色:1 绿色:2");
    Console.WriteLine(new string('-', 40));

    var wordLength = guess.Length;
    wordClues.WordLength = wordLength;

    for (var i = 0; i < wordLength; i++)
    {
        guess = guess!.ToLower();
        Console.Write("结果:");
        var result = Console.ReadLine();
        if (result?.Length != wordLength)
        {
            Console.WriteLine("无效的输入");
            i--;
            continue;
        }

        // 处理结果，添加单词线索
        for (var j = 0; j < result.Length; j++)
        {
            var r = result[j];
            var ch = guess[j];
            switch (r)
            {
                case '0':
                    wordClues.TryAddClue(ch, new WordClue { ClueType = WordClueType.Gray, Position = j });
                    break;
                case '1':
                    wordClues.TryAddClue(ch, new WordClue { ClueType = WordClueType.Yellow, Position = j });
                    break;
                case '2':
                    wordClues.TryAddClue(ch, new WordClue { ClueType = WordClueType.Green, Position = j });
                    break;
                default:
                    Console.WriteLine("存在无效输入");
                    break;
            }
        }

        // 过滤单词
        allWords = wordClues.FilterWords(allWords);

        // 输出建议
        Console.WriteLine("可能的单词:");
        Console.WriteLine(string.Join(" ", allWords));

        Console.Write("本轮猜测:");

        guess = Console.ReadLine();
        if (guess == null)
        {
            Console.WriteLine("无效的输入");
            continue;
        }

        if (guess == "END")
        {
            Console.WriteLine("猜测结束");
            break;
        }
    }
}