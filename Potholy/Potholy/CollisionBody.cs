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
    /// A fancy wrapper for a Rectangle object
    /// </summary>
    public class CollisionBody : Microsoft.Xna.Framework.GameComponent
    {
        public Vector2 m_size;
        public Vector2 m_position;
        public Vector2 m_positionOffset;

        public CollisionBody(Game game, Vector2 size, Vector2 position)
            : base(game)
        {
            m_size = size;
            m_position = position;
            m_positionOffset = new Vector2(0);

        }

        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="gameTime">gameTime</param>
        /// <param name="position">new position</param>
        public void Update(GameTime gameTime, Vector2 position)
        {
            m_position = position;
            

            base.Update(gameTime);
        }


        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)(m_position.X - m_positionOffset.X * 0.5f), (int)(m_position.Y - m_positionOffset.Y * 0.5f),(int)m_size.X, (int)m_size.Y);
            }
        }

        /// <summary>
        /// Similar to Rectangle's .Inflate, but applied to this class
        /// </summary>
        /// <param name="x">Width to add to the hitbox</param>
        /// <param name="y">Height to add</param>
        public void Inflate(int x, int y)
        {
            m_size.Y += y;
            m_size.X += x;
            m_positionOffset.X += x;
            m_positionOffset.Y += y;
        }

    }
}