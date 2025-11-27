using System;
using System.Collections.Generic;

public class GratitudeActivity : Activity
{
    private List<string> _prompts = new List<string>
    {
        "What are you grateful for today?",
        "Who made a difference in your life recently?",
        "What simple pleasures bring you joy?",
        "What about your health are you thankful for?"
    };

    public GratitudeActivity() : base("Gratitude", "This activity helps you focus on the blessings in your life and cultivate daily gratitude.")
    {
    }

    protected override void PerformActivity()
    {
        Random rand = new Random();
        Console.WriteLine("Take a moment to reflect on gratitude...");
        ShowSpinner(3);

        string prompt = _prompts[rand.Next(_prompts.Count)];
        Console.WriteLine($"\n{prompt}");
        Console.WriteLine("Type one thing at a time. Press Enter twice when done.\n");

        List<string> gratitudes = new List<string>();
        string input;
        while (DateTime.Now < DateTime.Now.AddSeconds(_duration))
        {
            input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) break;
            gratitudes.Add(input);
        }

        Console.WriteLine($"\nYou expressed gratitude for {gratitudes.Count} things today.");
        Console.WriteLine("Wonderful practice!");
    }
}