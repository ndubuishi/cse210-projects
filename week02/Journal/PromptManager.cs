// PromptManager.cs
using System;

public class PromptManager
{
    private readonly string[] prompts = {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I could change one thing about today, what would it be?",
        "What am I grateful for today?",
        "What did I learn today?",
        "How did I help someone today?"
    };

    private readonly Random random = new();

    public string GetRandomPrompt()
    {
        return prompts[random.Next(prompts.Length)];
    }
}