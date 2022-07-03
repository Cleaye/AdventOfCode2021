using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int maxX = 989;
            int maxY = 989;
            // int maxX = 9;
            // int maxY = 9;

            // Set up matrix
            List<List<int>> matrix = new List<List<int>>();
            List<int> xCoord = new List<int>();

            // Assign 0 values in matrix
            for (int i = 0; i <= maxX; i++)
            {
                for (int j = 0; j <= maxY; j++)
                    xCoord.Add(0);

                matrix.Add(xCoord);
                xCoord = new List<int>();
            }

            string[] lines = System.IO.File.ReadAllLines(@"F:\Repos\AdventOfCode2021\Day 5\Advent5.txt");

            foreach (string line in lines)
            {
                // 0 = x, 1 = y
                string[] coordinates = line.Split(new string[] { " -> " }, StringSplitOptions.None);
                string[] x = coordinates[0].Split(',');
                string[] y = coordinates[1].Split(',');

                if (x[0] == y[0]) // Vertical line
                {
                    AddLineInMatrix(matrix, false, Int32.Parse(x[0]), Int32.Parse(x[1]), Int32.Parse(y[1]));
                    continue;
                }
                else if (x[1] == y[1]) // Horizontal line
                {
                    AddLineInMatrix(matrix, true, Int32.Parse(x[1]), Int32.Parse(x[0]), Int32.Parse(y[0]));
                    continue;
                }
                else // Diagonal
                {
                    AddDiagonalInMatrix(matrix, x, y);
                }
            }


            // Print result
            string text = "";
            foreach (List<int> x in matrix)
            {
                foreach (int number in x)
                {
                    if (number == 0)
                        text += ".";
                    else
                        text += number.ToString();
                }

                // Console.WriteLine(text);
                text = "";
            }

            int count = 0;
            foreach (List<int> x in matrix)
            {
                foreach (int number in x)
                {
                    if (number > 1)
                        count++;
                }
            }

            Console.WriteLine(count);
            Console.Read();
        }

        static void AddLineInMatrix(List<List<int>> matrix, bool isHorizontal, int linePosition, int lineStart, int lineEnd)
        {
            if (lineStart > lineEnd)
            {
                int temp = lineStart;
                lineStart = lineEnd;
                lineEnd = temp;
            }

            if (isHorizontal)
            {
                List<int> row = matrix[linePosition];
                for (int i = lineStart; i <= lineEnd; i++)
                {
                    row[i]++;
                }
            }
            else
            {
                for (int i = lineStart; i <= lineEnd; i++)
                {
                    List<int> column = matrix[i];
                    column[linePosition]++;
                }
            }
        }

        static void AddDiagonalInMatrix(List<List<int>> matrix, string[] xCoord, string[] yCoord)
        {
            int pos1x = Int32.Parse(xCoord[0]);
            int pos1y = Int32.Parse(xCoord[1]);
            int pos2x = Int32.Parse(yCoord[0]);
            int pos2y = Int32.Parse(yCoord[1]);

            // Diagonal: x++ && y++ || x-- && y--
            // If second coordinate is smaller, change the coordinates around
            if (pos2x < pos1x && pos2y < pos1y)
            {
                int temp = pos1x;
                pos1x = pos2x;
                pos2x = temp;

                temp = pos1y;
                pos1y = pos2y;
                pos2y = temp;
            }

            if (pos1x < pos2x && pos1y < pos2y)
            {
                for (int i = pos1y; i <= pos2y; i++) // starting column to ending column
                {
                    List<int> column = matrix[i];
                    column[pos1x]++;
                    pos1x++;
                }

                return;
            }

            // Diagonal: x++ || x-- && y++
            // For convenience, always draw line downwards
            if (pos2y < pos1y)
            {
                int temp = pos1x;
                pos1x = pos2x;
                pos2x = temp;

                temp = pos1y;
                pos1y = pos2y;
                pos2y = temp;
            }

            if (pos1x > pos2x)
            {
                for (int i = pos1y; i <= pos2y; i++) // starting column to ending column
                {
                    List<int> column = matrix[i];
                    column[pos1x]++;
                    pos1x--; // Go right
                }
            }
            else
            {
                for (int i = pos1y; i <= pos2y; i++) // starting column to ending column
                {
                    List<int> column = matrix[i];
                    column[pos1x]++;
                    pos1x++; // Go left
                }
            }
        }
    }
}
