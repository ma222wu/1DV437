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
    /// This is basically a hitbox that other objects use to check for collisions with each other
    /// </summary>
    public class CollisionBody : Microsoft.Xna.Framework.GameComponent
    {
        Vector2 m_size;
        Vector2 m_position;
        Vector2 m_positionOffset;

        


        public CollisionBody(Game game, Vector2 size, Vector2 position)
            : base(game)
        {
            m_size = size;
            m_position = position;
            m_positionOffset = new Vector2(0);

        }

        /// <summary>
        /// Calls base.Initialize
        /// </summary>
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

        /// <summary>
        /// Returns a rectangle based on the stats of this object
        /// </summary>
        /// <returns></returns>
        public Rectangle Get()
        {
            Rectangle r;

            r.Height = (int)m_size.Y;
            r.Width = (int)m_size.X;
            r.X = (int)(m_position.X - m_positionOffset.X *0.5f);
            r.Y = (int)(m_position.Y - m_positionOffset.Y * 0.5f);

            return new Rectangle(r.X,r.Y,r.Width,r.Height);
        }

        /// <summary>
        /// Retuns a boundingbox based on this object
        /// </summary>
        /// <returns></returns>
        public BoundingBox ToBoundingBox()
        {
            return new BoundingBox(new Vector3(new Vector2(this.Get().X, this.Get().Y), 0), new Vector3(new Vector2(this.Get().X + m_size.X, this.Get().Y + m_size.Y), 0));
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

        #region properties
        public Vector2 Position
        {
            get { return m_position; }
            set { m_position = value; }
        }

        public Vector2 Size
        {
            get { return m_size; }
            set { m_size = value; }
        }
        #endregion
    }
}