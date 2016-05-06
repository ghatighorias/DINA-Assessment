using System;
using System.Collections.Generic;

namespace Assessment
{
    public class RomanNumeralParser
    {
        private List<RomanNumerals> parsedRomanNumerals;

        public RomanNumeralParser() { }

        public int CalculateRomanNumeralSet(params string[] RomanNumeralLetters)
        {
            int sum = 0;

            try
            {
                ParseRomanNumeralSet(RomanNumeralLetters);
                int firstIndex = 0;
                int secondIndex = 1;

                while (firstIndex < RomanNumeralLetters.Length)
                {
                    if (secondIndex == RomanNumeralLetters.Length) // handling the last letter in the queue
                        sum += AddNumerals(parsedRomanNumerals[firstIndex], RomanNumerals.INVALID);
                    else
                    {
                        if (parsedRomanNumerals[firstIndex] >= parsedRomanNumerals[secondIndex])
                            sum += AddNumerals(parsedRomanNumerals[firstIndex], RomanNumerals.INVALID);
                        else
                        {
                            sum += SubtractNumerals(parsedRomanNumerals[secondIndex], parsedRomanNumerals[firstIndex]);
                            firstIndex++;
                            secondIndex++;
                        }

                    }

                    firstIndex++;
                    secondIndex++;
                }
            }
            catch(ArgumentException e)
            {
                throw e;
            }
            return sum;
        }

        private void ParseRomanNumeralSet(params string[] RomanNumeralLetters)
        {
            RomanNumerals parsedItem;
            parsedRomanNumerals = new List<RomanNumerals>();

            foreach (var item in RomanNumeralLetters)
            {
                if (Enum.TryParse<RomanNumerals>(item, out parsedItem))
                    parsedRomanNumerals.Add(parsedItem);
                else
                    throw new ArgumentException("The input is not a valid Roman Numeral series");
            }
        }

        public bool TryParse(params string[] RomanNumeralLetters)
        {
            RomanNumerals parsedItem;

            foreach (var item in RomanNumeralLetters)
            {
                if (!Enum.TryParse<RomanNumerals>(item.Trim(), out parsedItem))
                    return false;
            }

            return true;
        }

        private int AddNumerals(RomanNumerals first, RomanNumerals second)
        {
            return (int)first + (int)second;
        }

        private int SubtractNumerals(RomanNumerals first, RomanNumerals second)
        {
            return (int)first - (int)second;
        }
    }
}
