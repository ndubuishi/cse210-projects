using System;
using System.Threading;

public class BreathingActivity : Activity
{
    public BreathingActivity() : base("Breathing", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
    }

    protected override void PerformActivity()
    {
        DateTime endTime = DateTime.Now.AddSeconds(_duration);
        bool inhale = true;

        while (DateTime.Now < endTime)
        {
            string message = inhale ? "Breathe in" : "Breathe out";
            Console.Write(message);
            for (int i = 1; i <= 5; i++)
            {
                Console.Write($" {new string('.', i)}   ");
                Thread.Sleep(800);
                Console.Write("\r" + new string(' ', 30) + "\r");
            }
            Console.WriteLine(inhale ? "Now breathe in..." : "Now breathe out...");
            inhale = !inhale;
        }
    }
}