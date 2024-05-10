using arcardnoid.Models.Content.Components.Map.Models;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace arcardnoid.Models.Content.Components.Map
{
    public static class MapExtensions
    {
        #region Public Methods

        public static bool IsEmpty(this MapLayer layer, int x, int y)
        {
            return layer.GetLayerData(x, y) == "";
        }

        public static double Distance(this Point point1, Point point2)
        {
            return Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2));
        }

        public static double Distance(this Vector2 point1, Vector2 point2)
        {
            return Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2));
        }


        public static MapLayer Trim(this MapLayer layer, int startX, int startY, int endX, int endY)
        {
            List<string> lines = new List<string>();
            for (int y = 0; y < layer.Data.Length; y++)
            {
                if (y < startY || y > endY) continue;
                string line = layer.Data[y];
                string[] tab = line.Split(',');
                string result = string.Empty;
                for (int x = 0; x < tab.Length; x++)
                {
                    if (x < startX || x > endX) continue;
                    string data = layer.GetLayerData(x, y);
                    result += data + ",";
                }
                if (result != "")
                {
                    lines.Add(result);
                }
            }
            return new MapLayer()
            {
                Name = layer.Name,
                Data = lines.ToArray()
            };
        }

        public static int GetChunkStartX(this MapChunk chunk)
        {
            int startX = int.MaxValue;
            foreach (MapLayer layer in chunk.Layers)
            {
                for (int y = 0; y < chunk.Height; y++)
                {
                    for (int x = 0; x < chunk.Width; x++)
                    {
                        string data = layer.GetLayerData(x, y);
                        if (data != "")
                        {
                            startX = MathHelper.Min(startX, x);
                        }
                    }
                }
            }
            return startX;
        }

        public static int GetChunkEndX(this MapChunk chunk)
        {
            int endX = 0;
            foreach (MapLayer layer in chunk.Layers)
            {
                for (int y = 0; y < chunk.Height; y++)
                {
                    for (int x = 0; x < chunk.Width; x++)
                    {
                        string data = layer.GetLayerData(x, y);
                        if (data != "")
                        {
                            endX = MathHelper.Max(x, endX);
                        }
                    }
                }
            }
            return endX;
        }

        public static int GetChunkStartY(this MapChunk chunk)
        {
            int startY = int.MaxValue;
            foreach (MapLayer layer in chunk.Layers)
            {
                for (int y = 0; y < chunk.Height; y++)
                {
                    for (int x = 0; x < chunk.Width; x++)
                    {
                        string data = layer.GetLayerData(x, y);
                        if (data != "")
                        {
                            startY = MathHelper.Min(startY, y);
                        }
                    }
                }
            }
            return startY;
        }

        public static int GetChunkEndY(this MapChunk chunk)
        {
            int endY = 0;
            foreach (MapLayer layer in chunk.Layers)
            {
                for (int y = 0; y < chunk.Height; y++)
                {
                    for (int x = 0; x < chunk.Width; x++)
                    {
                        string data = layer.GetLayerData(x, y);
                        if (data != "")
                        {
                            endY = MathHelper.Max(y, endY);
                        }
                    }
                }
            }
            return endY;
        }

        public static string GetLayerData(this MapLayer layer, int x, int y)
        {
            if (y < 0 || y >= layer.Data.Length) return "";
            string[] data = layer.Data[y].Split(',');
            if (x < 0 || x >= data.Length) return "";
            return data[x].Trim();
        }

        public static List<MapChunk> Clone(this List<MapChunk> list)
        {
            string content = JsonConvert.SerializeObject(list);
            return JsonConvert.DeserializeObject<List<MapChunk>>(content);
        }

        public static List<T> RandomList<T>(this List<T> list, FastRandom random)
        {
            if (list == null || list.Count == 0) return list;
            return list.OrderBy(c => random.Next()).ToList();
        }

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
                    else if (data[x, y] == 99)
                    {
                        line += "XX,";
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

        #endregion Public Methods
    }
}