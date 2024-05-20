using arcardnoid.Models.Framework.Tools;
using ArcardnoidContent.Components.GamePlay;
using ArcardnoidContent.Components.Shared.Map;
using ArcardnoidContent.Components.Shared.Map.Cells;
using ArcardnoidContent.Components.Shared.Map.Enums;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;
using ArcardnoidShared.Framework.Tools;

namespace ArcardnoidContent.Components.GameScene.Battle
{
    public class BattleField : GameComponent
    {
        #region Private Properties

        private AnimatedStaticMap StaticMap { get; set; }

        private IGamePlay GamePlay => GameServiceProvider.GetService<IGamePlay>();
        #endregion Private Properties

        #region Public Constructors

        public BattleField(GroundType ground, int x, int y) : base(x, y, 1664, 1080)
        {
            StaticMap = AddGameComponent(new AnimatedStaticMap(ground == GroundType.Grass ? "Maps/grassmap.json" : "Maps/sandmap.json", 256, 60, true));
        }

        #endregion Public Constructors

        #region Public Methods

        public void ShowMap(EncounterType encounterType, double distanceFromStart)
        {
            AddUnits(encounterType, distanceFromStart);
            StaticMap.AddTilesAnimation();
        }

        private void AddUnits(EncounterType encounterType, double distanceFromStart)
        {
            ITexture playerAsset = LoadAssetTexture("map/units/player-battle");
            ITexture opponentAsset = LoadAssetTexture(AssetPath(encounterType));
            int numberOfOpponents = NumberOfOpponents(distanceFromStart);
            int[,] opponentField = GenerateField(numberOfOpponents);
            int[,] playerField = GenerateField(GamePlay.GetUnits());

            AddUnitsComponents(opponentField, opponentAsset, 352, 168);
            AddUnitsComponents(playerField, playerAsset, 352, 672);
        }

        private void AddUnitsComponents(int[,] positions, ITexture asset, int positionX, int positionY)
        {
            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    if (positions[x, y] == 1)
                    {
                        AddGameComponent(new AnimatedCell(asset, 6, 1, 120, 0, 0, x, y, positionX + x * 64, positionY + y * 64, 0, 0));
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

        private ITexture LoadAssetTexture(string asset)
        {
            return GameServiceProvider.GetService<ITextureService>().Load(asset);
        }

        private int[,] GenerateField(int numberOfUnits)
        {
            // Répartir de maniere homogène les unités sur le terrain
            int[,] field = new int[20, 5];
            for (int i = 0; i < numberOfUnits; i++)
            {
                int x = i % 20;
                int y = i / 20;
                field[x, y] = 1;
            }
            return field;
        }

        private int NumberOfOpponents(double distanceFromStart)
        {
            float ratio = (float)distanceFromStart / 30;
            return (int)(ratio * (20 * 5));
        }

        private int[,] InitializeBattleSide()
        {
            int[,] encountersPosition = new int[20, 5];
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    encountersPosition[i, j] = 1;
                }
            }
            return encountersPosition;
        }

        #endregion Public Methods
    }
}