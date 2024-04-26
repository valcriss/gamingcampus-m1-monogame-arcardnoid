﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace arcardnoid.Models.Framework.Scenes
{
    public abstract class ComponentContainer : Element
    {
        #region Protected Properties

        protected List<Component> Components { get; set; }

        #endregion Protected Properties

        #region Public Constructors

        public ComponentContainer()
        {
            Components = new List<Component>();
        }

        #endregion Public Constructors

        #region Public Methods

        public virtual Component AddComponent(Component component)
        {
            Components.Add(component);
            return component;
        }

        public void RemoveComponent(Component component)
        {
            component.InnerUnload();
        }

        #endregion Public Methods

        #region Internal Methods

        internal void InnerDraw()
        {
            if (this.State == ElementState.Loaded)
            {
                this.Draw();
                foreach (var component in Components)
                {
                    if (component.State == ElementState.Loaded)
                        component.InnerDraw();
                }
            }
        }

        internal void InnerLoad()
        {
            this.State = ElementState.Loading;
            this.Load();
            foreach (var component in Components)
            {
                component.SetGame(Game);
                component.InnerLoad();
            }
            this.State = ElementState.Loaded;
        }

        internal void InnerUnload()
        {
            this.State = ElementState.Unloading;
            this.Unload();
            foreach (var component in Components)
            {
                component.InnerUnload();
            }
            this.State = ElementState.Unloaded;
        }

        internal void InnerUpdate(GameTime gameTime)
        {
            if (this.State == ElementState.Loaded)
            {
                this.Update(gameTime);
                foreach (var component in Components)
                {
                    if (component.State == ElementState.Loaded)
                        component.InnerUpdate(gameTime);
                }
            }
        }

        #endregion Internal Methods
    }
}