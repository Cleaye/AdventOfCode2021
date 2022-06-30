using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeOfAdvent2021
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] textFile = System.IO.File.ReadAllLines(@"advent3.txt");
            List<string> selection = textFile.ToList();

            for(int i = 0; i < textFile[0].Length; i++)
            {
                if (selection.Count == 1)
                    break;

                selection = CheckCollection(selection, i);
            }
                
            
            foreach (string line in selection)
                Console.WriteLine(line);

            Console.ReadKey();
        }

        public static List<string> CheckCollection(List<string> selection, int bitPosition)
        {
            List<string> newSelection = new List<string>();

            bool oneIsMostCommon = CheckMostCommonBit(selection, bitPosition);
            char number = oneIsMostCommon ? '1' : '0';
            foreach (string line in selection)
            {
                if (line[bitPosition].Equals(number))
                    newSelection.Add(line);
            }

            return newSelection;
        }

        public static bool CheckMostCommonBit(List<string> lines, int bitPosition)
        {
            int[] counts = new int[lines[0].Length];
            float totalLinesHalf = lines.Count * 0.5f;

            foreach (string line in lines)
            {
                if (line[bitPosition].Equals('1'))
                    counts[bitPosition]++;
            }

            if (counts[bitPosition] >= totalLinesHalf)
                return false; // 1 is the most common

            return true; // 0 is the most common
        }
    }
}
