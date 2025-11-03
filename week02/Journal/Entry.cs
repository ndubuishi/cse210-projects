using System;

public class Entry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Date { get; set; }
    public string Mood { get; set; }

    public Entry(string prompt, string response, string date, string mood)
    {
        Prompt = prompt;
        Response = response;
        Date = date;
        Mood = mood;
    }

    public override string ToString()
    {
        return $"Date: {Date} | Mood: {Mood}\nPrompt: {Prompt}\nResponse: {Response}\n";
    }
}