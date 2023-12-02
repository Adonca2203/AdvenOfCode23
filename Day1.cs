using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day1
    {
        string pathToInput1 = "..\\..\\..\\Data\\Day1Problem1Input.txt";

        public int Problem1()
        {
            int currentSum = 0;
            string[] lines = File.ReadAllLines(pathToInput1);
            foreach (string line in lines)
            {
                string numbersOnly = new string(line.Where(c => char.IsDigit(c)).ToArray());
                if (numbersOnly.Length > 0)
                {
                    char[] num = { numbersOnly[0], numbersOnly[^1] };
                    string currentCalibration = new string(num);
                    currentSum += Int32.Parse(currentCalibration);
                }
            }
            return currentSum;
        }

        public int Problem2()
        {
            int currentSum = 0;
            string[] lines = File.ReadAllLines(pathToInput1);
            Dictionary<string, string> wordToNumberMap = new Dictionary<string, string>() {
                { "one", "1" }, { "two", "2"}, {"three", "3"}, {"four", "4"}, {"five", "5"},
                { "six", "6" }, { "seven", "7"}, { "eight", "8"}, { "nine", "9"}
            };

            foreach (string line in lines)
            {
                string first_digit = string.Empty;
                int first_digit_start_index = int.MaxValue;
                int last_digit_start_index = 0;
                string last_digit = string.Empty;
                foreach (string key in wordToNumberMap.Keys)
                {
                    if (line.Contains(key))
                    {
                        int windowSize = key.Length;
                        for (int i = 0; i <= line.Length - windowSize; i++)
                        {
                            string currentWord = line.Substring(i, windowSize);
                            if (currentWord != key) continue;
                            if (wordToNumberMap.TryGetValue(currentWord, out string value))
                            {
                                if (i < first_digit_start_index)
                                {
                                    first_digit_start_index = i;
                                    first_digit = value;
                                    if (last_digit_start_index <= first_digit_start_index)
                                    {
                                        last_digit = first_digit;
                                        last_digit_start_index = first_digit_start_index;
                                    }
                                }
                                else if (i >= last_digit_start_index)
                                {
                                    last_digit = value;
                                    last_digit_start_index = i;
                                    if (first_digit_start_index > last_digit_start_index)
                                    {
                                        first_digit = last_digit;
                                        first_digit_start_index = last_digit_start_index;
                                    }
                                }
                            }
                        }
                    }
                }

                foreach (char c in line)
                {
                    if (char.IsDigit(c))
                    {
                        if (line.IndexOf(c) < first_digit_start_index)
                        {
                            first_digit = c.ToString();
                            first_digit_start_index = line.IndexOf(c);
                            if (last_digit_start_index <= first_digit_start_index)
                            {
                                last_digit = first_digit;
                                last_digit_start_index = first_digit_start_index;
                            }
                        }
                        else if (line.LastIndexOf(c) > last_digit_start_index)
                        {
                            last_digit = c.ToString();
                            last_digit_start_index = line.LastIndexOf(c);
                            if (first_digit_start_index >= last_digit_start_index)
                            {
                                first_digit = last_digit;
                                first_digit_start_index = last_digit_start_index;
                            }
                        }
                    }
                }
                currentSum += Int32.Parse(first_digit + last_digit);
            }

            return currentSum;
        }
    }
}
