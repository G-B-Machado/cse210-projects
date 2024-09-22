using System;

class Program
{
    static void Main(string[] args)
    {
        Random random = new Random();
        int magic_number;
        int count;

        Console.WriteLine("Welcome to the Game!");

        while (true)
        {
            magic_number = random.Next(1, 101);
            count = 0;

            while (true)
            {
                count++;
                Console.Write("What is your guess? ");
                int guess = int.Parse(Console.ReadLine());

                if (magic_number == guess)
                {
                    Console.WriteLine("Congratulations! You win!!!");
                    Console.WriteLine($"You used {count} attempts.");

                    Console.Write("Do you want to play again? (Yes/No): ");
                    string answer = Console.ReadLine().Trim().ToLower();

                    if (answer != "yes")
                    {
                        Console.WriteLine("Thanks for playing!");
                        return;
                    }
                    else
                    {
                        break;
                    }
                }
                else if (magic_number > guess)
                {
                    Console.WriteLine("Higher");
                }
                else if (magic_number < guess)
                {
                    Console.WriteLine("Lower");
                }
            }
        }
    }
}