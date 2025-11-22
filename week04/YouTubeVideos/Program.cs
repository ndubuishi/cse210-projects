using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        Video video1 = new Video
        {
            Title = "Amazing Grace (Violin Cover)",
            Author = "Lindsey Stirling",
            LengthInSeconds = 295
        };
        video1.AddComment(new Comment("Sarah87", "This gave me chills! Beautiful!"));
        video1.AddComment(new Comment("MusicLover23", "Best violinist on YouTube!"));
        video1.AddComment(new Comment("JohnDoe", "So peaceful. Thank you."));
        video1.AddComment(new Comment("FaithfulOne", "Perfect hymn performance."));
        videos.Add(video1);

        Video video2 = new Video
        {
            Title = "C# Tutorial for Beginners",
            Author = "Programming with Mosh",
            LengthInSeconds = 1020
        };
        video2.AddComment(new Comment("CodeNewbie", "Finally understood classes! Thanks!"));
        video2.AddComment(new Comment("Dev2025", "Best C# series ever!"));
        video2.AddComment(new Comment("Student99", "Please do more advanced topics!"));
        videos.Add(video2);

        Video video3 = new Video
        {
            Title = "Baby Otter Learns to Swim",
            Author = "CuteAnimalsDaily",
            LengthInSeconds = 187
        };
        video3.AddComment(new Comment("AnimalFan", "My heart can't take this cuteness!"));
        video3.AddComment(new Comment("MomOf3", "My kids watched this 10 times!"));
        video3.AddComment(new Comment("OtterLover", "Those little paws!!"));
        video3.AddComment(new Comment("NatureIsBest", "Pure joy in 3 minutes."));
        videos.Add(video3);

        Video video4 = new Video
        {
            Title = "LDS General Conference Highlights - April 2025",
            Author = "The Church of Jesus Christ",
            LengthInSeconds = 645
        };
        video4.AddComment(new Comment("Member4Life", "So inspired by President Nelson's talk!"));
        video4.AddComment(new Comment("NewConvert", "This changed my life. Thank you."));
        video4.AddComment(new Comment("FamilyForever", "Watching with my family right now!"));
        videos.Add(video4);

        Console.WriteLine("YouTube Video Tracker\n".PadLeft(40));
        Console.WriteLine(new string('=', 60));

        foreach (Video video in videos)
        {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.GetLengthFormatted()} ({video.LengthInSeconds} seconds)");
            Console.WriteLine($"Number of Comments: {video.GetCommentCount()}");
            Console.WriteLine("Comments:");
            
            foreach (Comment comment in video.GetComments())
            {
                Console.WriteLine($"  - {comment}");
            }
            
            Console.WriteLine(new string('-', 60));
        }

        Console.WriteLine("Program complete. Press any key to exit...");
        Console.ReadKey();
    }
}