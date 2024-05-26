using ArcardnoidContent.Components.GamePlay;
using ArcardnoidContent.Components.GameScene.Battle.Bars;
using ArcardnoidContent.Components.GameScene.Battle.Enums;
using ArcardnoidContent.Components.Shared.Map;
using ArcardnoidContent.Components.Shared.Map.Cells;
using ArcardnoidContent.Components.Shared.Map.Enums;
using ArcardnoidShared.Framework.Components.Images;
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

        private static IGamePlay GamePlay => GameServiceProvider.GetService<IGamePlay>();
        private List<BattleColliderItem> ColliderItems { get; set; } = new List<BattleColliderItem>();
        private Rectangle GameBounds { get; set; } = Rectangle.Empty;
        private bool OponentBallAttached => OponentFireBall == null || OponentFireBall.Attached;
        private OponentBattleBar? OponentBattleBar { get; set; }
        private FireBall? OponentFireBall { get; set; }
        private bool PlayerBallAttached => PlayerFireBall == null || PlayerFireBall.Attached;
        private PlayerBattleBar? PlayerBattleBar { get; set; }
        private FireBall? PlayerFireBall { get; set; }
        private Primitive2D Primitive2D { get; set; }
        private AnimatedStaticMap StaticMap { get; set; }

        #endregion Private Properties

        #region Private Fields

        private List<GameComponent> _oponentComponents = new List<GameComponent>();
        private List<GameComponent> _playerComponents = new List<GameComponent>();

        #endregion Private Fields

        #region Public Constructors

        public BattleField(GroundType ground, int x, int y) : base(x, y, 1664, 1080)
        {
            GameBounds = new Rectangle(x + 256, y + 60, 22 * 64, 15 * 64);
            StaticMap = AddGameComponent(new AnimatedStaticMap(ground == GroundType.Grass ? "Maps/grassmap.json" : "Maps/sandmap.json", 256, 60, MapAnimationEnded, true));
            Primitive2D = AddGameComponent(new Primitive2D());
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

        public override void Update(float delta)
        {
            base.Update(delta);

            UpdateColliders();
            UpdatePlayerBall();
            UpdateDebug();
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
            _playerComponents.Clear();
            _oponentComponents.Clear();
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
                        var c = StaticMap.AddGameComponent(new AnimatedCell(asset, GetTextureColumns(encounterType), 1, 120, 0, 0, x, y, positionX + x * 64, positionY + y * 64, 0, 0));
                        if (encounterType == EncounterType.None)
                        {
                            _playerComponents.Add(c);
                        }
                        else
                        {
                            _oponentComponents.Add(c);
                        }
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

        private void CheckCollisionWithOthers(FireBall ball, BattleFaction faction, ColliderType type, out bool destroy)
        {
            destroy = false;
            foreach (BattleColliderItem item in ColliderItems)
            {
                if ((item.BounceFaction == faction && item.BounceType == type) || (item.DestroyFaction == faction && item.DestroyType == type))
                {
                    RectangleFace face = item.Bounds.Collide(ball.RealBounds);
                    if (face != RectangleFace.None)
                    {
                        CollidingPlane plane = face == RectangleFace.Left || face == RectangleFace.Right ? CollidingPlane.Vertical : CollidingPlane.Horizontal;
                        ball.ColideWithPlane(plane, false);
                        return;
                    }
                }
            }
        }

        private CollidingPlane CollideWithGameBounds(Rectangle bounds, BattleFaction faction)
        {
            if ((bounds.X <= GameBounds.X) || (bounds.X >= GameBounds.X + GameBounds.Width - 16))
            {
                return CollidingPlane.Vertical;
            }
            else if (bounds.Y <= GameBounds.Y)
            {
                return faction == BattleFaction.Player ? CollidingPlane.Horizontal : CollidingPlane.None;
            }
            else if (bounds.Y >= GameBounds.Y + GameBounds.Height - 16)
            {
                return faction == BattleFaction.Opponent ? CollidingPlane.Horizontal : CollidingPlane.None;
            }
            return CollidingPlane.None;
        }

        private GameColor GetColliderColor(ColliderType type)
        {
            return type switch
            {
                ColliderType.Bar => GameColor.Yellow,
                ColliderType.Ball => GameColor.Cyan,
                _ => GameColor.Blue,
            };
        }

        private void UpdateColliders()
        {
            ColliderItems.Clear();
            if (PlayerBattleBar != null)
            {
                ColliderItems.Add(new BattleColliderItem() { ColliderType = ColliderType.Bar, Faction = BattleFaction.Player, BounceFaction = BattleFaction.Player, Component = PlayerBattleBar, Bounds = PlayerBattleBar.CollidingBounds, DestroyFaction = BattleFaction.None, BounceType = ColliderType.Ball, DestroyType = ColliderType.None });
            }
            if (OponentBattleBar != null)
            {
                ColliderItems.Add(new BattleColliderItem() { ColliderType = ColliderType.Bar, Faction = BattleFaction.Opponent, BounceFaction = BattleFaction.Opponent, Component = OponentBattleBar, Bounds = OponentBattleBar.CollidingBounds, DestroyFaction = BattleFaction.None, BounceType = ColliderType.Ball, DestroyType = ColliderType.None });
            }
            if (PlayerFireBall != null)
            {
                ColliderItems.Add(new BattleColliderItem() { ColliderType = ColliderType.Ball, Faction = BattleFaction.Player, BounceFaction = BattleFaction.Opponent, Component = PlayerFireBall, Bounds = PlayerFireBall.RealBounds, DestroyFaction = BattleFaction.None, BounceType = ColliderType.Bar, DestroyType = ColliderType.None });
            }
            if (OponentFireBall != null)
            {
                ColliderItems.Add(new BattleColliderItem() { ColliderType = ColliderType.Ball, Faction = BattleFaction.Opponent, BounceFaction = BattleFaction.Player, Component = OponentFireBall, Bounds = OponentFireBall.RealBounds, DestroyFaction = BattleFaction.None, BounceType = ColliderType.Bar, DestroyType = ColliderType.None });
            }
            foreach (GameComponent component in _playerComponents)
            {
                ColliderItems.Add(new BattleColliderItem() { Component = component, Faction = BattleFaction.Player, DestroyType = ColliderType.Ball, BounceType = ColliderType.None, ColliderType = ColliderType.Actor, Bounds = new Rectangle(component.RealBounds.X - 32, component.RealBounds.Y - 32, 64, 64), BounceFaction = BattleFaction.None, DestroyFaction = BattleFaction.Opponent });
            }
            foreach (GameComponent component in _oponentComponents)
            {
                ColliderItems.Add(new BattleColliderItem() { Component = component, Faction = BattleFaction.Opponent, DestroyType = ColliderType.Ball, BounceType = ColliderType.None, ColliderType = ColliderType.Actor, Bounds = new Rectangle(component.RealBounds.X - 32, component.RealBounds.Y - 32, 64, 64), BounceFaction = BattleFaction.None, DestroyFaction = BattleFaction.Player });
            }
        }

        private void UpdateDebug()
        {
            if (BattleContainer.Debug)
            {
                Primitive2D.SetDrawAction((primitives2D) =>
                {
                    primitives2D.DrawRectangle(GameBounds, GameColor.Red, 3f);
                    foreach (BattleColliderItem item in ColliderItems)
                    {
                        primitives2D.DrawRectangle(item.Bounds, GetColliderColor(item.ColliderType), 2f);
                    }
                });
            }
            else
            {
                Primitive2D.SetDrawAction(null);
            }
        }

        private void UpdatePlayerBall()
        {
            if (PlayerBattleBar != null && PlayerFireBall != null && PlayerBallAttached)
            {
                PlayerFireBall.ForcePosition(PlayerBattleBar.Bounds, PlayerBattleBar.BarPosition);
                IMouseService mouseService = GameServiceProvider.GetService<IMouseService>();
                Point mousePosition = mouseService.GetMousePosition();
                float angle = MathTools.AngleBetweenTwoPoints(PlayerFireBall.Position, mousePosition);
                if (mouseService.IsMouseLeftButtonPressed())
                {
                    PlayerFireBall.Shoot(angle);
                }
            }
            else if (PlayerBattleBar != null && PlayerFireBall != null && !PlayerBallAttached)
            {
                // Check collision with battlefield bounds
                PlayerFireBall.ColideWithPlane(CollideWithGameBounds(PlayerFireBall.RealBounds, BattleFaction.Player), true);
                if (PlayerFireBall.CanCollide)
                {
                    CheckCollisionWithOthers(PlayerFireBall, BattleFaction.Player, ColliderType.Ball, out bool destroy);
                }
            }
        }

        #endregion Private Methods
    }
}