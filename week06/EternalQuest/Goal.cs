// Goal.cs
public abstract class Goal
{
    protected string _shortName;
    protected string _description;
    protected int _points;

    public bool IsComplete { get; protected set; }

    public Goal(string name, string description, int points)
    {
        _shortName = name;
        _description = description;
        _points = points;
        IsComplete = false;
    }

    public abstract int RecordEvent();
    public abstract string GetDetailsString();
    public abstract string GetStringRepresentation();

    public string GetName() => _shortName;
}