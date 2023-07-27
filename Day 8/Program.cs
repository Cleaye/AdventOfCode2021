using System;
using System.Collections.Generic;
using System.Linq;

class HelloWorld
{
    static void Main()
    {
        string[] lines = System.IO.File.ReadAllLines(@"F:\Repos\AdventOfCode2021\Day 8\Day8.txt");
        // string[] lines = System.IO.File.ReadAllLines(@"F:\Repos\AdventOfCode2021\Day 8\Day8Test.txt");
        List<string> sequences = new List<string>();

        foreach (string line in lines)
            sequences.Add(line);

        int count = 0;
        foreach(string sequence in sequences)
        {
            string[] segment = sequence.Split(new string[] { " | " }, StringSplitOptions.None);
            Dictionary<char, char> sequenceWiring = AnalyzeSegment(segment[0]);
            Dictionary<string, int> wiring = TranslateToNumbers(sequenceWiring);
            Console.WriteLine(DecodeOutput(wiring, segment[1]));

            count += DecodeOutput(wiring, segment[1]);
        }

        Console.WriteLine(count);
    }

    static Dictionary<char, char> AnalyzeSegment(string signal)
    {
        Dictionary<char, char> wiring = new Dictionary<char, char>();
        wiring.Add('a', ' '); 
        wiring.Add('b', ' '); 
        wiring.Add('c', ' '); 
        wiring.Add('d', ' '); 
        wiring.Add('e', ' '); 
        wiring.Add('f', ' '); 
        wiring.Add('g', ' '); 

        string[] patterns = signal.Split(' ');
        List<string> zerosixnine = new List<string>();
        List<string> twothreefive = new List<string>();

        // Can be deducted immediately
        string one = "";
        string four = "";
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

        // Unique segment occurrences
        // a = 8x
        // b = 6x
        // c = 8x
        // d = 7x
        // e = 4x
        // f = 9x
        // g = 7x

        Dictionary<char, int> charCounts = new Dictionary<char, int>();
        foreach(string s in patterns)
        {
            foreach (char c in s)
                if (!charCounts.ContainsKey(c))
                    charCounts.Add(c, 1);
                else
                {
                    int count = charCounts[c] + 1;
                    charCounts[c] = count;
                }
        }

        // Figure out the unique segment occurrences
        wiring['b'] = charCounts.FirstOrDefault(x => x.Value == 6).Key;
        wiring['e'] = charCounts.FirstOrDefault(x => x.Value == 4).Key;
        wiring['f'] = charCounts.FirstOrDefault(x => x.Value == 9).Key;

        // Figure out 'c'
        string letterC = one;
        letterC = String.Join("", letterC.Split(wiring['f']));
        wiring['c'] = letterC.ToCharArray()[0];

        // Figure out 'a'
        string letterA = seven;
        letterA = String.Join("", letterA.Split(wiring['c'], wiring['f']));
        wiring['a'] = letterA.ToCharArray()[0];

        // Figure out 'd'
        string letterD = four;
        letterD = String.Join("", letterD.Split(wiring['b'], wiring['c'], wiring['f']));
        wiring['d'] = letterD.ToCharArray()[0];

        // Figure out 'g'
        string letterG = eight;
        letterG = String.Join("", letterG.Split(wiring['a'], wiring['b'], wiring['c'], wiring['d'], wiring['e'], wiring['f']));
        wiring['g'] = letterG.ToCharArray()[0];

        return wiring;
    }

    static Dictionary<string, int> TranslateToNumbers(Dictionary<char, char> decodedWiring)
    {
        string entry = "";
        Dictionary<string, int> numberTranslation = new Dictionary<string, int>();

        entry = decodedWiring['a'].ToString() + decodedWiring['b'].ToString() + decodedWiring['c'].ToString() + decodedWiring['e'].ToString() + decodedWiring['f'].ToString() + decodedWiring['g'].ToString();
        entry = String.Concat(entry.OrderBy(c => c));
        numberTranslation.Add(entry, 0);

        entry = decodedWiring['c'].ToString() + decodedWiring['f'].ToString();
        entry = String.Concat(entry.OrderBy(c => c));
        numberTranslation.Add(entry, 1);

        entry = decodedWiring['a'].ToString() + decodedWiring['c'].ToString() + decodedWiring['d'].ToString() + decodedWiring['e'].ToString() + decodedWiring['g'].ToString();
        entry = String.Concat(entry.OrderBy(c => c));
        numberTranslation.Add(entry, 2);

        entry = decodedWiring['a'].ToString() + decodedWiring['c'].ToString() + decodedWiring['d'].ToString() + decodedWiring['f'].ToString() + decodedWiring['g'].ToString();
        entry = String.Concat(entry.OrderBy(c => c));
        numberTranslation.Add(entry, 3);

        entry = decodedWiring['b'].ToString() + decodedWiring['c'].ToString() + decodedWiring['d'].ToString() + decodedWiring['f'].ToString();
        entry = String.Concat(entry.OrderBy(c => c));
        numberTranslation.Add(entry, 4);

        entry = decodedWiring['a'].ToString() + decodedWiring['b'].ToString() + decodedWiring['d'].ToString() + decodedWiring['f'].ToString() + decodedWiring['g'].ToString();
        entry = String.Concat(entry.OrderBy(c => c));
        numberTranslation.Add(entry, 5);

        entry = decodedWiring['a'].ToString() + decodedWiring['b'].ToString() + decodedWiring['d'].ToString() + decodedWiring['e'].ToString() + decodedWiring['f'].ToString() + decodedWiring['g'].ToString();
        entry = String.Concat(entry.OrderBy(c => c));
        numberTranslation.Add(entry, 6);

        entry = decodedWiring['a'].ToString() + decodedWiring['c'].ToString() + decodedWiring['f'].ToString();
        entry = String.Concat(entry.OrderBy(c => c));
        numberTranslation.Add(entry, 7);

        entry = decodedWiring['a'].ToString() + decodedWiring['b'].ToString() + decodedWiring['c'].ToString() + decodedWiring['d'].ToString() + decodedWiring['e'].ToString() + decodedWiring['f'].ToString() + decodedWiring['g'].ToString();
        entry = String.Concat(entry.OrderBy(c => c));
        numberTranslation.Add(entry, 8);

        entry = decodedWiring['a'].ToString() + decodedWiring['b'].ToString() + decodedWiring['c'].ToString() + decodedWiring['d'].ToString() + decodedWiring['f'].ToString() + decodedWiring['g'].ToString();
        entry = String.Concat(entry.OrderBy(c => c));
        numberTranslation.Add(entry, 9);

        return numberTranslation;
    }

    static int DecodeOutput(Dictionary<string, int> wiring, string output)
    {
        string outputValue = "";
        string[] split = output.Split(' ');

        foreach (string part in split)
            outputValue += wiring[String.Concat(part.OrderBy(c => c))];

        return Int32.Parse(outputValue);
    }
}