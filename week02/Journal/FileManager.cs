using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

public class FileManager
{
    private string EscapeCsvField(string field)
    {
        if (string.IsNullOrEmpty(field)) return "";
        if (field.Contains(',') || field.Contains('"') || field.Contains('\n') || field.Contains('\r'))
            return $"\"{field.Replace("\"", "\"\"")}\"";
        return field;
    }

    private string[] ParseCsvLine(string line)
    {
        var result = new List<string>();
        var field = new StringBuilder();
        bool inQuotes = false;

        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];

            if (c == '"')
            {
                if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                {
                    field.Append('"');
                    i++;
                }
                else
                    inQuotes = !inQuotes;
            }
            else if (c == ',' && !inQuotes)
            {
                result.Add(field.ToString());
                field.Clear();
            }
            else
                field.Append(c);
        }
        result.Add(field.ToString());
        return result.ToArray();
    }

    public void SaveAsCsv(Journal journal, string filename)
    {
        var lines = new List<string> { "Date,Prompt,Response,Mood" };

        foreach (var e in journal.GetEntries())
        {
            string line = $"{EscapeCsvField(e.Date)},{EscapeCsvField(e.Prompt)},{EscapeCsvField(e.Response)},{EscapeCsvField(e.Mood)}";
            lines.Add(line);
        }

        File.WriteAllLines(filename, lines, Encoding.UTF8);
    }

    public void LoadFromCsv(Journal journal, string filename)
    {
        if (!File.Exists(filename)) throw new FileNotFoundException();

        var lines = File.ReadAllLines(filename, Encoding.UTF8);
        if (lines.Length == 0) return;

        for (int i = 1; i < lines.Length; i++) // skip header
        {
            var fields = ParseCsvLine(lines[i]);
            if (fields.Length >= 4)
                journal.AddEntry(new Entry(fields[1], fields[2], fields[0], fields[3]));
        }
    }

    public void SaveAsJson(Journal journal, string filename)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(journal.GetEntries(), options);
        File.WriteAllText(filename, json, Encoding.UTF8);
    }

    public void LoadFromJson(Journal journal, string filename)
    {
        if (!File.Exists(filename)) throw new FileNotFoundException();

        string json = File.ReadAllText(filename, Encoding.UTF8);
        var list = JsonSerializer.Deserialize<List<Entry>>(json);
        if (list != null)
            foreach (var e in list)
                journal.AddEntry(e);
    }
}