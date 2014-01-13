using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace Potholy
{
    /// <summary>
    /// A Scene
    /// </summary>
    public class Scene : Microsoft.Xna.Framework.DrawableGameComponent
    {
        #region Members
        protected SceneManager m_sceneManager;
        protected readonly SpriteBatch m_spriteBatch;
        private List<GameComponent> m_components;
        protected bool m_justEntered = true;

        #endregion

        public Scene(Game game)
            : base(game)
        {
            m_spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            m_components = new List<GameComponent>();
            m_sceneManager = new SceneManager(Game);
        }
        #region Methods
        /// <summary>
        /// Initialize - calls initialize on all GameComponents (referred to as gc) in the list
        /// </summary>
        public override void Initialize()
        {
            foreach (GameComponent gc in Components)
                gc.Initialize();

            base.Initialize();
        }

        /// <summary>
        /// Update
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            //Goes through the list of GameComponents and updates them if they're enabled
            //for (int i = 0; i < Components.Count; i++)
            //{
            //    GameComponent gc = Components[i];

            //    if (gc.Enabled)
            //        gc.Update(gameTime);
            //}
            base.Update(gameTime);
        }

        /// <summary>
        /// Draw
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            //for (int i = 0; i < Components.Count; i++)
            //{
            //    //Checks if component is drawable
            //    if (Components[i] is DrawableGameComponent)
            //    {
            //        //Checks if component is visible
            //        if ((Components[i] as DrawableGameComponent).Visible)
            //        {
            //            (Components[i] as DrawableGameComponent).Draw(gameTime);
            //        }
            //    }
            //}
            base.Draw(gameTime);
        }

        /// <summary>
        /// Shows the scene - makes the scene Updatable and Drawable
        /// </summary>
        public virtual void Show()
        {
            Visible = true;
            Enabled = true;
        }

        /// <summary>
        /// Hides the scene - makes the scene non-Updatable and non-Drawable
        /// </summary>
        public virtual void Hide()
        {
            Visible = false;
            Enabled = false;
        }

        /// <summary>
        /// Called when entering the scene
        /// </summary>
        public virtual void OnEnter()
        {

        }

        /// <summary>
        /// Called when quitting the scene
        /// </summary>
        public virtual void OnExit()
        {

        }

        #endregion

        #region Properties

        public SpriteBatch SpriteBatch
        {
            get { return m_spriteBatch; }
        }

        public List<GameComponent> Components
        {
            get { return m_components; }
        }
        #endregion
    }
}