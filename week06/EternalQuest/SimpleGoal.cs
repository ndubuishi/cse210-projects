// SimpleGoal.cs
public class SimpleGoal : Goal
{
    public SimpleGoal(string name, string description, int points) 
        : base(name, description, points) { }

    public SimpleGoal(string name, string description, int points, bool isComplete)
        : base(name, description, points)
    {
        IsComplete = isComplete;
    }

    public override int RecordEvent()
    {
        if (!IsComplete)
        {
            IsComplete = true;
            return _points;
        }
        return 0;
    }

    public override string GetDetailsString()
    {
        return $"{(IsComplete ? "[X]" : "[ ]")} {_shortName} -- {_description}";
    }

    public override string GetStringRepresentation()
    {
        return $"SimpleGoal:{_shortName},{_description},{_points},{IsComplete}";
    }
}