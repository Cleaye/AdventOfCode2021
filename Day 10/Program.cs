using System;
using System.Collections.Generic;
using System.Linq;

class HelloWorld
{
    static void Main()
    {
        //string[] lines = System.IO.File.ReadAllLines(@"F:\Repos\AdventOfCode2021\Day 10\Day10Test.txt");
        string[] lines = System.IO.File.ReadAllLines(@"F:\Repos\AdventOfCode2021\Day 10\Day10.txt");

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

            int characters = sequence.Count;
            int remainingChars = characters;

            for (int i = 0; i < characters; i++)
            {
                if (i >= sequence.Count)
                    break;

                if (closing.Contains(sequence.ElementAt(i)))
                {
                    if (Array.IndexOf(opening, sequence.ElementAt(i - 1)) != Array.IndexOf(closing, sequence.ElementAt(i))) // Illegal character found
                    {
                        points += characterValues.ContainsKey(sequence.ElementAt(i)) ? characterValues[sequence.ElementAt(i)] : 0;
                        Console.WriteLine(line);
                        break;
                    }
                    else 
                    {
                        sequence.RemoveAt(i);
                        sequence.RemoveAt(i - 1);

                        if (i - 2 >= 0)
                            i -= 2;
                        else
                            i--;
                    }
                }
            }
        }

        Console.WriteLine(points);
    }
}