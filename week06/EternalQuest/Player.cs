public class Player
{
    public int Score { get; private set; }
    public int Level => Score / 1000 + 1;
    public string Title => Level switch
    {
        >= 50 => "Celestial Overlord",
        >= 30 => "Archangel of Willpower",
        >= 20 => "Diamond Saint",
        >= 15 => "Master of Eternity",
        >= 10 => "Ninja Unicorn",
        >= 5  => "Goal Slayer",
        _     => "Novice Disciple"
    };

    public void AddPoints(int points)
    {
        Score += points;
        if (points > 0 && Score / 1000 > (Score - points) / 1000)
        {
            Console.WriteLine($"\nLEVEL UP! You are now Level {Level}: {Title}!");
        }
    }
}