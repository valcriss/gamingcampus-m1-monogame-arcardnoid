namespace ArcardnoidShared.Framework.Scenes.Element
{
    public abstract class Element
    {
        #region Public Properties

        public virtual bool Enabled { get; set; } = true;
        public ElementState State { get; set; }
        public virtual bool Visible { get; set; } = true;

        #endregion Public Properties

        #region Public Constructors

        public Element()
        {
            State = ElementState.None;
        }

        #endregion Public Constructors

        #region Public Methods

        public virtual void Draw()
        {
        }

        public virtual void Load()
        {
        }

        public virtual void Unload()
        {
        }

        public virtual void Update(float delta)
        {
        }

        #endregion Public Methods
    }
}