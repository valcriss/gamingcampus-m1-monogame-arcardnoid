using System.Collections.Generic;

namespace arcardnoid.Models.Framework.Components.Profiler
{
    public static class ProfilerCollection
    {
        #region Public Properties

        public static List<ProfilerItem> Items { get; set; }

        #endregion Public Properties

        #region Public Constructors

        static ProfilerCollection()
        {
            Items = new List<ProfilerItem>();
        }

        #endregion Public Constructors

        #region Public Methods

        public static void Add(string name, double value)
        {
            Items.Add(new ProfilerItem { Name = name, Value = value });
        }

        public static void Clear()
        {
            Items.Clear();
        }

        #endregion Public Methods
    }

    public class ProfilerItem
    {
        #region Public Properties

        public string Name { get; set; }
        public double Value { get; set; }

        #endregion Public Properties
    }
}