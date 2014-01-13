using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Potholy
{
    class FireballSystem : ParticleSystem
    {
        List<FireballParticle> m_particles;

        public FireballSystem(Texture2D texture, Vector2 position)
            : base(position)
        {
            m_particles = new List<FireballParticle>();

            Random r = new Random(DateTime.Now.Ticks.GetHashCode());

            for (int i = 0; i < 100; i++)
            {
                m_particles.Add(new FireballParticle(m_position, new Vector2(0, 1), texture, r));
            }
        }

        public void Update()
        {
            for(int i = 0; i < m_particles.Count;i++) 
            {
                m_particles[i].Update();

                if (m_particles[i].m_opacity <= 0)
                    m_particles.RemoveAt(i);
            }
        }

        public void Draw(SpriteBatch sb)
        {
            foreach (FireballParticle fp in m_particles)
            {
                fp.Draw(sb);
            }
        }
    }
}
