﻿using System;
using System.IO;

namespace AdventOfCode2021
{
    class Program
    {
        static void Main(string[] args)
        {
            // Day 1
            Console.WriteLine(String.Format("Day 1 p. 1: {0}", GetDepthIncreases()));
            Console.WriteLine(String.Format("Day 1 p. 2: {0}", GetThreesDepthIncreases()));
        }

        static int GetDepthIncreases()
        {
            string filename = "day1inputs.txt";
            int increases = 0;
            int lastval = 0;

            string[] contents = File.ReadAllLines(filename);
            foreach (string line in contents)
            {
                int val1 = Convert.ToInt32(line);

                if (val1 > lastval)
                {
                    increases++;
                }

                lastval = val1;
            }

            return (increases - 1);
        }

        static int GetThreesDepthIncreases()
        {
            string filename = "day1inputs.txt";
            int increases = 0;
            int lastval = 0;

            string[] contents = File.ReadAllLines(filename);
            for (int i = 0; i < contents.Length; i++)
            {
                if (contents.Length >= i + 3)
                {
                    int val1 = Convert.ToInt32(contents[i]);
                    int val2 = Convert.ToInt32(contents[i+1]);
                    int val3 = Convert.ToInt32(contents[i+2]);

                    int sum = val1 + val2 + val3;
                    if (sum > lastval)
                    {
                        increases++;
                    }

                    lastval = sum;
                }
            }

            return (increases - 1);
        }
    }
}
