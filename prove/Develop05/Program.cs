using System;
using System.Threading;

// Base class for activities
abstract class MindfulnessActivity
{
    protected int Duration;
    protected string Name;
    protected string Description;

    public void StartActivity()
    {
        Console.WriteLine($"Starting activity: {Name}");
        Console.WriteLine(Description);
        Console.Write("Please enter the duration of the activity in seconds: ");
        Duration = int.Parse(Console.ReadLine());
        Console.WriteLine("Prepare to begin...");
        ShowSpinner(3);

        ExecuteActivity();

        Console.WriteLine("Good job! You have completed the activity.");
        Console.WriteLine($"Activity: {Name}, Duration: {Duration} seconds.");
        ShowSpinner(3);
    }

    protected abstract void ExecuteActivity();

    protected void ShowSpinner(int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            Console.Write(".");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }
}

// Class for breathing activity
class BreathingActivity : MindfulnessActivity
{
    public BreathingActivity()
    {
        Name = "Breathing Activity";
        Description = "This activity will help you relax by guiding you through slow and deep breathing.";
    }

    protected override void ExecuteActivity()
    {
        int halfDuration = Duration / 2;
        for (int i = 0; i < halfDuration; i++)
        {
            Console.WriteLine("Breathe in...");
            ShowCountdown(3);
            Console.WriteLine("Breathe out...");
            ShowCountdown(3);
        }
    }

    private void ShowCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write(i + " ");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }
}

// Class for reflection activity
class ReflectionActivity : MindfulnessActivity
{
    private string[] prompts = {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private string[] questions = {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience?",
        "What did you learn about yourself?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectionActivity()
    {
        Name = "Reflection Activity";
        Description = "This activity will help you reflect on times when you showed strength and resilience.";
    }

    protected override void ExecuteActivity()
    {
        Random random = new Random();
        string prompt = prompts[random.Next(prompts.Length)];
        Console.WriteLine(prompt);
        ShowSpinner(3);

        int questionDuration = Duration / questions.Length;
        foreach (string question in questions)
        {
            Console.WriteLine(question);
            ShowSpinner(questionDuration);
        }
    }
}

// Class for listing activity
class ListingActivity : MindfulnessActivity
{
    private string[] prompts = {
        "Who are people you appreciate?",
        "What are your personal strengths?",
        "Who are people you have helped this week?",
        "When have you felt something spiritual this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity()
    {
        Name = "Listing Activity";
        Description = "This activity will help you reflect on the good things in your life by listing them.";
    }

    protected override void ExecuteActivity()
    {
        Random random = new Random();
        string prompt = prompts[random.Next(prompts.Length)];
        Console.WriteLine(prompt);
        ShowSpinner(3);

        Console.WriteLine("Start listing items:");
        DateTime endTime = DateTime.Now.AddSeconds(Duration);
        int itemCount = 0;

        while (DateTime.Now < endTime)
        {
            Console.Write("Item: ");
            Console.ReadLine();
            itemCount++;
        }

        Console.WriteLine($"You listed {itemCount} items.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Select an activity:");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Exit");
            Console.Write("Choice: ");
            string choice = Console.ReadLine();

            MindfulnessActivity activity = null;

            switch (choice)
            {
                case "1":
                    activity = new BreathingActivity();
                    break;
                case "2":
                    activity = new ReflectionActivity();
                    break;
                case "3":
                    activity = new ListingActivity();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    continue;
            }

            activity.StartActivity();
        }
    }
}