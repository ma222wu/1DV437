using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Potholy
{
    class ExplosionParticle
    {
        Vector2 m_position;

        float m_rotation;

        Vector2 m_direction;

        Vector2 m_gravity;

        float m_maxSpeed = 5;

        float m_baseSpeed = 1f;

        Texture2D m_texture;

        public ExplosionParticle(Vector2 position, Vector2 gravity, Texture2D tex, float baseSpeed, Random r)
        {
            m_direction = new Vector2((float)r.NextDouble() - 0.5f, (float)r.NextDouble() - 0.5f);
            
            m_direction.Normalize();

            m_direction = m_direction * ((float)r.NextDouble() * m_maxSpeed);

            m_position = position;

            m_gravity = gravity;

            m_texture = tex;

            m_baseSpeed = baseSpeed;
        }

        public void Update()
        {
            m_position += (m_direction + m_gravity) * (Vector2.One * m_baseSpeed);

            m_gravity *= 1.02f;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(m_texture, m_position, Color.White);
        }
    }
}
