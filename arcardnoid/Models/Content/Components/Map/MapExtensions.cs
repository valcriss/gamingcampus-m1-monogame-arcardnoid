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
                    if (index != " ")
                    {
                        map[x, y] = int.Parse(index);
                    }
                }
            }

            return map;
        }

        #endregion Public Methods
    }
}