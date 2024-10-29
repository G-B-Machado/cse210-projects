using System;
using System.Collections.Generic;
using System.IO;

namespace EternalQuest
{
    abstract class Goal
    {
        protected string _shortName;
        protected string _description;
        protected int _points;

        public int Points => _points; // Propriedade pública para acessar _points

        public Goal(string name, string description, int points)
        {
            _shortName = name;
            _description = description;
            _points = points;
        }

        public abstract bool RecordEvent();
        public abstract bool IsComplete();
        public abstract string GetDetailsString();
        public abstract string GetStringRepresentation();
    }

    class SimpleGoal : Goal
    {
        private bool _isComplete;

        public SimpleGoal(string name, string description, int points)
            : base(name, description, points)
        {
            _isComplete = false;
        }

        public override bool RecordEvent()
        {
            if (!_isComplete)
            {
                _isComplete = true;
                return true;
            }
            return false;
        }

        public override bool IsComplete() => _isComplete;

        public override string GetDetailsString() => $"{_shortName}: {_description} - {_points} points";

        public override string GetStringRepresentation() => $"[SimpleGoal] {_shortName}: {_description} - Complete: {_isComplete}";
    }

    class EternalGoal : Goal
    {
        public EternalGoal(string name, string description, int points)
            : base(name, description, points)
        {
        }

        public override bool RecordEvent()
        {
            return false; // Eternal goals are never complete
        }

        public override bool IsComplete() => false;

        public override string GetDetailsString() => $"{_shortName}: {_description} - {_points} points each time";

        public override string GetStringRepresentation() => $"[EternalGoal] {_shortName}: {_description}";
    }

    class ChecklistGoal : Goal
    {
        private int _amountCompleted;
        private int _target;
        private int _bonus;

        public int Bonus => _bonus; // Propriedade pública para acessar _bonus

        public ChecklistGoal(string name, string description, int points, int target, int bonus)
            : base(name, description, points)
        {
            _amountCompleted = 0;
            _target = target;
            _bonus = bonus;
        }

        public override bool RecordEvent()
        {
            _amountCompleted++;
            return _amountCompleted >= _target;
        }

        public override bool IsComplete() => _amountCompleted >= _target;

        public override string GetDetailsString() => $"{_shortName}: {_description} - {_points} points each time, Bonus: {_bonus}, Completed {_amountCompleted}/{_target} times";

        public override string GetStringRepresentation() => $"[ChecklistGoal] {_shortName}: {_description} - Completed {_amountCompleted}/{_target} times";
    }

    class GoalManager
    {
        private List<Goal> _goals;
        private int _score;

        public GoalManager()
        {
            _goals = new List<Goal>();
            _score = 0;
        }

        public void Start()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("\nEternal Quest - Main Menu");
                Console.WriteLine("1. Display Player Info");
                Console.WriteLine("2. List Goals");
                Console.WriteLine("3. Create Goal");
                Console.WriteLine("4. Record Event");
                Console.WriteLine("5. Save Goals");
                Console.WriteLine("6. Load Goals");
                Console.WriteLine("7. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DisplayPlayerInfo();
                        break;
                    case "2":
                        ListGoalDetails();
                        break;
                    case "3":
                        CreateGoal();
                        break;
                    case "4":
                        RecordEvent();
                        break;
                    case "5":
                        SaveGoals();
                        break;
                    case "6":
                        LoadGoals();
                        break;
                    case "7":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        public void DisplayPlayerInfo()
        {
            Console.WriteLine($"Current Score: {_score}");
        }

        public void ListGoalNames()
        {
            foreach (var goal in _goals)
            {
                Console.WriteLine(goal.GetDetailsString());
            }
        }

        public void ListGoalDetails()
        {
            foreach (var goal in _goals)
            {
                Console.WriteLine(goal.GetStringRepresentation());
            }
        }

        public void CreateGoal()
        {
            Console.WriteLine("\nCreate a New Goal");
            Console.WriteLine("1. Simple Goal");
            Console.WriteLine("2. Eternal Goal");
            Console.WriteLine("3. Checklist Goal");
            Console.Write("Choose a goal type: ");
            string choice = Console.ReadLine();

            Console.Write("Enter goal name: ");
            string name = Console.ReadLine();
            Console.Write("Enter goal description: ");
            string description = Console.ReadLine();
            Console.Write("Enter points: ");
            int points = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case "1":
                    _goals.Add(new SimpleGoal(name, description, points));
                    break;
                case "2":
                    _goals.Add(new EternalGoal(name, description, points));
                    break;
                case "3":
                    Console.Write("Enter target count: ");
                    int target = int.Parse(Console.ReadLine());
                    Console.Write("Enter bonus points: ");
                    int bonus = int.Parse(Console.ReadLine());
                    _goals.Add(new ChecklistGoal(name, description, points, target, bonus));
                    break;
                default:
                    Console.WriteLine("Invalid choice. Goal not created.");
                    break;
            }
        }

        public void RecordEvent()
        {
            Console.WriteLine("\nRecord an Event");
            ListGoalNames();
            Console.Write("Enter the number of the goal to record: ");
            int index = int.Parse(Console.ReadLine());

            if (index >= 0 && index < _goals.Count)
            {
                Goal goal = _goals[index];
                if (goal.RecordEvent())
                {
                    if (goal is ChecklistGoal checklistGoal && checklistGoal.IsComplete())
                    {
                        _score += checklistGoal.Bonus;
                    }
                    _score += goal.Points;
                    Console.WriteLine("Event recorded!");
                }
                else
                {
                    Console.WriteLine("Goal already completed or does not apply.");
                }
            }
            else
            {
                Console.WriteLine("Invalid goal number.");
            }
        }

        public void SaveGoals()
        {
            using (StreamWriter writer = new StreamWriter("goals.txt"))
            {
                writer.WriteLine(_score);
                foreach (var goal in _goals)
                {
                    writer.WriteLine(goal.GetStringRepresentation());
                }
                Console.WriteLine("Goals saved.");
            }
        }

        public void LoadGoals()
        {
            if (File.Exists("goals.txt"))
            {
                using (StreamReader reader = new StreamReader("goals.txt"))
                {
                    _score = int.Parse(reader.ReadLine());
                    _goals.Clear();
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(' ');
                        string type = parts[0];
                        string name = parts[1];
                        string description = parts[2];
                        int points = int.Parse(parts[3]);

                        switch (type)
                        {
                            case "[SimpleGoal]":
                                bool isComplete = bool.Parse(parts[5]);
                                SimpleGoal simpleGoal = new SimpleGoal(name, description, points);
                                if (isComplete) simpleGoal.RecordEvent();
                                _goals.Add(simpleGoal);
                                break;
                            case "[EternalGoal]":
                                _goals.Add(new EternalGoal(name, description, points));
                                break;
                            case "[ChecklistGoal]":
                                int completed = int.Parse(parts[5].Split('/')[0]);
                                int target = int.Parse(parts[5].Split('/')[1]);
                                int bonus = int.Parse(parts[7]);
                                ChecklistGoal checklistGoal = new ChecklistGoal(name, description, points, target, bonus);
                                for (int i = 0; i < completed; i++) checklistGoal.RecordEvent();
                                _goals.Add(checklistGoal);
                                break;
                        }
                    }
                    Console.WriteLine("Goals loaded.");
                }
            }
            else
            {
                Console.WriteLine("No saved goals found.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            GoalManager manager = new GoalManager();
            manager.Start();
        }
    }
}