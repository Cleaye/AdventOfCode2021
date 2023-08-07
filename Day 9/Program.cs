using System;
using System.Collections.Generic;
using System.Linq;

class HelloWorld
{
    public static int BasinSize = 0;
    public static int ElementsPerRow = 0;

    static void Main()
    {
        string[] lines = System.IO.File.ReadAllLines(@"F:\Repos\AdventOfCode2021\Day 9\Day9.txt");
        //string[] lines = System.IO.File.ReadAllLines(@"F:\Repos\AdventOfCode2021\Day 9\Day9Test.txt");
        ElementsPerRow = lines[0].Length;

        int[] sequenceAbove = new int[0];
        int[] sequenceBelow = new int[0];
        int[] sequence = Array.ConvertAll(lines[0].ToArray(), c => (int)Char.GetNumericValue(c));

        // Challenge #1
        // List<int> lowPoints = new List<int>(); 

        // Challenge #2
        List<int[]> dataSet = new List<int[]>();

        // Save all the coordinates of the lowpoints
        List<int> lowPointsX = new List<int>();
        List<int> lowPointsY = new List<int>();

        // Retrieve low points
        for (int y = 0; y < lines.Length; y++)
        {
            if (y > 0) // Save the previous sequence
                sequenceAbove = sequence;

            sequence = Array.ConvertAll(lines[y].ToArray(), c => (int)Char.GetNumericValue(c));
            dataSet.Add(sequence); // Challenge #2

            if (y + 1 < lines.Length)
                sequenceBelow = Array.ConvertAll(lines[y + 1].ToArray(), c => (int)Char.GetNumericValue(c));
            else
                sequenceBelow = new int[0];

            for (int x = 0; x < ElementsPerRow; x++)
            {
                List<int> filter = new List<int>(); // Array to store the values of the 8 surrounding elements
                
                if (sequence.Length != 0) 
                {
                    if (sequenceAbove.Length != 0)
                        filter.Add(sequenceAbove[x]);
                    if (sequenceBelow.Length != 0)
                        filter.Add(sequenceBelow[x]);
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

                if (smallestNumber)
                {
                    // Challenge #1
                    // lowPoints.Add(sequence[x]); 

                    lowPointsX.Add(x);
                    lowPointsY.Add(y);
                }
            }
        }

        // Challenge #1
        /*int sum = 0;
        foreach (int i in lowPoints)
            sum += i + 1;*/

        // Console.WriteLine(sum);

        // For every low point, figure out the size of the basin
        List<int> BasinSizes = new List<int>();
        for (int i = 0; i < lowPointsY.Count; i++)
        {
            CheckBasinSize(lowPointsX.ElementAt(i), lowPointsY.ElementAt(i), dataSet);
            BasinSizes.Add(BasinSize);
            BasinSize = 0; // Reset
        }

        BasinSizes.Sort(); 
        BasinSizes.Reverse();
        Console.WriteLine(BasinSizes.ElementAt(0) * BasinSizes.ElementAt(1) * BasinSizes.ElementAt(2));
    }

    static void CheckBasinSize(int xCoord, int yCoord, List<int[]> dataSet)
    {
        // Out of bounds
        if (xCoord < 0 || yCoord < 0 || xCoord >= ElementsPerRow || yCoord >= dataSet.Count) 
            return;

        int element = dataSet.ElementAt(yCoord)[xCoord];
        if (element >= 9) // Reached end of basin
            return;
        else
        {
            dataSet.ElementAt(yCoord)[xCoord] = 9; // Prevent checking this node again
            BasinSize = BasinSize + 1;
            CheckBasinSize(xCoord + 1, yCoord, dataSet);
            CheckBasinSize(xCoord - 1, yCoord, dataSet);
            CheckBasinSize(xCoord, yCoord + 1, dataSet);
            CheckBasinSize(xCoord, yCoord - 1, dataSet);
        }
    }
}