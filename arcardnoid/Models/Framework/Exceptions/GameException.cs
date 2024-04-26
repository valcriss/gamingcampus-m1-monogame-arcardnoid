using System;

namespace arcardnoid.Models.Framework.Exceptions
{
    internal class GameException : Exception
    {
        #region Public Constructors

        public GameException(string message, Exception ex) : base(message, ex)
        {
        }

        #endregion Public Constructors
    }
}