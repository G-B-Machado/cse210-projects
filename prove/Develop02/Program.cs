using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class Program
{
    static void Main()
    {
        Journal journal = new Journal();
        bool running = true;

        Console.WriteLine("Welcome to the Journal!");
        Console.WriteLine("Do you want to load an existing journal or create a new one?");
        Console.WriteLine("1. Load existing journal");
        Console.WriteLine("2. Create new journal");
        Console.Write("Choose an option: ");
        
        string initialChoice = Console.ReadLine();

        switch (initialChoice)
        {
            case "1":
                journal.LoadFromFile();
                break;
            case "2":
                Console.WriteLine("New journal created.");
                break;
            default:
                Console.WriteLine("Invalid option. Creating a new journal by default.");
                break;
        }

        while (running)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Write new entry");
            Console.WriteLine("2. Display journal");
            Console.WriteLine("3. Save journal to file");
            Console.WriteLine("4. Load journal from file");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");
            
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    journal.AddEntry();
                    break;
                case "2":
                    journal.DisplayEntries();
                    break;
                case "3":
                    journal.SaveToFile();
                    break;
                case "4":
                    journal.LoadFromFile();
                    break;
                case "5":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }
}

class Journal
{
    private List<Entry> entries;
    private List<string> prompts;

    public Journal()
    {
        entries = new List<Entry>();
        prompts = new List<string>
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I could redo one thing today, what would it be?",
            "Write down what blessings you received today!",
            "What important ideas and thoughts did I have today?"
        };
    }

    public void AddEntry()
    {
        Random random = new Random();
        string prompt = prompts[random.Next(prompts.Count)];
        Console.WriteLine(prompt);
        Console.Write("Your response: ");
        string response = Console.ReadLine();
        entries.Add(new Entry(prompt, response, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
    }

    public void DisplayEntries()
    {
        if (entries.Count == 0)
        {
            Console.WriteLine("The journal is empty.");
            return;
        }

        foreach (var entry in entries)
        {
            Console.WriteLine($"Date: {entry.Date}");
            Console.WriteLine($"Question: {entry.Prompt}");
            Console.WriteLine($"Response: {entry.Response}");
            Console.WriteLine();
        }
    }

    public void SaveToFile()
    {
        Console.Write("Enter the filename to save (with .json extension): ");
        string filename = Console.ReadLine();
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);

        string jsonString = JsonSerializer.Serialize(entries, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, jsonString);

        Console.WriteLine($"Journal successfully saved to: {filePath}");
    }

    public void LoadFromFile()
    {
        Console.Write("Enter the filename to load (with .json extension): ");
        string filename = Console.ReadLine();
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);

        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);
            entries = JsonSerializer.Deserialize<List<Entry>>(jsonString);
            Console.WriteLine($"Journal successfully loaded from: {filePath}");
            DisplayEntries();
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }
}

class Entry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Date { get; set; }

    public Entry(string prompt, string response, string date)
    {
        Prompt = prompt;
        Response = response;
        Date = date;
    }
}