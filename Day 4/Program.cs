using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;


namespace Day_4
{
    public class BingocardNumber
    {
        public int Number { get; set; }
        public bool Checked { get; set; }

        public BingocardNumber(int number, bool isChecked)
        {
            Number = number;
            Checked = isChecked;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            string[] text = System.IO.File.ReadAllLines(@"F:\Repos\AdventOfCode2021\Day 4\advent4.txt");
            string bingoNumbersText = text[0];

            // Skip element 0 (bingo numbers) and 1 (newline)
            int startOfBingoCards = 2;
            int bingoCardSize = 25;

            string bingoCardText = "";

            // Save all numbers in one line
            for (int i = startOfBingoCards; i < text.Length; i++)
            {
                if (text[i] == "")
                    continue;

                bingoCardText += text[i] + " ";
            }

            // Remove double spaces from single digit numbers
            var formattedNumbers = Regex.Split(bingoCardText, @"\s{2,}");
            string finalBingoCardString = "";

            foreach (string number in formattedNumbers)
            {
                if (number != "")
                    finalBingoCardString += number + " ";
            }

            // Convert text to individual numbers
            string[] allBingoCardNumbers = finalBingoCardString.Split(' ');
            // For some reason there is white space in the last 2 elements. Remove final white space
            List<string> bingoCardNumbersList = new List<string>(allBingoCardNumbers);
            allBingoCardNumbers = allBingoCardNumbers.Take(allBingoCardNumbers.Length - 2).ToArray();

            // Convert data to convenient format
            List<List<BingocardNumber>> AllBingoCards = new List<List<BingocardNumber>>();
            List<BingocardNumber> bingoCards = new List<BingocardNumber>();

            // Format to individual cards
            foreach (string number in allBingoCardNumbers)
            {
                if (bingoCards.Count != bingoCardSize)
                    bingoCards.Add(new BingocardNumber(Int32.Parse(number), false));
                else
                {
                    AllBingoCards.Add(bingoCards);
                    bingoCards = new List<BingocardNumber>();
                    bingoCards.Add(new BingocardNumber(Int32.Parse(number), false));
                }
            }

            // Add the last bingo card
            AllBingoCards.Add(bingoCards);

            List<BingocardNumber> Winner = null;
            int lastBingonumber = 0;

            // Cross off numbers and check for bingo
            string[] bingoNumbersInt = bingoNumbersText.Split(',');
            foreach (string bingoNumber in bingoNumbersInt)
            {
                foreach (List<BingocardNumber> card in AllBingoCards)
                {
                    CheckNumber(card, Int32.Parse(bingoNumber));
                    if (CheckBingo(card) != null)
                    {
                        Winner = card;
                        lastBingonumber = Int32.Parse(bingoNumber);
                        break;
                    }
                }

                if (Winner != null)
                    break;
            }

            int uncheckedSum = 0;
            foreach (BingocardNumber winnerCard in Winner)
            {
                if (winnerCard.Checked == false)
                    uncheckedSum += winnerCard.Number;
            }

            // Print answer
            Console.WriteLine(uncheckedSum * lastBingonumber);
        }

        static void CheckNumber(List<BingocardNumber> bingoCard, int bingoNumber)
        {
            foreach (BingocardNumber entry in bingoCard)
            {
                if (entry.Number == bingoNumber)
                    entry.Checked = true;
            }
        }

        static List<BingocardNumber> CheckBingo(List<BingocardNumber> bingoCard)
        {
            int numberOfRows = 5;
            int numberOfColumns = 5;

            int checks = 0;

            // First check horizontal
            // Row
            for (int i = 0; i < numberOfRows; i++)
            {
                for (int j = 0; j < numberOfColumns; j++)
                {
                    // Console.WriteLine(i * numberOfColumns + j);
                    if (bingoCard[(i * numberOfColumns) + j].Checked)
                        checks++;
                    else
                    {
                        checks = 0;
                        break;
                    }
                }

                // Horizontal Bingo!
                if (checks == numberOfColumns)
                    return bingoCard;
                else
                    continue;
            }

            // Check vertical
            for (int i = 0; i < numberOfRows; i++)
            {
                for (int j = 0; j < numberOfColumns; j++)
                {
                    //Console.WriteLine(j * numberOfColumns + i);
                    if (bingoCard[(j * numberOfColumns) + i].Checked)
                        checks++;
                    else
                    {
                        checks = 0;
                        break;
                    }
                }

                // Vertical Bingo!
                if (checks == numberOfRows)
                    return bingoCard;
                else
                    continue;
            }

            return null;
        }
    }
}
