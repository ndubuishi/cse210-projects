using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        PromptManager promptManager = new PromptManager();
        FileManager fileManager = new FileManager();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=== Daily Journal App ===");
        Console.ResetColor();

        bool running = true;
        while (running)
        {
            DisplayMenu();
            string choice = Console.ReadLine()?.Trim() ?? "";

            switch (choice)
            {
                case "1":
                    Entry entry = CreateEntry(promptManager);
                    if (entry != null) journal.AddEntry(entry);
                    break;
                case "2":
                    journal.DisplayEntries();
                    break;
                case "3":
                    SaveJournal(journal, fileManager, false); // CSV
                    break;
                case "4":
                    LoadJournal(journal, fileManager, false); // CSV
                    break;
                case "5":
                    SaveJournal(journal, fileManager, true); // JSON
                    break;
                case "6":
                    LoadJournal(journal, fileManager, true); // JSON
                    break;
                case "7":
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Goodbye! Keep journaling!");
                    Console.ResetColor();
                    running = false;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid option. Please try again.");
                    Console.ResetColor();
                    break;
            }
        }
    }

    static void DisplayMenu()
    {
        Console.WriteLine("\n" + new string('-', 40));
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("            JOURNAL MENU");
        Console.ResetColor();
        Console.WriteLine(new string('-', 40));
        Console.WriteLine("1. Write a new entry");
        Console.WriteLine("2. Display the journal");
        Console.WriteLine("3. Save journal to CSV file");
        Console.WriteLine("4. Load journal from CSV file");
        Console.WriteLine("5. Save journal to JSON file (Bonus)");
        Console.WriteLine("6. Load journal from JSON file (Bonus)");
        Console.WriteLine("7. Quit");
        Console.Write("Choose an option (1-7): ");
    }

    static Entry CreateEntry(PromptManager pm)
    {
        string prompt = pm.GetRandomPrompt();
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"\nPrompt: {prompt}");
        Console.ResetColor();
        Console.Write("Your response: ");
        string response = Console.ReadLine()?.Trim() ?? "";

        if (string.IsNullOrEmpty(response))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Response cannot be empty.");
            Console.ResetColor();
            return null;
        }

        Console.Write("How are you feeling? (Happy/Neutral/Sad/Excited/Stressed): ");
        string moodInput = Console.ReadLine()?.Trim() ?? "Neutral";
        string mood = moodInput.ToLower() switch
        {
            "happy" => "Happy",
            "neutral" => "Neutral",
            "sad" => "Sad",
            "excited" => "Excited",
            "stressed" => "Stressed",
            _ => "Neutral"
        };

        string date = DateTime.Now.ToShortDateString();
        return new Entry(prompt, response, date, mood);
    }

    static void SaveJournal(Journal journal, FileManager fm, bool asJson)
    {
        Console.Write(asJson ? "Enter JSON filename: " : "Enter CSV filename: ");
        string filename = Console.ReadLine()?.Trim();
        if (string.IsNullOrEmpty(filename)) return;

        if (!asJson && !filename.EndsWith(".csv", StringComparison.OrdinalIgnoreCase)) filename += ".csv";
        if (asJson && !filename.EndsWith(".json", StringComparison.OrdinalIgnoreCase)) filename += ".json";

        try
        {
            if (asJson)
                fm.SaveAsJson(journal, filename);
            else
                fm.SaveAsCsv(journal, filename);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Journal saved to {filename}");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error saving file: {ex.Message}");
            Console.ResetColor();
        }
    }

    static void LoadJournal(Journal journal, FileManager fm, bool asJson)
    {
        Console.Write(asJson ? "Enter JSON filename to load: " : "Enter CSV filename to load: ");
        string filename = Console.ReadLine()?.Trim();
        if (string.IsNullOrEmpty(filename)) return;

        try
        {
            journal.Clear(); // start fresh
            if (asJson)
                fm.LoadFromJson(journal, filename);
            else
                fm.LoadFromCsv(journal, filename);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Journal loaded from {filename}");
            Console.ResetColor();
        }
        catch (FileNotFoundException)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("File not found.");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error loading file: {ex.Message}");
            Console.ResetColor();
        }
    }
}