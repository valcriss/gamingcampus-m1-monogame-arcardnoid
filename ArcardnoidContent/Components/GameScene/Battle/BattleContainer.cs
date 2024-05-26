using ArcardnoidContent.Components.Shared.Map.Enums;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Animations;
using ArcardnoidShared.Framework.Scenes.Components;

namespace ArcardnoidContent.Components.GameScene.Battle
{
    public class BattleContainer : GameComponent
    {
        #region Public Properties

        public static bool Debug { get; set; } = false;

        #endregion Public Properties

        #region Private Properties

        private BattleEnd? BattleEnd { get; set; }
        private Action<bool, Point>? BattleEnded { get; set; } = null;
        private BattleField? BattleField { get; set; }
        private Point Cell { get; set; } = Point.Zero;
        private double DistanceFromStart { get; set; }
        private EncounterType EncounterType { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public BattleContainer()
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public void Show(GroundType ground, EncounterType type, double distanceFromStart, Point cell, Action<bool, Point>? battleEnded)
        {
            RemoveAllGameComponents();
            Cell = cell;
            BattleEnded = battleEnded;
            EncounterType = type;
            DistanceFromStart = distanceFromStart;
            BattleField = AddGameComponent(new BattleField(ground, 0, 0, OnBattleEnded));
            BattleEnd = AddGameComponent(new BattleEnd());
            AddShowAnimation(BattleField);
        }

        public void ToggleDebug()
        {
            Debug = !Debug;
        }

        #endregion Public Methods

        #region Private Methods

        private void AddShowAnimation(BattleField side)
        {
            side.AddAnimation<BattleField>(new MoveAnimation(0.5f, new Point(0, -1300), new Point(0, 0), false, true, EaseType.InOutBounce, () => { side.ShowMap(EncounterType, DistanceFromStart); }));
        }

        private void OnBattleEnded(bool victory)
        {
            if (BattleField == null || BattleEnd == null)
            {
                return;
            }
            BattleField.Enabled = false;
            BattleEnd.Show(victory, 7, () =>
            {
                BattleEnded?.Invoke(victory, Cell);
                BattleField.InnerUnload();
                BattleEnd.InnerUnload();
            });
        }

        #endregion Private Methods
    }
}