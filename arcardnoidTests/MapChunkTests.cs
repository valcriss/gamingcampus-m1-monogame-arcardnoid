
using arcardnoid.Models.Content.Components.Map.Models;
using SharpDX.Direct2D1;

namespace arcardnoidTests
{
    [TestClass]
    public class MapChunkTests
    {
        private const string CHUNK_DIRECTORY = @"C:\Users\Z019817\Documents\GitHub\gamingcampus-m1-monogame-arcardnoid\arcardnoid\Maps\Chunks";

        [TestMethod]
        public void TestChunkJsonLayers()
        {
            string[] layers = new string[] { "WaterSplash", "Terrain Layer 1", "Shadow Layer", "Terrain Layer 2", "Deco Layer", "Bridge Layer 1", "Bridge Layer 2", "Building Layer", "Actor Layer" };
            foreach (string file in Directory.GetFiles(CHUNK_DIRECTORY, "*.json", SearchOption.AllDirectories))
            {
                MapChunk mapChunk = MapChunk.FromFile(file);
                Assert.IsNotNull(mapChunk);
                foreach (string layer in layers)
                {
                    Assert.AreNotEqual(mapChunk.Layers.FirstOrDefault(c => c.Name == layer), null, file + $" missing layer {layer}");                    
                }
                int names = mapChunk.Layers.Select(c => c.Name).ToList().Count();
                Assert.AreEqual(names,9, file + $" invalid layer count {names}");
                int invalidNames = mapChunk.Layers.Select(c => c.Name).Where(c=>!layers.Contains(c)).ToList().Count();
                Assert.AreEqual(invalidNames, 0, file + $" invalid layer names {invalidNames}");
                foreach(MapLayer mapLayer in mapChunk.Layers)
                {
                    Assert.AreEqual(mapLayer.Data.Length, mapChunk.Height, file + $" invalid layer height {mapLayer.Name}");
                    foreach(string layerLine in mapLayer.Data)
                    {
                        Assert.AreEqual(layerLine.Split(',').Length,mapChunk.Width, file + $" invalid layer width {mapLayer.Name}");
                    }
                }

                Assert.AreNotEqual(0,mapChunk.Entrances.Count, file + $" invalid entrance count 0");
            }
        }
    }
}