using MonoGame.Extended;
using System.Collections.Generic;
using System.Linq;

namespace arcardnoid.Models.Content.Components.Map
{
    public static class MapExtensions
    {
        #region Public Methods

        public static int[,] ToMapArray(this string[] layer, int with, int height)
        {
            int[,] map = new int[with, height];
            for (int x = 0; x < with; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    map[x, y] = -1;
                }
            }

            for (int y = 0; y < layer.Length; y++)
            {
                string[] line = layer[y].Split(',');
                for (int x = 0; x < line.Length; x++)
                {
                    string index = line[x];
                    if (index.Trim() == "XX")
                    {
                        map[x, y] = 1;
                    }
                    else if (index.Trim() != "")
                    {
                        map[x, y] = int.Parse(index.Trim());
                    }
                }
            }

            return map;
        }

        public static string[] ToStringData(this int[,] data)
        {
            List<string> list = new List<string>();
            for (int y = 0; y < data.GetLength(1); y++)
            {
                string line = "";
                for (int x = 0; x < data.GetLength(0); x++)
                {
                    if (data[x, y] == -1)
                    {
                        line += ",";
                    }
                    else
                    {
                        line += data[x, y].ToString() + ",";

                    }
                }
                list.Add(line);
            }
            return list.ToArray();
        }

        public static List<T> RandomList<T>(this List<T> list, FastRandom random)
        {
            if (list == null || list.Count == 0) return list;
            return list.OrderBy(c=> random.Next()).ToList();
        }

        #endregion Public Methods
    }
}