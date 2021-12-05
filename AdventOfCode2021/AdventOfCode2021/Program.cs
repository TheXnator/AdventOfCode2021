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

            // Day 4
            Console.WriteLine(String.Format("Day 4 p. 1: {0}", GetSquidBingoScore(true)));
            Console.WriteLine(String.Format("Day 4 p. 2: {0}", GetSquidBingoScore(false)));
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
            List<string> toremove = new List<string>();

            for (int i = 0; i < contents.Count; i++)
            {
                if (!contents[i][pos].Equals(common))
                {
                    toremove.Add(contents[i]);
                }
            }

            foreach (string rem in toremove)
            {
                contents.Remove(rem);
            }
        }

        static void GetRating(List<string> contents, bool isoxygen)
        {
            for (int i = 0; i < contents[0].Length; i++)
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

                if (contents.Count > 1)
                {
                    ClearNonMatchingBitCriteria(contents, zero > one ? (isoxygen ? '0' : '1') : (isoxygen ? '1' : '0'), i);
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

            return Convert.ToInt32(finaloxygen[0], 2) * Convert.ToInt32(finalco2[0], 2);
        }

        static int CalculateBoardScore(List<List<string>> bingoboards, List<List<string>> bingoboardsmarked, int i, int bingonum)
        {
            List<string> winningboard = bingoboards[i];
            int boardscore = 0;
            for (int boardpos = 0; boardpos < winningboard.Count; boardpos++)
            {
                if (bingoboardsmarked[i][boardpos].Equals("0"))
                {
                    boardscore += Convert.ToInt32(winningboard[boardpos]);
                }
            }

            return boardscore * bingonum;
        }

        static int GetSquidBingoScore(bool dofirst)
        {
            string filename = "day4inputs.txt";

            string[] contents = File.ReadAllLines(filename);
            string bingonums = contents[0];
            List<List<string>> bingoboards = new List<List<string>>();
            List<List<string>> bingoboardsmarked = new List<List<string>>();
            List<Dictionary<int, int[]>> bingoscores = new List<Dictionary<int, int[]>>();
            List<int> wonboards = new List<int>();

            // generate bingo boards
            for (int i = 1; i < contents.Length; i++)
            {
                if (contents[i].Contains(" "))
                {
                    foreach (string num in contents[i].Split(" "))
                    {
                        if (!num.Equals(""))
                        {
                            bingoboards[bingoboards.Count - 1].Add(num);
                            bingoboardsmarked[bingoboardsmarked.Count - 1].Add("0");
                        }
                    }
                }
                else
                {
                    bingoboards.Add(new List<string>());
                    bingoboardsmarked.Add(new List<string>());
                    bingoscores.Add(new Dictionary<int, int[]> { { 1, new int[5] }, { 2, new int[5] } });
                }
            }

            // play bingo
            foreach (string bingonum in bingonums.Split(","))
            {
                for (int i = 0; i < bingoboards.Count; i++)
                {
                    for (int x = 0; x < bingoboards[i].Count; x++)
                    {
                        if (bingoboards[i][x].Equals(bingonum))
                        {
                            bingoboardsmarked[i][x] = "1";

                            int rownum = (int)Math.Floor((double)(x / 5));
                            int colnum = x % 5;

                            bingoscores[i][1][rownum]++;
                            bingoscores[i][2][colnum]++;

                            if (bingoscores[i][1][rownum] == 5 || bingoscores[i][2][colnum] == 5)
                            {
                                int boardscore = CalculateBoardScore(bingoboards, bingoboardsmarked, i, Convert.ToInt32(bingonum));

                                if (!wonboards.Contains(i)) { wonboards.Add(i); }
                                if (dofirst) { return boardscore; }
                                if (wonboards.Count == bingoboards.Count) { return boardscore; }
                            }
                        }

                    }
                }
            }

            return 0;
        }
    }
}
