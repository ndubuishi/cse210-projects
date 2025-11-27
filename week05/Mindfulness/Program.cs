// W05 Mindfulness Program - Exceeds Requirements
// Creativity & Exceeds:
// 1. Added a 4th activity: Gratitude Activity
// 2. Tracks and displays total sessions completed (saved to log.txt)
// 3. Prevents duplicate prompts/questions in one session (uses a queue system)
// 4. Saves session log to file (log.txt) with timestamp
// 5. Enhanced breathing animation with expanding/contracting text

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Mindfulness
{
    class Program
    {
        private static int totalSessions = 0;
        private static readonly string logFile = "log.txt";

        static void Main(string[] args)
        {
            LoadLog();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Mindfulness & Reflection App");
                Console.WriteLine("Total sessions completed: " + totalSessions);
                Console.WriteLine("\nMenu Options:");
                Console.WriteLine("  1. Breathing Activity");
                Console.WriteLine("  2. Reflecting Activity");
                Console.WriteLine("  3. Listing Activity");
                Console.WriteLine("  4. Gratitude Activity (Bonus!)");
                Console.WriteLine("  5. Quit");
                Console.Write("Select a choice from the menu: ");

                string choice = Console.ReadLine();

                Activity activity = choice switch
                {
                    "1" => new BreathingActivity(),
                    "2" => new ReflectingActivity(),
                    "3" => new ListingActivity(),
                    "4" => new GratitudeActivity(),
                    "5" => null,
                    _ => null
                };

                if (activity == null) break;

                activity.Run();
                totalSessions++;
                SaveLog();
            }

            Console.WriteLine("\nThank you for practicing mindfulness today!");
            Thread.Sleep(2000);
        }

        private static void LoadLog()
        {
            if (File.Exists(logFile))
            {
                string[] lines = File.ReadAllLines(logFile);
                if (lines.Length > 0)
                    int.TryParse(lines[0], out totalSessions);
            }
        }

        private static void SaveLog()
        {
            File.WriteAllText(logFile, totalSessions.ToString() + "\n" +
                $"Last session: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n");
        }
    }
}