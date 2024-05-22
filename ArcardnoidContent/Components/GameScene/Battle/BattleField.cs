using ArcardnoidContent.Components.GamePlay;
using ArcardnoidContent.Components.Shared.Map;
using ArcardnoidContent.Components.Shared.Map.Cells;
using ArcardnoidContent.Components.Shared.Map.Enums;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;

namespace ArcardnoidContent.Components.GameScene.Battle
{
    public class BattleField : GameComponent
    {
        #region Private Properties

        private IGamePlay GamePlay => GameServiceProvider.GetService<IGamePlay>();
        private AnimatedStaticMap StaticMap { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public BattleField(GroundType ground, int x, int y) : base(x, y, 1664, 1080)
        {
            StaticMap = AddGameComponent(new AnimatedStaticMap(ground == GroundType.Grass ? "Maps/grassmap.json" : "Maps/sandmap.json", 256, 60, MapAnimationEnded, true));
        }

        #endregion Public Constructors

        #region Public Methods

        public void MapAnimationEnded()
        {
            System.Diagnostics.Debug.WriteLine("Map animation ended");
        }

        public void ShowMap(EncounterType encounterType, double distanceFromStart)
        {
            AddUnits(encounterType, distanceFromStart);
            StaticMap.AddTilesAnimation();
        }

        #endregion Public Methods

        #region Private Methods

        private void AddUnits(EncounterType encounterType, double distanceFromStart)
        {
            ITexture playerAsset = LoadAssetTexture("map/units/player-battle");
            ITexture opponentAsset = LoadAssetTexture(AssetPath(encounterType));
            int numberOfOpponents = NumberOfOpponents(distanceFromStart);
            int[,] opponentField = GenerateField(numberOfOpponents);
            int[,] playerField = GenerateField(GamePlay.GetUnits());

            AddUnitsComponents(opponentField, opponentAsset, encounterType, 96, 136);
            AddUnitsComponents(playerField, playerAsset, EncounterType.None, 96, 576);
        }

        private void AddUnitsComponents(int[,] positions, ITexture asset, EncounterType encounterType, int positionX, int positionY)
        {
            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    if (positions[x, y] == 1)
                    {
                        StaticMap.AddGameComponent(new AnimatedCell(asset, GetTextureColumns(encounterType), 1, 120, 0, 0, x, y, positionX + x * 64, positionY + y * 64, 0, 0));
                    }
                }
            }
        }

        private string AssetPath(EncounterType encounterType)
        {
            switch (encounterType)
            {
                case EncounterType.Archer:
                    return "map/units/archer-blue-idle";

                case EncounterType.Warrior:
                    return "map/units/warrior-blue-idle";

                case EncounterType.Torch:
                    return "map/units/torch-red-idle";

                case EncounterType.Tnt:
                    return "map/units/tnt-red-idle";
            }
            return "map/units/archer-blue-idle";
        }

        private int[,] GenerateField(int numberOfUnits)
        {
            // Répartir de maniere homogène les unités sur le terrain
            int[,] field = new int[20, 5];
            int left = numberOfUnits;
            int lines = (int)Math.Ceiling(numberOfUnits / (float)20);
            for (int y = 0; y < lines; y++)
            {
                int inLine = Math.Min(20, left);
                for (int x = 10 - inLine / 2; x < 10 + inLine / 2; x++)
                {
                    field[x, y] = 1;
                }
                left -= inLine;
            }
            return field;
        }

        private int GetTextureColumns(EncounterType encounterType)
        {
            switch (encounterType)
            {
                case EncounterType.Torch:
                    return 7;

                default:
                    return 6;
            }
        }

        private ITexture LoadAssetTexture(string asset)
        {
            return GameServiceProvider.GetService<ITextureService>().Load(asset);
        }

        private int NumberOfOpponents(double distanceFromStart)
        {
            System.Diagnostics.Debug.WriteLine($"Distance from start: {distanceFromStart}");
            float ratio = (float)distanceFromStart / 30;
            System.Diagnostics.Debug.WriteLine($"Ratio: {ratio}");
            int value = Math.Max(10, (int)Math.Min(ratio * (20 * 5), (20 * 5)));
            System.Diagnostics.Debug.WriteLine($"Value: {value}");
            // Si la valeur n'est pas pair on la rend pair
            return value % 2 == 0 ? value : value + 1;
        }

        #endregion Private Methods
    }
}