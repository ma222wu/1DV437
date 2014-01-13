using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Potholy
{
    class ExplosionSystem : ParticleSystem
    {
        List<ExplosionParticle> m_particles;

        public ExplosionSystem(Texture2D texture, Vector2 position, Vector2 gravity, float baseSpeed)
            : base(position)
        {
            m_particles = new List<ExplosionParticle>();

            Random r = new Random(DateTime.Now.Ticks.GetHashCode());

            for (int i = 0; i < 100; i++)
            {
                m_particles.Add(new ExplosionParticle(position, gravity, texture, baseSpeed, r));
            }
        }

        public void Update()
        {
            foreach (ExplosionParticle ep in m_particles)
            {
                ep.Update();
            }
        }

        public void Draw(SpriteBatch sb)
        {
            foreach (ExplosionParticle ep in m_particles)
            {
                ep.Draw(sb);
            }
        }
    }
}
