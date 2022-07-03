using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_7
{
    internal class Program
    {
        static void Main()
        {
            string[] lines = System.IO.File.ReadAllLines(@"F:\Repos\AdventOfCode2021\Day 7\Day7.txt");
            string[] crabs = lines[0].Split(','); // 1000 crabs
            // Console.WriteLine(lines[0]);

            //string test = "16,1,2,0,4,2,7,1,2,14";
            //string[] crabs = test.Split(',');

            List<int> crabList = new List<int>();

            // First input
            foreach (string crab in crabs)
                crabList.Add(Int32.Parse(crab));


            bool foundAnswer = false;
            int currentEfficient = Int32.MaxValue;
            int startPosition = (int)(crabs.Length * 0.5f);

            while (!foundAnswer)
            {
                int less = CalculateFuel(crabList, startPosition - 1);
                int more = CalculateFuel(crabList, startPosition + 1);

                if (less < currentEfficient)
                {
                    currentEfficient = less;
                    startPosition--;
                }
                else if (more < currentEfficient)
                {
                    currentEfficient = more;
                    startPosition++;
                }
                else
                    foundAnswer = true;
            }

            Console.WriteLine(currentEfficient);
            Console.WriteLine(startPosition);
            Console.Read();
        }

        static int CalculateFuel(List<int> CrabList, int position)
        {
            List<int> fuelList = new List<int>();
            foreach (int crab in CrabList)
                fuelList.Add(Math.Abs(crab - position));

            int fuelSpent = 0;
            // Challenge 1
            //foreach (int fuel in fuelList)
            //    fuelSpent += fuel;

            foreach(int fuel in fuelList)
            {
                int startFuel = 1;
                for(int i = 0; i < fuel; i++)
                    fuelSpent += (startFuel++);
            }

            return fuelSpent;
        }
    }
}
