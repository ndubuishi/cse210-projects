using System;
using System.Collections.Generic;

public class ReflectingActivity : Activity
{
    private List<string> _prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> _questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different?",
        "What is your favorite thing about this experience?",
        "What did you learn about yourself?",
        "How can you keep this in mind in the future?"
    };

    public ReflectingActivity() : base("Reflecting", "This activity will help you reflect on times in your life when you have shown strength and resilience.")
    {
    }

    protected override void PerformActivity()
    {
        Random rand = new Random();
        string prompt = _prompts[rand.Next(_prompts.Count)];
        Console.WriteLine("Consider the following prompt:\n");
        Console.WriteLine($" --- {prompt} --- ");
        Console.WriteLine("\nWhen you have something in mind, press Enter to continue.");
        Console.ReadLine();

        Console.WriteLine("Now ponder on each of the following questions as they relate to this experience.");
        ShowCountdown(8);

        DateTime endTime = DateTime.Now.AddSeconds(_duration);
        List<string> usedQuestions = new List<string>();

        while (DateTime.Now < endTime && usedQuestions.Count < _questions.Count)
        {
            string question = GetUnusedQuestion(usedQuestions);
            usedQuestions.Add(question);

            Console.Write($"> {question} ");
            ShowSpinner(5);
            Console.WriteLine();
        }
    }

    private string GetUnusedQuestion(List<string> used)
    {
        var available = new List<string>(_questions);
        foreach (var q in used) available.Remove(q);
        if (available.Count == 0) return "Keep reflecting...";
        return available[new Random().Next(available.Count)];
    }
}