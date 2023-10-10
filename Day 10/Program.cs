using System;
using System.Collections.Generic;
using System.Linq;

class HelloWorld
{
    static void Main()
    {
        //string[] lines = System.IO.File.ReadAllLines(@"F:\Repos\AdventOfCode2021\Day 10\Day10Test.txt");
        string[] lines = System.IO.File.ReadAllLines("..\\..\\..\\Day10.txt");

        int points = 0;
        char[] opening = { '(', '[', '{', '<' };
        char[] closing = { ')', ']', '}', '>' };

        Dictionary<char, int> characterValues = new Dictionary<char, int>
        {
            { ')', 3 },
            { ']', 57 },
            { '}', 1197 },
            { '>', 25137 }
        };

        foreach (string line in lines)
        {
            List<char> sequence = line.ToList();
            Stack<char> checkedS = new Stack<char>();

            int characters = sequence.Count;
            int remainingChars = characters;

            for (int i = 0; i < characters; i++) // New Method
            {
                if (closing.Contains(sequence.ElementAt(i)))
                {
                    if (Array.IndexOf(opening, checkedS.Pop()) != Array.IndexOf(closing, sequence.ElementAt(i))) // Illegal character found
                    {
                        points += characterValues.ContainsKey(sequence.ElementAt(i)) ? characterValues[sequence.ElementAt(i)] : 0;
                        Console.WriteLine(line);
                        break;
                    }
                }
                else
                    checkedS.Push(sequence.ElementAt(i));
            }
        }

        Console.WriteLine(points);
    }
}