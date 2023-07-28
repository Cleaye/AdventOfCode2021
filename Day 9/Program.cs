using System;
using System.Collections.Generic;
using System.Linq;

class HelloWorld
{
    static void Main()
    {
        string[] lines = System.IO.File.ReadAllLines(@"F:\Repos\AdventOfCode2021\Day 9\Day9.txt");
        //string[] lines = System.IO.File.ReadAllLines(@"F:\Repos\AdventOfCode2021\Day 9\Day9Test.txt");
        int elementsPerRow = lines[0].Length;

        int[] sequenceAbove = new int[0];
        int[] sequenceBelow = new int[0];
        int[] sequence = Array.ConvertAll(lines[0].ToArray(), c => (int)Char.GetNumericValue(c));
        List<int> lowPoints = new List<int>();

        for (int y = 0; y < lines.Length; y++)
        {
            if (y > 0) // Save the previous sequence
                sequenceAbove = sequence;

            sequence = Array.ConvertAll(lines[y].ToArray(), c => (int)Char.GetNumericValue(c));

            if (y + 1 < lines.Length)
                sequenceBelow = Array.ConvertAll(lines[y + 1].ToArray(), c => (int)Char.GetNumericValue(c));
            else
                sequenceBelow = new int[0];

            for (int x = 0; x < elementsPerRow; x++)
            {
                List<int> filter = new List<int>(); // Array to store the values of the 8 surrounding elements
                
                if (sequence.Length != 0) // Get the 3 elements above
                {
                    if (sequenceAbove.Length != 0)
                    {
                        /*if (x - 1 >= 0)
                            filter.Add(sequenceAbove[x - 1]);
                        if (x + 1 < sequence.Length)
                            filter.Add(sequenceAbove[x + 1]);*/
                        filter.Add(sequenceAbove[x]);
                    }

                    if (sequenceBelow.Length != 0)
                    {
                        /*if (x - 1 >= 0)
                            filter.Add(sequenceBelow[x - 1]);
                        if (x + 1 < sequence.Length)
                            filter.Add(sequenceBelow[x + 1]);*/
                        filter.Add(sequenceBelow[x]);
                    }

                    if (x - 1 >= 0)
                        filter.Add(sequence[x - 1]);
                    if (x + 1 < sequence.Length)
                        filter.Add(sequence[x + 1]);
                }

                bool smallestNumber = true;
                foreach (int i in filter)
                {
                    if (sequence[x] >= i)
                    {
                        smallestNumber = false;
                        break;
                    }
                }

                if(smallestNumber)
                    lowPoints.Add(sequence[x]);

                /*foreach (int i in filter)
                    Console.Write(i);

                Console.Write(" ");
                Console.WriteLine(sequence[x]);*/
            }
        }

        /*foreach (int i in lowPoints)
            Console.WriteLine(i);*/

        int sum = 0;
        foreach (int i in lowPoints)
            sum += i + 1;

        Console.WriteLine(sum);
    }

    static bool CheckIfLowpoint()
    {
        return false;
    }
        
}