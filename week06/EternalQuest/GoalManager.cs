public class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private Player _player = new Player();

    public void Start()
    {
        while (true)
        {
            DisplayPlayerInfo();
            Console.WriteLine("\nMenu Options:");
            Console.WriteLine("  1. List Goals");
            Console.WriteLine("  2. Create New Goal");
            Console.WriteLine("  3. Record Event");
            Console.WriteLine("  4. Save Goals");
            Console.WriteLine("  5. Load Goals");
            Console.WriteLine("  6. Quit");
            Console.Write("Select a choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": ListGoals(); break;
                case "2": CreateGoal(); break;
                case "3": RecordEvent(); break;
                case "4": SaveGoals(); break;
                case "5": LoadGoals(); break;
                case "6": return;
            }
        }
    }

    private void DisplayPlayerInfo()
    {
        Console.WriteLine($"\nYou have {_player.Score} points | Level {_player.Level}: {_player.Title}");
    }

    private void ListGoals()
    {
        Console.WriteLine("\nYour Goals:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i+1}. {_goals[i].GetDetailsString()}");
        }
    }

    private void CreateGoal()
    {
        Console.WriteLine("Goal Types:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.WriteLine("4. Negative Goal (Avoid!)");
        Console.Write("Which type? ");
        string type = Console.ReadLine();

        Console.Write("Name: ");
        string name = Console.ReadLine();
        Console.Write("Description: ");
        string desc = Console.ReadLine();
        Console.Write("Points: ");
        int points = int.Parse(Console.ReadLine());

        Goal goal = type switch
        {
            "1" => new SimpleGoal(name, desc, points),
            "2" => new EternalGoal(name, desc, points),
            "3" => CreateChecklistGoal(name, desc, points),
            "4" => new NegativeGoal(name, desc, points),
            _ => null
        };

        if (goal != null) _goals.Add(goal);
    }

    private Goal CreateChecklistGoal(string name, string desc, int points)
    {
        Console.Write("How many times to complete? ");
        int target = int.Parse(Console.ReadLine());
        Console.Write("Bonus points for completion? ");
        int bonus = int.Parse(Console.ReadLine());
        return new ChecklistGoal(name, desc, points, target, bonus);
    }

    private void RecordEvent()
    {
        ListGoals();
        Console.Write("Which goal did you accomplish? ");
        int index = int.Parse(Console.ReadLine()) - 1;
        if (index >= 0 && index < _goals.Count)
        {
            int points = _goals[index].RecordEvent();
            _player.AddPoints(points);
            Console.WriteLine(points > 0 
                ? $"You earned {points} points!" 
                : $"You lost {-points} points...");
        }
    }

    public void SaveGoals()
    {
        Console.Write("Enter filename to save: ");
        string filename = Console.ReadLine();

        using (StreamWriter file = new StreamWriter(filename))
        {
            file.WriteLine(_player.Score);
            foreach (Goal g in _goals)
            {
                file.WriteLine(g.GetStringRepresentation());
            }
        }
        Console.WriteLine("Goals saved!");
    }

    public void LoadGoals()
    {
        Console.Write("Enter filename to load: ");
        string filename = Console.ReadLine();

        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found.");
            return;
        }

        string[] lines = File.ReadAllLines(filename);
        _goals.Clear();
        _player = new Player();

        int score = int.Parse(lines[0]);
        _player.AddPoints(score); // Restore score silently

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            string[] parts = line.Split(':');
            string type = parts[0];
            string data = parts[1];
            string[] values = data.Split(',');

            Goal goal = type switch
            {
                "SimpleGoal" => new SimpleGoal(values[0], values[1], int.Parse(values[2]), bool.Parse(values[3])),
                "EternalGoal" => new EternalGoal(values[0], values[1], int.Parse(values[2])),
                "ChecklistGoal" => new ChecklistGoal(values[0], values[1], int.Parse(values[2]), int.Parse(values[3]), int.Parse(values[4]), int.Parse(values[5])),
                "NegativeGoal" => new NegativeGoal(values[0], values[1], int.Parse(values[2])),
                _ => null
            };

            if (goal != null && _goals.Add(goal);
        }
        Console.WriteLine("Goals loaded!");
    }
}