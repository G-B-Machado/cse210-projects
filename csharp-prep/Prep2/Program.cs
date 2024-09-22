using System;

class Program
{
    static void Main(string[] args)
    {   
        string letter;

        Console.Write("What is your grade percentage? ");
        
        int grade = int.Parse(Console.ReadLine());

        if (grade >= 90){
            letter = "A";
        } else if (grade >= 80){
            letter = "B";
        } else if (grade >= 70){
            letter = "C";
        } else if (grade >= 60){
            letter = "D";
        } else {
            letter = "F";
        }

        Console.WriteLine($"Your letter grade is: {letter}");

        if(grade >= 70){
            Console.WriteLine("Congratulations! You Passed!");
        } else{
            Console.WriteLine("Try to work hard and you will get it next year.");
        }
    }
}