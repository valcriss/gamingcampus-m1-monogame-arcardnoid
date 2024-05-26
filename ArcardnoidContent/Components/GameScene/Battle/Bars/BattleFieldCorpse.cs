using ArcardnoidContent.Components.Shared.Map.Cells;

namespace ArcardnoidContent.Components.GameScene.Battle.Bars
{
    public enum BattleFieldCorpseElapsedAction
    {
        Disappear,
        Unload,
    }

    public struct BattleFieldCorpse
    {
        #region Public Properties

        public BattleFieldCorpseElapsedAction BattleFieldCorpseElapsedAction { get; set; }
        public MapCell Component { get; set; }
        public DateTime CreationTime { get; set; }
        public double Duration { get; set; }

        #endregion Public Properties
    }
}