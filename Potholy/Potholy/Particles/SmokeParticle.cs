using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Potholy
{
    class SmokeParticle
    {
        Vector2 m_position;

        float m_rotation;

        Vector2 m_direction;

        Vector2 m_gravity;

        float m_maxSpeed = 1;

        Texture2D m_texture;

        public float m_opacity;

        Vector2 m_scale = Vector2.One;

        public float m_fadeSpeed;

        public SmokeParticle(Vector2 position, Vector2 gravity, Texture2D tex, Random r)
        {
            m_direction = new Vector2((float)r.NextDouble() - 0.5f, (float)r.NextDouble() - 0.5f);
            
            m_direction.Normalize();

            m_direction = m_direction * ((float)r.NextDouble() * m_maxSpeed);

            m_position = position;

            m_gravity = gravity;

            m_texture = tex;

            m_opacity = 1;

            m_fadeSpeed = 0.01f * (float)r.NextDouble() + 0.0001f;
        }

        public void Update()
        {
            m_position += m_direction + m_gravity;

            m_gravity *= 1.02f;

            m_scale *= 1.02f;

            m_rotation += 0.01f;

            m_opacity -= m_fadeSpeed;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(m_texture, m_position,null, new Color(Vector4.One*m_opacity), m_rotation, Vector2.Zero,m_scale, SpriteEffects.None, 0.5f);
        }
    }
}
