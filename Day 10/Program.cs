using System;
using System.Collections.Generic;
using System.Linq;

class HelloWorld
{
    static void Main()
    {
        //string[] lines = System.IO.File.ReadAllLines("..\\..\\..\\Day10Test.txt");
        string[] lines = System.IO.File.ReadAllLines("..\\..\\..\\Day10.txt");

        char[] opening = { '(', '[', '{', '<' };
        char[] closing = { ')', ']', '}', '>' };

        Dictionary<char, int> characterValues = new Dictionary<char, int>
        {
            { ')', 3 },
            { ']', 57 },
            { '}', 1197 },
            { '>', 25137 }
        };

        Dictionary<char, int> characterValuesPart2 = new Dictionary<char, int>
        {
            { '(', 1 },
            { '[', 2 },
            { '{', 3 },
            { '<', 4 }
        };

        int points = 0;
        List<Int64> autocompleteScores = new List<Int64>();

        foreach (string line in lines)
        {
            List<char> sequence = line.ToList();
            List<char> checkedChars = new List<char>();

            int characters = sequence.Count;
            bool illegal = false;

            for (int i = 0; i < characters; i++) 
            {
                if (closing.Contains(sequence.ElementAt(i))) // Part 1
                {
                    if (Array.IndexOf(opening, checkedChars.Last()) != Array.IndexOf(closing, sequence.ElementAt(i))) // Illegal character found
                    {
                        /* Part 1
                        points += characterValues.ContainsKey(sequence.ElementAt(i)) ? characterValues[sequence.ElementAt(i)] : 0;
                        Console.WriteLine(line);
                        break;
                        */

                        illegal = true;
                        break;
                    }
                    checkedChars.RemoveAt(checkedChars.Count - 1);
                }
                else
                    checkedChars.Add(sequence.ElementAt(i));
            }

            if (!illegal)
            {
                Int64 lineScore = 0;
                // Check how to complete incomplete lines
                for (int i = checkedChars.Count - 1; i >= 0; i--)
                {
                    lineScore *= 5;
                    lineScore += characterValuesPart2.ContainsKey(checkedChars[i]) ? characterValuesPart2[checkedChars[i]] : 0;
                }
                autocompleteScores.Add(lineScore);
            }
        }

        autocompleteScores.Sort();
        Console.WriteLine(autocompleteScores.Count);
        Console.WriteLine(autocompleteScores[(int)Math.Floor(autocompleteScores.Count * 0.5)]);
        //Console.WriteLine(points);
    }
}