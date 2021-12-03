using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    class Program
    {
        static void Main(string[] args)
        {
            // Day 1
            Console.WriteLine(String.Format("Day 1 p. 1: {0}", GetDepthIncreases()));
            Console.WriteLine(String.Format("Day 1 p. 2: {0}", GetThreesDepthIncreases()));

            // Day 2
            Console.WriteLine(String.Format("Day 2 p. 1: {0}", GetFinalDepthMult()));
            Console.WriteLine(String.Format("Day 2 p. 2: {0}", GetFinalAimDepthMult()));

            // Day 3
            Console.WriteLine(String.Format("Day 3 p. 1: {0}", GetGammaEpsilon()));
            Console.WriteLine(String.Format("Day 3 p. 2: {0}", GetLifeSupport()));
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

        static int GetFinalDepthMult()
        {
            string filename = "day2inputs.txt";
            int hor = 0;
            int depth = 0;

            string[] contents = File.ReadAllLines(filename);
            foreach (string line in contents)
            {
                string[] dat = line.Split(" ");
                int increase = Convert.ToInt32(dat[1]);

                if (dat[0] == "forward")
                {
                    hor += increase;
                }
                else
                {
                    depth = (dat[0] == "up") ? (depth - increase) : (depth + increase);
                }
            }

            return hor * depth;
        }

        static int GetFinalAimDepthMult()
        {
            string filename = "day2inputs.txt";
            int hor = 0;
            int depth = 0;
            int aim = 0;

            string[] contents = File.ReadAllLines(filename);
            foreach (string line in contents)
            {
                string[] dat = line.Split(" ");
                int increase = Convert.ToInt32(dat[1]);

                if (dat[0] == "forward")
                {
                    hor += increase;
                    depth = depth + (aim * increase);
                }
                else
                {
                    aim = (dat[0] == "up") ? (aim - increase) : (aim + increase);
                }
            }

            return hor * depth;
        }

        static int GetGammaEpsilon()
        {
            string filename = "day3inputs.txt";
            string gammabin = "";
            string epsilonbin = "";

            string[] contents = File.ReadAllLines(filename);
            int len = contents[0].Length;

            for (int i = 0; i < len; i++)
            {
                int zero = 0;
                int one = 0;

                foreach (string line in contents)
                {
                    if (line[i].Equals('0'))
                    {
                        zero++;
                    }
                    else
                    {
                        one++;
                    }
                }

                gammabin = gammabin + ((zero > one) ? "0" : "1");
                epsilonbin = epsilonbin + ((zero > one) ? "1" : "0");
            }

            int gamma = Convert.ToInt32(gammabin, 2);
            int epsilon = Convert.ToInt32(epsilonbin, 2);

            return gamma * epsilon;
        }

        static void ClearNonMatchingBitCriteria(List<string> contents, char common, int pos)
        {
            for (int i = 0; i < contents.Count; i++)
            {
                Console.WriteLine("comparing " + contents[i][pos] + " to " + common);
                if (!contents[i][pos].Equals(common))
                {
                    Console.WriteLine("Removing " + contents[i]);
                    contents.RemoveAt(i);
                }
            }

        }

        static void GetRating(List<string> contents, bool isoxygen)
        {
            for (int i = 0; i < contents[0].Length; i++)
            {
                if (!isoxygen)
                {
                    Console.WriteLine("Checking position: " + i);
                }

                int zero = 0;
                int one = 0;

                foreach (string line in contents)
                {
                    if (line[i].Equals('0'))
                    {
                        zero++;
                    }
                    else
                    {
                        one++;
                    }
                }

                if (!isoxygen)
                {
                    Console.WriteLine("Zero: " + zero);
                    Console.WriteLine("One: " + one);
                }

                if (contents.Count > 1)
                {
                    ClearNonMatchingBitCriteria(contents, zero > one ? (isoxygen ? '0' : '1') : (isoxygen ? '1' : '0'), i);
                }

                if (!isoxygen)
                {
                    Console.WriteLine("Kept:");

                    foreach (string line in contents)
                    {
                        Console.WriteLine(line);
                    }
                }
            }
        }

        static int GetLifeSupport()
        {
            string filename = "day3inputs.txt";

            string[] contents = File.ReadAllLines(filename);
            List<string> finaloxygen = contents.ToList();
            List<string> finalco2 = contents.ToList();

            GetRating(finaloxygen, true);
            GetRating(finalco2, false);

            Console.WriteLine(finaloxygen[0]);
            Console.WriteLine(finalco2[0]);

            return Convert.ToInt32(finaloxygen[0], 2) * Convert.ToInt32(finalco2[0], 2);
        }
    }
}
