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


namespace LevelEditor
{
    /// <summary>
    /// A drawableGameComponent with a bunch of neat stuff added on top. Should be used for all 2D objects that are to be rendered
    /// Pretty much identical to _1DV430Project version, but implements IClonable to allow for CTRL+C functionality.
    /// See _1DV430Project version for more in depth documentation.
    /// </summary>
    public class Sprite : ICloneable
    {
        public Texture2D m_texture;
        public Transformation m_transform;
        public Rectangle m_sourceRectangle;
        public Vector2 m_sourceRectangleHotSpot;
        public SpriteBatch spriteBatch;
        public bool m_flipX = false;
        public bool m_flipY = false;

        public bool Visible=true;

        public float m_red = 1.0f;
        public float m_green = 1.0f;
        public float m_blue = 1.0f;
        public float m_alpha = 1.0f;

        Vector2 m_spawnPoint;

        public Game Game;

        
        /// <summary>
        /// Constructor - news the transformation
        /// </summary>
        public Sprite(Game game)
        {
            m_transform = new Transformation();
            Game = game;
        }



        /// <summary>
        /// Initialize - sets texture and SpriteBatch
        /// </summary>
        /// <param name="texture">Incoming 2D Texture</param>
        /// <param name="spriteBatch">Incoming SpriteBatch</param>
        public virtual void Initialize(ref Texture2D texture, ref SpriteBatch spriteBatch)
        {
            m_texture = texture;
            this.spriteBatch = spriteBatch;
            this.m_transform.m_depth = 0.5f;

            SourceRectangle = new Rectangle(0, 0, m_texture.Width, m_texture.Height);
        }

        public virtual void Initialize(string textureName, ref SpriteBatch spriteBatch)
        {
            m_texture = Game.Content.Load<Texture2D>(textureName);
            this.spriteBatch = spriteBatch;
            this.m_transform.m_depth = 0.5f;

            SourceRectangle = new Rectangle(0, 0, m_texture.Width, m_texture.Height);
        }

        /// <summary>
        /// Update
        /// </summary>
        public virtual void Update(GameTime gameTime)
        {

            

        }

        /// <summary>
        /// Draws the texture 
        /// </summary>
        public virtual void Draw(GameTime gameTime)
        {
            if (this.Visible)
            {
                if (m_flipX && !m_flipY)
                    spriteBatch.Draw(m_texture,
                    this.m_transform.m_position,
                    this.m_sourceRectangle,
                    new Color(new Vector4(Red,Green,Blue,Alpha)),
                    this.m_transform.m_rotation,
                    SourceRectangleHotSpot,
                    this.m_transform.m_scale,
                    SpriteEffects.FlipHorizontally,
                    this.m_transform.m_depth);

                if (m_flipY && !m_flipX)
                    spriteBatch.Draw(m_texture,
                    this.m_transform.m_position,
                    this.m_sourceRectangle,
                    new Color(new Vector4(Red, Green, Blue, Alpha)),
                    this.m_transform.m_rotation,
                    SourceRectangleHotSpot,
                    this.m_transform.m_scale,
                    SpriteEffects.FlipVertically,
                    this.m_transform.m_depth);

                if (!m_flipY && !m_flipX)
                    spriteBatch.Draw(m_texture,
                        this.m_transform.m_position,
                        this.m_sourceRectangle,
                        new Color((new Vector4(Red, Green, Blue, Alpha))),
                        this.m_transform.m_rotation,
                        SourceRectangleHotSpot,
                        this.m_transform.m_scale,
                        SpriteEffects.None,
                        this.m_transform.m_depth);
            }
        }

        public object Clone()
        {
            return this;
        }

        #region Properties

        public Transformation Transform
        {
            get { return m_transform; }
            set { m_transform = value; }
        }


        public Rectangle SourceRectangle
        {
            get { return m_sourceRectangle; }
            set 
            { 
                m_sourceRectangle = value;
            }
        }

        public Vector2 SourceRectangleHotSpot
        {
            get { return m_sourceRectangleHotSpot; }
            set { m_sourceRectangleHotSpot = value; }
        }

        public bool FlipX
        {
            get { return m_flipX; }
            set { m_flipX = value; }
        }

        public bool FlipY
        {
            get { return m_flipY; }
            set { m_flipY = value; }
        }

        public float Red
        {
            get { return m_red; }
            set { m_red = value; }
        }

        public float Green
        {
            get { return m_green; }
            set { m_green = value; }
        }

        public float Blue
        {
            get { return m_blue; }
            set { m_blue = value; }
        }

        public float Alpha
        {
            get { return m_alpha; }
            set { m_alpha = value; }
        }

        public Texture2D Texture
        {
            get { return m_texture; }
            set { m_texture = value; }
        }

        public Vector2 SPAWNPOINT
        {
            get { return m_spawnPoint; }
            set { m_spawnPoint = value; }
        }

        public Vector2 Center
        {
            get { return Transform.m_position + new Vector2(m_texture.Width,m_texture.Height) * 0.5f; }
        }

        #endregion
    }
}