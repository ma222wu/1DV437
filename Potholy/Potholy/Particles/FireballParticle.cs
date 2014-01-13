using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Potholy
{
    class FireballParticle
    {
        Vector2 m_position;

        Texture2D m_texture;

        public float m_opacity;

        Vector2 m_scale = (Vector2.One * 100);

        public float m_fadeSpeed;

        public FireballParticle(Vector2 position, Vector2 gravity, Texture2D tex, Random r)
        {
            m_position = position;

            m_texture = tex;

            m_opacity = 1;

            m_fadeSpeed = 0.1f * (float)r.NextDouble() + 0.01f;
        }

        public void Update()
        {
            m_scale *= 1.02f;

            m_opacity -= m_fadeSpeed;
        }

        public void Draw(SpriteBatch sb)
        {
            
            sb.Draw(m_texture, m_position, new Color(Vector4.One*m_opacity));
        
        }
    }
}
