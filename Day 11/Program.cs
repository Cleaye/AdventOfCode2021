using System;
using System.Collections.Generic;
using System.Linq;

class Octopus
{
    public int Value { get; set; } = 0;
    public List<Octopus> Neighbors { get; set; } = new List<Octopus>();
    private bool Flashed = false;

    public Octopus(int value)
    {
        Value = value;
    }    

    public void Flash()
    {
        foreach (Octopus neighbor in Neighbors)
            neighbor.Energize();
    }

    public void Energize()
    {
        if(!Flashed)
            Value++;

        if (Value > 9)
        {
            Value = 0;
            Flashed = true;
            Flash();
        }
    }

    public void Refresh()
    {
        Flashed = false;
    }
}

class HelloWorld
{
    static void Main()
    {
        const int GRID_SIZE = 10;
        // string[] lines = System.IO.File.ReadAllLines("..\\..\\..\\Day11.txt");
        string[] lines = System.IO.File.ReadAllLines("..\\..\\..\\Day11Test.txt");
        Octopus[,] octopusGrid = new Octopus[GRID_SIZE, GRID_SIZE];

        for (int y = 0; y < GRID_SIZE; y++)
        {
            char[] sequence = lines[y].ToArray();
            for (int x = 0; x < GRID_SIZE; x++)
                octopusGrid[y, x] = new Octopus(sequence[x] - '0');
        }

        int[] dr = { -1, 1, 0, 0, -1, 1, -1, 1 };
        int[] dc = { 0, 0, -1, 1, -1, 1, 1, -1 };
        for (int y = 0; y < GRID_SIZE; y++)
        {
            for (int x = 0; x < GRID_SIZE; x++)
            {
                for (int i = 0; i < dr.Length; i++)
                {
                    int newRow = x + dr[i];
                    int newCol = y + dc[i];

                    // Check if the neighbor is within bounds
                    if (newRow >= 0 && newRow < GRID_SIZE && newCol >= 0 && newCol < GRID_SIZE)
                        octopusGrid[y, x].Neighbors.Add(octopusGrid[newRow, newCol]);
                }
            }
        }

        int steps = 2;
        while(steps >= 0)
        {
            // First make each a step for each octopus
            for (int y = 0; y < GRID_SIZE; y++)
            {
                for (int x = 0; x < GRID_SIZE; x++)
                    octopusGrid[y, x].Energize();
            }

            // Make every octopus flashable again
            for (int y = 0; y < GRID_SIZE; y++)
            {
                for (int x = 0; x < GRID_SIZE; x++)
                    octopusGrid[y, x].Refresh();
            }

            // Check Results
            for (int y = 0; y < GRID_SIZE; y++)
            {
                for (int x = 0; x < GRID_SIZE; x++)
                    Console.Write(octopusGrid[y, x].Value + " ");

                Console.WriteLine();
            }

            Console.WriteLine();
            steps--;
        }
    }
}