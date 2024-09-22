using System;
using System.Collections.Generic;


class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int>();
        int number;

        do
        {
            Console.Write("Enter number: ");
            number = int.Parse(Console.ReadLine());

            if (number != 0) 
            {
                numbers.Add(number);
            }

        } while (number != 0);

        int sum = 0;
        foreach (int num in numbers)
        {
            sum += num;
        }

        double average = (double)sum / numbers.Count;

        int largest = int.MinValue;
        int smallest = int.MaxValue;

        foreach (int num in numbers)
        {
            if (num > largest)
            {
                largest = num;
            }
            if (num < smallest)
            {
                smallest = num;
            }
        }

        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {average}");
        Console.WriteLine($"The largest number is: {largest}");
        Console.WriteLine($"The smallest number is: {smallest}");

        numbers.Sort();
        Console.WriteLine("The sorted list is:");
        foreach (int num in numbers)
        {
            Console.WriteLine(num);
        }

    }
}