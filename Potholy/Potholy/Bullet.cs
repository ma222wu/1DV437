using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Potholy
{
    public class Bullet : Sprite
    {
        public Vector2 m_direction;
        public CollisionBody m_hitBox;

        /// <summary>
        /// Projectile with a direction
        /// </summary>
        /// <param name="game"></param>
        /// <param name="direction"></param>
        public Bullet(Game game, Vector2 direction)
            : base(game)
        {
            m_direction = direction;
        }

        public override void Initialize(Microsoft.Xna.Framework.Graphics.Texture2D texture, ref Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            m_hitBox = new CollisionBody(Game, new Vector2(texture.Height*0.5f, texture.Height*0.5f), Vector2.Zero);            
            
            base.Initialize(texture, ref spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            m_hitBox.Update(gameTime, m_transform.m_position);

            m_transform.m_position += m_direction;
            
            base.Update(gameTime);
        }
    }
}
