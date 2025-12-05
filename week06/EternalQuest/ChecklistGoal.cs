public class ChecklistGoal : Goal
{
    private int _amountCompleted;
    private int _target;
    private int _bonus;

    public ChecklistGoal(string name, string description, int points, int target, int bonus)
        : base(name, description, points)
    {
        _target = target;
        _bonus = bonus;
        _amountCompleted = 0;
    }

    public ChecklistGoal(string name, string description, int points, int target, int bonus, int amountCompleted)
        : this(name, description, points, target, bonus)
    {
        _amountCompleted = amountCompleted;
        if (_amountCompleted >= _target)
            IsComplete = true;
    }

    public override int RecordEvent()
    {
        if (IsComplete) return 0;

        _amountCompleted++;
        if (_amountCompleted >= _target)
        {
            IsComplete = true;
            return _points + _bonus;
        }
        return _points;
    }

    public override string GetDetailsString()
    {
        return $"{(IsComplete ? "[X]" : "[ ]")} {_shortName} -- {_description} -- Completed {_amountCompleted}/{_target}";
    }

    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal:{_shortName},{_description},{_points},{_target},{_bonus},{_amountCompleted}";
    }
}