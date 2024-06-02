using ArcardnoidContent.Components.GamePlay;
using ArcardnoidContent.Components.GamePlay.Cards;
using ArcardnoidContent.Components.GameScene.Battle.Bars;
using ArcardnoidContent.Components.GameScene.Battle.Cards;
using ArcardnoidContent.Components.GameScene.Battle.Enums;
using ArcardnoidContent.Components.Shared.Map;
using ArcardnoidContent.Components.Shared.Map.Cells;
using ArcardnoidContent.Components.Shared.Map.Enums;
using ArcardnoidShared.Framework.Components.Images;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Animations;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.Scenes.Element;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Enums;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;
using ArcardnoidShared.Framework.Tools;

namespace ArcardnoidContent.Components.GameScene.Battle
{
    public class BattleField : GameComponent
    {
        #region Private Properties

        private static IGamePlay GamePlay => GameServiceProvider.GetService<IGamePlay>();
        private static IRandom Random => GameServiceProvider.GetService<IRandomService>().GetRandom();
        private BattleCardsDeck BattleCardsDeck { get; set; }
        private List<BattleColliderItem> ColliderItems { get; set; } = new List<BattleColliderItem>();
        private Rectangle GameBounds { get; set; } = Rectangle.Empty;
        private Action<bool>? OnBattleEnded { get; set; } = null;
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

        private const double CORPSE_DURATION = 5;
        private const double CORPSE_HIDE_DURATION = 0.5f;
        private List<BattleFieldCorpse> _battleFieldCorpses = new List<BattleFieldCorpse>();
        private bool _battleStarted = false;
        private List<GameComponent> _oponentComponents = new List<GameComponent>();
        private List<GameComponent> _playerComponents = new List<GameComponent>();

        #endregion Private Fields

        #region Public Constructors

        public BattleField(GroundType ground, int x, int y, Action<bool>? onBattleEnded) : base(x, y, 1664, 1080)
        {
            OnBattleEnded = onBattleEnded;
            GameBounds = new Rectangle(x + 256, y + 60, 22 * 64, 15 * 64);
            StaticMap = AddGameComponent(new AnimatedStaticMap(ground == GroundType.Grass ? "Maps/grassmap.json" : "Maps/sandmap.json", 256, 60, MapAnimationEnded, true));
            Primitive2D = AddGameComponent(new Primitive2D());
            GamePlay.AttackSpellCasted += AttackSpellCasted;
            GamePlay.UnitsSpawned += UnitsSpawned;
        }

        #endregion Public Constructors

        #region Public Methods

        public static ITexture LoadAssetTexture(TextureType asset)
        {
            return GameServiceProvider.GetService<ITextureService>().Load(asset);
        }

        public void MapAnimationEnded()
        {
            System.Diagnostics.Debug.WriteLine("Map animation ended");
            PlayerBattleBar = AddGameComponent(new PlayerBattleBar(GameBounds)).Show(() => { BarAnimationCompleted(BattleFaction.Player); });
            OponentBattleBar = AddGameComponent(new OponentBattleBar(GameBounds)).Show(() => { BarAnimationCompleted(BattleFaction.Opponent); });
            BattleCardsDeck = AddGameComponent((new BattleCardsDeck(0, 0) { Opacity = 0 }).AddAnimation<BattleCardsDeck>(new AlphaFadeAnimation(0.5f, 0, 1, false, true, EaseType.Linear)));
        }

        public void ShowMap(EncounterType encounterType, double distanceFromStart)
        {
            _oponentComponents.Clear();
            _playerComponents.Clear();
            _battleFieldCorpses.Clear();
            AddUnits(encounterType, distanceFromStart);
            StaticMap.AddTilesAnimation();
        }

        public override void Update(float delta)
        {
            base.Update(delta);
            UpdateColliders();
            UpdatePlayerBall();
            UpdateOponentBall(delta);
            UpdateDebug();
            UpdateCorpses();
            CheckBattleEnded();
        }

        #endregion Public Methods

        #region Private Methods

