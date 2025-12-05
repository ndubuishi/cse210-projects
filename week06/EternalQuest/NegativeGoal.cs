// NegativeGoal.cs
public class NegativeGoal : Goal
{
    public NegativeGoal(string name, string description, int pointsToLose)
        : base(name, description, pointsToLose) { }

    public override int RecordEvent()
    {
        Console.WriteLine($"Oh no! You lost {-_points} points for {_shortName}!");
        return -_points;
    }

    public override string GetDetailsString()
    {
        return $"[!] {_shortName} -- {_description} (Avoid this!)";
    }

    public override string GetStringRepresentation()
    {
        return $"NegativeGoal:{_shortName},{_description},{_points}";
    }
}