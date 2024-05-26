using ArcardnoidContent.Components.GameScene.Battle.Enums;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Components;

namespace ArcardnoidContent.Components.GameScene.Battle.Bars
{
    public struct BattleColliderItem
    {
        #region Public Properties

        public BattleFaction BounceFaction { get; set; }
        public ColliderType BounceType { get; set; }
        public Rectangle Bounds { get; set; }
        public ColliderType ColliderType { get; set; }
        public GameComponent Component { get; set; }
        public BattleFaction DestroyFaction { get; set; }
        public ColliderType DestroyType { get; set; }
        public BattleFaction Faction { get; set; }

        #endregion Public Properties
    }
}