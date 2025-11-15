// W03 Scripture Memorizer - @NansNdubest
// Exceeds Requirements:
// 1. Loads multiple scriptures from 'scriptures.txt'
// 2. Picks a random scripture each run
// 3. Only hides words that are still visible (no re-hiding)
// 4. Supports single and verse-range references
// 5. Clean, encapsulated OOP design

class Program
{
    static void Main(string[] args)
    {
        var scriptures = LoadScripturesFromFile("scriptures.txt");
        if (!scriptures.Any())
        {
            Console.WriteLine("No scriptures found in file.");
            return;
        }

        var random = new Random();
        var scripture = scriptures[random.Next(scriptures.Count)];

        while (true)
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine("\nPress ENTER to hide more words, or type 'quit' to exit.");

            string input = Console.ReadLine()?.Trim().ToLower();

            if (input == "quit") break;
            if (scripture.IsCompletelyHidden()) break;

            scripture.HideRandomWords(3); // Hide 3 words at a time
        }

        Console.Clear();
        Console.WriteLine(scripture.GetDisplayText());
        Console.WriteLine("\nAll words hidden. Program ended.");
    }

    static List<Scripture> LoadScripturesFromFile(string filename)
    {
        var scriptures = new List<Scripture>();
        if (!File.Exists(filename)) return scriptures;

        var lines = File.ReadAllLines(filename);
        for (int i = 0; i < lines.Length; i += 2)
        {
            if (i + 1 >= lines.Length) break;

            string refLine = lines[i].Trim();
            string textLine = lines[i + 1].Trim();

            if (string.IsNullOrEmpty(refLine) || string.IsNullOrEmpty(textLine)) continue;

            var reference = ParseReference(refLine);
            if (reference != null)
            {
                scriptures.Add(new Scripture(reference, textLine));
            }
        }

        return scriptures;
    }

    static Reference ParseReference(string refText)
    {
        try
        {
            var parts = refText.Split(' ');
            string book = string.Join(" ", parts.Take(parts.Length - 1));
            string versePart = parts[^1];

            if (versePart.Contains('-'))
            {
                var verses = versePart.Split('-');
                var chapterVerse = verses[0].Split(':');
                int chapter = int.Parse(chapterVerse[0]);
                int start = int.Parse(chapterVerse[1]);
                int end = int.Parse(verses[1]);
                return new Reference(book, chapter, start, end);
            }
            else
            {
                var chapterVerse = versePart.Split(':');
                int chapter = int.Parse(chapterVerse[0]);
                int verse = int.Parse(chapterVerse[1]);
                return new Reference(book, chapter, verse);
            }
        }
        catch
        {
            return null;
        }
    }
}