using ArcardnoidContent.Components.GamePlay;
using ArcardnoidContent.Components.GameScene.Battle.Bars;
using ArcardnoidContent.Components.GameScene.Battle.Enums;
using ArcardnoidContent.Components.Shared.Map;
using ArcardnoidContent.Components.Shared.Map.Cells;
using ArcardnoidContent.Components.Shared.Map.Enums;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;

namespace ArcardnoidContent.Components.GameScene.Battle
{
    public class BattleField : GameComponent
    {
        #region Private Properties

        private static IGamePlay GamePlay => GameServiceProvider.GetService<IGamePlay>();
        private Rectangle GameBounds { get; set; } = Rectangle.Empty;
        private bool OponentBallAttached => OponentFireBall == null || OponentFireBall.Attached;
        private OponentBattleBar? OponentBattleBar { get; set; }
        private FireBall? OponentFireBall { get; set; }
        private bool PlayerBallAttached => PlayerFireBall == null || PlayerFireBall.Attached;
        private PlayerBattleBar? PlayerBattleBar { get; set; }
        private FireBall? PlayerFireBall { get; set; }
        private AnimatedStaticMap StaticMap { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public BattleField(GroundType ground, int x, int y) : base(x, y, 1664, 1080)
        {
            GameBounds = new Rectangle(x + 256, y + 60, 22 * 64, 15 * 64);
            StaticMap = AddGameComponent(new AnimatedStaticMap(ground == GroundType.Grass ? "Maps/grassmap.json" : "Maps/sandmap.json", 256, 60, MapAnimationEnded, true));
        }

        #endregion Public Constructors

        #region Public Methods

        public void MapAnimationEnded()
        {
            System.Diagnostics.Debug.WriteLine("Map animation ended");
            PlayerBattleBar = AddGameComponent(new PlayerBattleBar(GameBounds)).Show(() => { BarAnimationCompleted(BattleFaction.Player); });
            OponentBattleBar = AddGameComponent(new OponentBattleBar(GameBounds)).Show(() => { BarAnimationCompleted(BattleFaction.Opponent); });
        }

        public void ShowMap(EncounterType encounterType, double distanceFromStart)
        {
            AddUnits(encounterType, distanceFromStart);
            StaticMap.AddTilesAnimation();
        }

        #endregion Public Methods

        #region Private Methods

        private static string AssetPath(EncounterType encounterType)
        {
            return encounterType switch
            {
                EncounterType.Archer => "map/units/archer-blue-idle",
                EncounterType.Warrior => "map/units/warrior-blue-idle",
                EncounterType.Torch => "map/units/torch-red-idle",
                EncounterType.Tnt => "map/units/tnt-red-idle",
                _ => "map/units/archer-blue-idle",
            };
        }

        private static int[,] GenerateField(int numberOfUnits)
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

        private static int GetTextureColumns(EncounterType encounterType)
        {
            return encounterType switch
            {
                EncounterType.Torch => 7,
                _ => 6,
            };
        }

        private static ITexture LoadAssetTexture(string asset)
        {
            return GameServiceProvider.GetService<ITextureService>().Load(asset);
        }

        private static int NumberOfOpponents(double distanceFromStart)
        {
            System.Diagnostics.Debug.WriteLine($"Distance from start: {distanceFromStart}");
            float ratio = (float)distanceFromStart / 30;
            System.Diagnostics.Debug.WriteLine($"Ratio: {ratio}");
            int value = Math.Max(10, (int)Math.Min(ratio * (20 * 5), (20 * 5)));
            System.Diagnostics.Debug.WriteLine($"Value: {value}");
            // Si la valeur n'est pas pair on la rend pair
            return value % 2 == 0 ? value : value + 1;
        }

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

        private void BarAnimationCompleted(BattleFaction faction)
        {
            if (faction == BattleFaction.Player)
                PlayerFireBall = AddGameComponent(new FireBall(BattleFaction.Player));
            else
                OponentFireBall = AddGameComponent(new FireBall(BattleFaction.Opponent));
        }

        #endregion Private Methods
    }
}