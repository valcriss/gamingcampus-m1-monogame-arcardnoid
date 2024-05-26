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

        private BattleField? BattleField { get; set; }
        private double DistanceFromStart { get; set; }
        private EncounterType EncounterType { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public BattleContainer()
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public void Show(GroundType ground, EncounterType type, double distanceFromStart)
        {
            RemoveAllGameComponents();
            EncounterType = type;
            DistanceFromStart = distanceFromStart;
            BattleField = AddGameComponent(new BattleField(ground, 0, 0));
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

        #endregion Private Methods
    }
}