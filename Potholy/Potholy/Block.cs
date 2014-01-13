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
    /// Struct used to ferry data between .xml-files and blocks
    /// </summary>
    public struct BlockData
    {
        public string textureName;
        public Vector2 position;
        public float depth;
    }

    /// <summary>
    /// A clip
    /// </summary>
    public class Block : Sprite
    {
        public CollisionBody m_hitBox;
        bool m_cosmetic = false;
        string m_textureName;
        Sprite debugBox;
        public Rectangle trueHitbox;

        public Block(Game game)
            : base(game)
        {}

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="texture">texture to use</param>
        /// <param name="spriteBatch">spritebatch to use to draw</param>
        /// <param name="position">initial position</param>
        /// <param name="size">initial size</param>
        public virtual void Initialize(Texture2D texture, ref SpriteBatch spriteBatch, Vector2 position, Vector2 size)
        {
            m_transform.m_position = position;
            if (size == null)
                m_hitBox = new CollisionBody(Game, new Vector2(texture.Width, texture.Height), position);
            else
            {
                m_hitBox = new CollisionBody(Game, (Vector2)size, position);
            }

            debugBox = new Sprite(Game);
            Texture2D temp = Game.Content.Load<Texture2D>("errorgrid");
            debugBox.Initialize(temp, ref spriteBatch);
            
            base.Initialize(texture, ref spriteBatch);

            if (size != null)
            {
                m_sourceRectangle.Width = (int)size.X;
                m_sourceRectangle.Height = (int)size.Y;
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            m_hitBox.Update(gameTime, m_transform.m_position);

            debugBox.m_transform.m_position = new Vector2(m_hitBox.Rectangle.Location.X, m_hitBox.Rectangle.Location.Y);
            debugBox.m_sourceRectangle = m_hitBox.Rectangle;

            base.Update(gameTime);
        }

        /// <summary>
        /// Draw. Debugbox provides visual information on where the block is.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            //debugBox.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}