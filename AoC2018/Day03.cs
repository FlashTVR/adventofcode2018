﻿using System;
using System.Collections.Generic;
using System.IO;

namespace AoC2018
{
    class Day03 : ISolution
    {
        private static readonly string[] SplitChars = new string[] { " @ ", ",", ": ", "x" };

        private string[] input;

        public void LoadInput(params string[] files)
        {
            input = File.ReadAllLines(files[0]);
        }

        public object Part1()
        {
            var coords = new HashSet<(int x, int y)>();
            var overlap = new HashSet<(int x, int y)>();
            foreach (var l in input)
            {
                var parts = l.Split(SplitChars, StringSplitOptions.None);
                var left = int.Parse(parts[1]);
                var top = int.Parse(parts[2]);
                var width = int.Parse(parts[3]);
                var height = int.Parse(parts[4]);
                for (var x = left; x < width + left; x++)
                {
                    for (var y = top; y < height + top; y++)
                    {
                        if (!coords.Add((x, y)))
                        {
                            overlap.Add((x, y));
                        }
                    }
                }
            }

            return overlap.Count;
        }

        public object Part2()
        {
            var ids = new HashSet<int>();
            foreach (var l in input)
            {
                ids.Add(int.Parse(l.Substring(1, l.IndexOf(' '))));
            }

            var map = new Dictionary<(int x, int y), List<int>>();
            foreach (var l in input)
            {
                var parts = l.Split(SplitChars, StringSplitOptions.None);
                var left = int.Parse(parts[1]);
                var top = int.Parse(parts[2]);
                var width = int.Parse(parts[3]);
                var height = int.Parse(parts[4]);
                for (var x = left; x < width + left; x++)
                {
                    for (var y = top; y < height + top; y++)
                    {
                        List<int> list;
                        if (!map.TryGetValue((x, y), out list))
                        {
                            list = new List<int>();
                        }

                        list.Add(int.Parse(parts[0].Substring(1)));
                        map[(x, y)] = list;
                    }
                }
            }

            foreach (var pos in map.Values)
            {
                if (pos.Count > 1)
                {
                    ids.RemoveWhere((int i) => pos.Contains(i));
                }
            }

            return new List<int>(ids)[0];
        }
    }
}
