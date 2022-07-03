using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_6
{
    internal class Program
    {
        static void Main()
        {

            string[] lines = System.IO.File.ReadAllLines(@"F:\Repos\AdventOfCode2021\Day 6\Day6.txt");
            Console.WriteLine(lines[0]);

            string[] fishes = lines[0].Split(',');
            List<long> fishList = new List<long>();

            // Initial list of fishes
            for (int i = 0; i < 9; i++)
                fishList.Add(0);

            // First input
            foreach (string fish in fishes)
                fishList[Int32.Parse(fish)]++;

            int daysToSimulate = 256;
            while (daysToSimulate > 0)
            {
                fishList = SimulateDay(fishList);
                daysToSimulate--;
            }

            long sum = 0;
            foreach (long number in fishList)
                sum += number;

            Console.WriteLine(sum);
            Console.Read();
        }

        static List<long> SimulateDay(List<long> FishList)
        {
            List<long> tempList = new List<long>();
            // Initial list of fishes
            for (int i = 0; i < 9; i++)
                tempList.Add(0);

            tempList[0] += FishList[1]; // 0
            tempList[1] += FishList[2]; // 1
            tempList[2] += FishList[3]; // 2
            tempList[3] += FishList[4]; // 3
            tempList[4] += FishList[5]; // 4
            tempList[5] += FishList[6]; // 5
            tempList[6] += (FishList[7] + FishList[0]); // 6
            tempList[7] += FishList[8]; // 7
            tempList[8] += FishList[0]; // 8


            return tempList;
        }
    }
}