        private static TextureType AssetPath(EncounterType encounterType)
        {
            return encounterType switch
            {
                EncounterType.Archer => TextureType.MAP_UNITS_ARCHER_BLUE_IDLE,
                EncounterType.Warrior => TextureType.MAP_UNITS_WARRIOR_BLUE_IDLE,
                EncounterType.Torch => TextureType.MAP_UNITS_TORCH_RED_IDLE,
                EncounterType.Tnt => TextureType.MAP_UNITS_TNT_RED_IDLE,
                _ => TextureType.MAP_UNITS_ARCHER_BLUE_IDLE,
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

        private static int NumberOfOpponents(double distanceFromStart)
        {
            System.Diagnostics.Debug.WriteLine($"Distance from start: {distanceFromStart}");
            float ratio = (float)distanceFromStart / 20;
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
            ITexture playerAsset = LoadAssetTexture(TextureType.MAP_UNITS_PLAYER_BATTLE);
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

        private void AttackSpellCasted(Card card)
        {
            if (card.SpellTexture == null) return;
            List<BattleColliderItem> items = ColliderItems.Where(c => c.Faction == BattleFaction.Opponent && c.ColliderType == ColliderType.Actor && c.Component.State != ElementState.Unloaded).Take((int)card.CardParam).ToList();
            items = items.OrderBy(items => Random.Next()).ToList();

            foreach (var item in items)
            {
                var component = item.Component;
                int gridX = ((MapCell)component).GridX;
                int gridY = ((MapCell)component).GridY;
                AnimatedCell c = AddGameComponent(new AnimatedCell(LoadAssetTexture(card.SpellTexture.Value), card.SpellCols, card.SpellRows, card.SpellSpeed, 0, 0, gridX, gridY, (int)component.RealBounds.X, (int)component.RealBounds.Y, 0, 0, false, (animatedCell) =>
                {
                    AnimatedCell corpse = AddGameComponent(new AnimatedCell(LoadAssetTexture(TextureType.MAP_UNITS_DEAD_1), 7, 1, 80, 0, 0, gridX, gridY, (int)component.RealBounds.X, (int)component.RealBounds.Y, 0, 0, false));
                    _battleFieldCorpses.Add(new BattleFieldCorpse() { Component = corpse, CreationTime = DateTime.Now, Duration = CORPSE_DURATION, BattleFieldCorpseElapsedAction = BattleFieldCorpseElapsedAction.Disappear });
                    MoveToFront(PlayerFireBall);
                    MoveToFront(OponentFireBall);
                    component.InnerUnload();
                    animatedCell.InnerUnload();
                    ColliderItems.RemoveAll(x => x.Component == component);
                }));
            }
        }

        private void BarAnimationCompleted(BattleFaction faction)
        {
            if (faction == BattleFaction.Player)
                PlayerFireBall = AddGameComponent(new FireBall(BattleFaction.Player));
            else
                OponentFireBall = AddGameComponent(new FireBall(BattleFaction.Opponent));
        }

        private void CheckBattleEnded()
        {
            if (PlayerFireBall == null || OponentFireBall == null) return;
            int playerUnitsCount = ColliderItems.Where(c => c.Faction == BattleFaction.Player && c.ColliderType == ColliderType.Actor && c.Component.State != ElementState.Unloaded).Count();
            int oponentUnitsCount = ColliderItems.Where(c => c.Faction == BattleFaction.Opponent && c.ColliderType == ColliderType.Actor && c.Component.State != ElementState.Unloaded).Count();
            if (playerUnitsCount == 0)
            {
                OnBattleEnded?.Invoke(false);
            }
            else if (oponentUnitsCount == 0)
            {
                OnBattleEnded?.Invoke(true);
            }
        }

        private void CheckCollisionWithOthers(FireBall ball, BattleFaction faction, ColliderType type, out bool destroy, out GameComponent? collidingComponent)
        {
            destroy = false;
            collidingComponent = null;
            foreach (BattleColliderItem item in ColliderItems)
            {
                if (item.Component.State == ElementState.Unloaded)
                {
                    continue;
                }
                if ((item.BounceFaction == faction && item.BounceType == type) || (item.DestroyFaction == faction && item.DestroyType == type))
                {
                    RectangleFace face = item.Bounds.Collide(ball.RealBounds);
                    if (face != RectangleFace.None)
                    {
                        CollidingPlane plane = face == RectangleFace.Left || face == RectangleFace.Right ? CollidingPlane.Vertical : CollidingPlane.Horizontal;
                        ball.ColideWithPlane(plane, false);
                        destroy = item.DestroyFaction == faction;
                        collidingComponent = item.Component;
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

        private void UnitsSpawned(int num)
        {
            ITexture playerAsset = LoadAssetTexture(TextureType.MAP_UNITS_PLAYER_BATTLE);
            int positionX = 96;
            int positionY = 576;
            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    bool isFree = StaticMap.IsFree(TextureType.MAP_UNITS_PLAYER_BATTLE, x, y);
                    if (isFree)
                    {
                        var c = StaticMap.AddGameComponent(new AnimatedCell(playerAsset, GetTextureColumns(EncounterType.None), 1, 120, 0, 0, x, y, positionX + x * 64, positionY + y * 64, 0, 0));
                        _playerComponents.Add(c);
                        StaticMap.AddTileAnimation(c);
                    }
                }
            }
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

        private void UpdateCorpses()
        {
            BattleFieldCorpse[] corpses = _battleFieldCorpses.ToArray();
            foreach (BattleFieldCorpse corpse in corpses)
            {
                if ((DateTime.Now - corpse.CreationTime).TotalSeconds > corpse.Duration)
                {
                    corpse.Component.InnerUnload();
                    _battleFieldCorpses.Remove(corpse);
                    if (corpse.BattleFieldCorpseElapsedAction == BattleFieldCorpseElapsedAction.Disappear)
                    {
                        var component = corpse.Component;
                        int gridX = ((MapCell)component).GridX;
                        int gridY = ((MapCell)component).GridY;
                        AnimatedCell c = AddGameComponent(new AnimatedCell(LoadAssetTexture(TextureType.MAP_UNITS_DEAD_2), 7, 1, 80, 0, 0, gridX, gridY, (int)component.RealBounds.X, (int)component.RealBounds.Y, 0, 0, false));
                        _battleFieldCorpses.Add(new BattleFieldCorpse() { Component = c, CreationTime = DateTime.Now, Duration = CORPSE_HIDE_DURATION, BattleFieldCorpseElapsedAction = BattleFieldCorpseElapsedAction.Unload });
                    }
                }
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

        private void UpdateOponentBall(float delta)
        {
            if (OponentBattleBar != null && OponentFireBall != null && OponentBallAttached)
            {
                OponentFireBall.ForcePosition(OponentBattleBar.Bounds, OponentBattleBar.BarPosition);
                int targetX = Random.Next(0, 1920);
                Point mousePosition = new Point(targetX, 1080);
                float angle = MathTools.AngleBetweenTwoPoints(OponentFireBall.Position, mousePosition);
                OponentFireBall.Shoot(angle, BattleFaction.Opponent);
            }
            else if (OponentBattleBar != null && OponentFireBall != null && !OponentBallAttached)
            {
                // Check collision with battlefield bounds
                OponentFireBall.ColideWithPlane(CollideWithGameBounds(OponentFireBall.RealBounds, BattleFaction.Opponent), true);
                if (OponentFireBall.CanCollide)
                {
                    CheckCollisionWithOthers(OponentFireBall, BattleFaction.Opponent, ColliderType.Ball, out bool destroy, out GameComponent? component);
                    if (component != null && destroy)
                    {
                        int gridX = ((MapCell)component).GridX;
                        int gridY = ((MapCell)component).GridY;
                        GamePlay.RemoveUnits(1);
                        AnimatedCell corpse = AddGameComponent(new AnimatedCell(LoadAssetTexture(TextureType.MAP_UNITS_DEAD_1), 7, 1, 80, 0, 0, gridX, gridY, (int)component.RealBounds.X, (int)component.RealBounds.Y, 0, 0, false));
                        _battleFieldCorpses.Add(new BattleFieldCorpse() { Component = corpse, CreationTime = DateTime.Now, Duration = CORPSE_DURATION, BattleFieldCorpseElapsedAction = BattleFieldCorpseElapsedAction.Disappear });
                        MoveToFront(PlayerFireBall);
                        MoveToFront(OponentFireBall);
                        component.InnerUnload();
                        ColliderItems.RemoveAll(x => x.Component == component);
                    }
                }

                // Check if this ball is outside the screen
                if (OponentFireBall.RealBounds.X < 0 || OponentFireBall.RealBounds.X > 1920 || OponentFireBall.RealBounds.Y < 0 || OponentFireBall.RealBounds.Y > 1080)
                {
                    BattleColliderItem[] items = ColliderItems.Where(c => c.Component.State != ElementState.Unloaded && c.ColliderType == ColliderType.Actor && c.Faction == BattleFaction.Opponent).ToArray();
                    if (items.Length > 0)
                    {
                        int index = Random.Next(0, items.Length - 1);
                        BattleColliderItem item = items[index];
                        int gridX = ((MapCell)item.Component).GridX;
                        int gridY = ((MapCell)item.Component).GridY;
                        AnimatedCell corpse = AddGameComponent(new AnimatedCell(LoadAssetTexture(TextureType.MAP_UNITS_DEAD_1), 7, 1, 80, 0, 0, gridX, gridY, (int)item.Component.RealBounds.X, (int)item.Component.RealBounds.Y, 0, 0, false));
                        _battleFieldCorpses.Add(new BattleFieldCorpse() { Component = corpse, CreationTime = DateTime.Now, Duration = CORPSE_DURATION, BattleFieldCorpseElapsedAction = BattleFieldCorpseElapsedAction.Disappear });

                        item.Component.InnerUnload();
                        ColliderItems.RemoveAll(x => x.Component == item.Component);
                        OponentFireBall.Reset();
                    }
                }
                OponentBattleBar.UpdateBarPosition(delta, OponentFireBall);
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
                    PlayerFireBall.Shoot(angle, BattleFaction.Player);
                }
            }
            else if (PlayerBattleBar != null && PlayerFireBall != null && !PlayerBallAttached)
            {
                // Check collision with battlefield bounds
                PlayerFireBall.ColideWithPlane(CollideWithGameBounds(PlayerFireBall.RealBounds, BattleFaction.Player), true);
                if (PlayerFireBall.CanCollide)
                {
                    CheckCollisionWithOthers(PlayerFireBall, BattleFaction.Player, ColliderType.Ball, out bool destroy, out GameComponent? component);
                    if (component != null && destroy)
                    {
                        int gridX = ((MapCell)component).GridX;
                        int gridY = ((MapCell)component).GridY;
                        AnimatedCell corpse = AddGameComponent(new AnimatedCell(LoadAssetTexture(TextureType.MAP_UNITS_DEAD_1), 7, 1, 80, 0, 0, gridX, gridY, (int)component.RealBounds.X, (int)component.RealBounds.Y, 0, 0, false));
                        _battleFieldCorpses.Add(new BattleFieldCorpse() { Component = corpse, CreationTime = DateTime.Now, Duration = CORPSE_DURATION, BattleFieldCorpseElapsedAction = BattleFieldCorpseElapsedAction.Disappear });

                        MoveToFront(PlayerFireBall);
                        MoveToFront(OponentFireBall);

                        component.InnerUnload();
                        ColliderItems.RemoveAll(x => x.Component == component);
                    }
                }

                // Check if this ball is outside the screen
                if (PlayerFireBall.RealBounds.X < 0 || PlayerFireBall.RealBounds.X > 1920 || PlayerFireBall.RealBounds.Y < 0 || PlayerFireBall.RealBounds.Y > 1080)
                {
                    BattleColliderItem[] items = ColliderItems.Where(c => c.ColliderType == ColliderType.Actor && c.Faction == BattleFaction.Player).ToArray();
                    if (items.Length > 0)
                    {
                        int index = Random.Next(0, items.Length - 1);
                        BattleColliderItem item = items[index];
                        int gridX = ((MapCell)item.Component).GridX;
                        int gridY = ((MapCell)item.Component).GridY;
                        AnimatedCell corpse = AddGameComponent(new AnimatedCell(LoadAssetTexture(TextureType.MAP_UNITS_DEAD_1), 7, 1, 80, 0, 0, gridX, gridY, (int)item.Component.RealBounds.X, (int)item.Component.RealBounds.Y, 0, 0, false));
                        _battleFieldCorpses.Add(new BattleFieldCorpse() { Component = corpse, CreationTime = DateTime.Now, Duration = CORPSE_DURATION, BattleFieldCorpseElapsedAction = BattleFieldCorpseElapsedAction.Disappear });
                        item.Component.InnerUnload();
                        GamePlay.RemoveUnits(1);
                        ColliderItems.RemoveAll(x => x.Component == item.Component);
                        PlayerFireBall.Reset();
                    }
                }
            }
        }

        #endregion Private Methods
    }
}