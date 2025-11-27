using System;
using System.Threading;

public class Activity
{
    protected string _name;
    protected string _description;
    protected int _duration;

    public Activity(string name, string description)
    {
        _name = name;
        _description = description;
    }

    public void Run()
    {
        DisplayStartingMessage();
        PerformActivity();
        DisplayEndingMessage();
    }

    protected void DisplayStartingMessage()
    {
        Console.Clear();
        Console.WriteLine($"Welcome to the {_name} Activity.\n");
        Console.WriteLine(_description);
        Console.Write("\nHow long, in seconds, would you like for your session? ");
        _duration = int.Parse(Console.ReadLine() ?? "30");
        Console.Clear();
        Console.WriteLine("Get ready...");
        ShowSpinner(3);
    }

    protected void DisplayEndingMessage()
    {
        Console.WriteLine("\n\nWell done!!");
        ShowSpinner(3);
        Console.WriteLine($"\nYou have completed the {_name} Activity.");
        Console.WriteLine($"Time spent: {_duration} seconds");
        ShowSpinner(4);
        Thread.Sleep(2000);
    }

    protected virtual void PerformActivity()
    {
        // To be overridden
    }

    protected void ShowSpinner(int seconds)
    {
        string[] spinner = { "|", "/", "-", "\\" };
        for (int i = 0; i < seconds * 4; i++)
        {
            Console.Write(spinner[i % 4]);
            Thread.Sleep(250);
            Console.Write("\b \b");
        }
        Console.WriteLine();
    }

    protected void ShowCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write(i);
            Thread.Sleep(1000);
            Console.Write("\b \b");
        }
        Console.WriteLine();
    }
}