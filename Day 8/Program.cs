using System;
using System.Collections.Generic;
using System.Linq;

class HelloWorld
{
    static void Main()
    {
        string[] lines = System.IO.File.ReadAllLines(@"F:\Repos\AdventOfCode2021\Day 8\Day8.txt");
        List<string> sequences = new List<string>();

        foreach (string line in lines)
            sequences.Add(line);

        AnalyzeSegment("acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf");
        int count = 0;
        foreach(string sequence in sequences)
        {
            string[] segment = sequence.Split(new string[] { " | " }, StringSplitOptions.None);
            Dictionary<string, string> openWith = AnalyzeSegment(segment[0]);

            string[] split = segment[1].Split(' ');
            foreach(string part in split)
            {
                if(part.Length == 2 || part.Length == 3 || part.Length == 4 || part.Length == 7)
                    count++;

                Console.WriteLine(String.Concat(part.OrderBy(c => c)));
            }
        }

        Console.WriteLine(count);
    }

    static Dictionary<string, string> AnalyzeSegment(string signal)
    {
        Dictionary<char, char> wiring = new Dictionary<char, char>();
        wiring.Add('a', ' '); //  aaaa GET
        wiring.Add('b', ' '); // b    c   GET
        wiring.Add('c', ' '); // b    c
        wiring.Add('d', ' '); //  dddd    GET
        wiring.Add('e', ' '); // e    f   GET
        wiring.Add('f', ' '); // e    f
        wiring.Add('g', ' '); //  gggg

        string[] patterns = signal.Split(' ');
        List<string> zerosixnine = new List<string>(); // 0, 6, 9 uses 7 segments
        List<string> twothreefive = new List<string>(); // 2, 3, 5 uses 6 segments
        string one = "";
        string two = "";
        string three = "";
        string four = "";
        string five = "";
        string seven = "";
        string eight = "";

        // First figure out the unique patterns
        foreach (string pattern in patterns)
        {
            switch (pattern.Length)
            {
                case 2:
                    {
                        one = pattern;
                        break;
                    }
                case 3:
                    {
                        seven = pattern;
                        break;
                    }
                case 4:
                    {
                        four = pattern;
                        break;
                    }
                case 5:
                    {
                        twothreefive.Add(pattern);
                        break;
                    }
                case 6:
                    {
                        // Can be 0, 6 or 9
                        zerosixnine.Add(pattern);
                        break;
                    }
                case 7:
                    {
                        eight = pattern;
                        break;
                    }
                default:
                    break;

            }
        }

        // Figure out 'a'
        foreach (char c in seven)
        {
            if (!one.Contains(c))
                wiring['a'] = c;
        }

        // Figure out 'c'
        foreach (char c in one)
        {
            // Console.WriteLine(c);
            foreach (string s in zerosixnine)
            {
                if (!s.Contains(c))
                    wiring['c'] = c;
            }
        }

        // Figure out 'f'
        foreach (char c in seven)
        {
            if (wiring[c] == ' ')
                wiring['f'] = c;
        }

        // Figure out 'd'
        char[] knownInFour = { wiring['c'], wiring['f'] };
        string possibleD = four.Trim(knownInFour);
        foreach (char c in possibleD)
        {
            foreach (string s in zerosixnine)
            {
                if (!s.Contains(c))
                    wiring['d'] = c;
            }
        }

        // Figure out 'g'
        /*foreach (string s in twothreefive)
        {
            char[] test = { wiring['c'], wiring['f'] };
            acfd = String.Concat(acfd.OrderBy(c => c));
            string s_ordered = String.Concat(s.OrderBy(c => c));
            if (s_ordered.Contains(acfd))
            {
                wiring['g'] = s_ordered.Trim(acfd);
            }
        }*/

        Console.WriteLine(wiring['a']);
        Console.WriteLine(wiring['c']);
        Console.WriteLine(wiring['f']);
        Console.WriteLine(wiring['d']);
        Console.WriteLine(wiring['g']);

        return null;
    }
}