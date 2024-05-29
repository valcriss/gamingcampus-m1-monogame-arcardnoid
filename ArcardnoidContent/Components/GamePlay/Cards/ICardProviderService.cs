using ArcardnoidShared.Framework.ServiceProvider.Interfaces;

namespace ArcardnoidContent.Components.GamePlay.Cards
{
    public interface ICardProviderService
    {
        #region Public Methods

        Card GetRandomCard(IRandom random, bool filterFree);

        #endregion Public Methods
    }
}