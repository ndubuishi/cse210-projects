public abstract class Activity
{
    private DateTime date;
    private int lengthInMinutes;

    public Activity(DateTime date, int lengthInMinutes)
    {
        this.date = date;
        this.lengthInMinutes = lengthInMinutes;
    }

    protected DateTime Date => date;
    protected int LengthInMinutes => lengthInMinutes;

    public abstract double GetDistance();  // in miles
    public abstract double GetSpeed();     // in mph
    public abstract double GetPace();      // in min per mile

    public virtual string GetSummary()
    {
        return $"{date:dd MMM yyyy} {GetType().Name.Replace("Activity", "")} ({lengthInMinutes} min): " +
               $"Distance {GetDistance():F1} miles, Speed {GetSpeed():F1} mph, Pace: {GetPace():F1} min per mile";
    }
}

public class RunningActivity : Activity
{
    private double distance; // in miles

    public RunningActivity(DateTime date, int lengthInMinutes, double distance)
        : base(date, lengthInMinutes)
    {
        this.distance = distance;
    }

    public override double GetDistance() => distance;

    public override double GetSpeed()
    {
        return (distance / LengthInMinutes) * 60;
    }

    public override double GetPace()
    {
        return LengthInMinutes / distance;
    }
}

public class CyclingActivity : Activity
{
    private double speed; // in mph

    public CyclingActivity(DateTime date, int lengthInMinutes, double speed)
        : base(date, lengthInMinutes)
    {
        this.speed = speed;
    }

    public override double GetDistance()
    {
        return (speed * LengthInMinutes) / 60;
    }

    public override double GetSpeed() => speed;

    public override double GetPace()
    {
        return 60 / speed;
    }
}

public class SwimmingActivity : Activity
{
    private int laps; // number of laps (50 meters each)

    public SwimmingActivity(DateTime date, int lengthInMinutes, int laps)
        : base(date, lengthInMinutes)
    {
        this.laps = laps;
    }

    public override double GetDistance()
    {
        return laps * 50 / 1000.0 * 0.62;
    }

    public override double GetSpeed()
    {
        return (GetDistance() / LengthInMinutes) * 60;
    }

    public override double GetPace()
    {
        return LengthInMinutes / GetDistance();
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Activity> activities = new List<Activity>();

        activities.Add(new RunningActivity(new DateTime(2022, 11, 3), 30, 3.0));     // 3 miles
        activities.Add(new CyclingActivity(new DateTime(2022, 11, 3), 30, 12.0));   // 12 mph
        activities.Add(new SwimmingActivity(new DateTime(2022, 11, 3), 30, 40));    // 40 laps

        Console.WriteLine("Exercise Tracking Summary\n");
        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}