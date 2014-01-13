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


namespace Potholy
{
    /// <summary>
    /// A drawableGameComponent with a bunch of neat stuff added on top. Should be used for all 2D objects that are to be rendered
    /// </summary>
    public class Sprite : Microsoft.Xna.Framework.DrawableGameComponent
    {
        protected Texture2D m_texture;
        public Transformation m_transform;
        public Rectangle m_sourceRectangle;
        public Vector2 m_sourceRectangleHotSpot;
        protected SpriteBatch spriteBatch;
        public bool m_flipX = false;
        private bool m_flipY = false;

        private float m_red = 1.0f;
        private float m_green = 1.0f;
        private float m_blue = 1.0f;
        private float m_alpha = 1.0f;

        Vector2 m_spawnPoint;

        
        /// <summary>
        /// Constructor - news the transformation
        /// </summary>
        public Sprite(Game game)
            : base(game)
        {
            m_transform = new Transformation();
        }



        /// <summary>
        /// Initialize - sets texture and SpriteBatch
        /// </summary>
        /// <param name="texture">Incoming 2D Texture</param>
        /// <param name="spriteBatch">Incoming SpriteBatch</param>
        public virtual void Initialize(Texture2D texture, ref SpriteBatch spriteBatch)
        {
            m_texture = texture;
            this.spriteBatch = spriteBatch;
            this.m_transform.m_depth = 0.5f;

            m_sourceRectangle = new Rectangle(0, 0, m_texture.Width, m_texture.Height);
            base.Initialize();
        }

        /// <summary>
        /// Update
        /// </summary>
        public override void Update(GameTime gameTime)
        {

            

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the texture 
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            if (this.Visible)
            {
                if (m_flipX && !m_flipY)
                    spriteBatch.Draw(m_texture,
                    this.m_transform.m_position,
                    this.m_sourceRectangle,
                    new Color(new Vector4(m_red, m_green, m_blue, m_alpha)),
                    this.m_transform.m_rotation,
                    m_sourceRectangleHotSpot,
                    this.m_transform.m_scale,
                    SpriteEffects.FlipHorizontally,
                    this.m_transform.m_depth);

                if (m_flipY && !m_flipX)
                    spriteBatch.Draw(m_texture,
                    this.m_transform.m_position,
                    this.m_sourceRectangle,
                    new Color(new Vector4(m_red, m_green, m_blue, m_alpha)),
                    this.m_transform.m_rotation,
                    m_sourceRectangleHotSpot,
                    this.m_transform.m_scale,
                    SpriteEffects.FlipVertically,
                    this.m_transform.m_depth);

                if (!m_flipY && !m_flipX)
                    spriteBatch.Draw(m_texture,
                        this.m_transform.m_position,
                        this.m_sourceRectangle,
                        new Color((new Vector4(m_red, m_green, m_blue, m_alpha))),
                        this.m_transform.m_rotation,
                        m_sourceRectangleHotSpot,
                        this.m_transform.m_scale,
                        SpriteEffects.None,
                        this.m_transform.m_depth);
            }
            
            base.Draw(gameTime);
        }

        public Vector2 Center
        {
            get { return m_transform.m_position + new Vector2(m_texture.Width, m_texture.Height) * 0.5f; }
            set { m_transform.m_position = value - new Vector2(m_texture.Width, m_texture.Height) * 0.5f; }
        }
    }
}