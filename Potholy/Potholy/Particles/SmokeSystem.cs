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
    public class SmokeSystem : ParticleSystem
    {
        List<SmokeParticle> m_particles;
        Random r;
        Texture2D m_texture;
        bool m_regenerating = false;

        public SmokeSystem(Texture2D texture, Camera c, Vector2 position, bool regenerating)
            : base(position)
        {
            m_particles = new List<SmokeParticle>();
            r = new Random(DateTime.Now.Ticks.GetHashCode());
            m_texture = texture;
            m_regenerating = regenerating;

            for (int i = 0; i < 100; i++)
            {
                m_particles.Add(CreateParticle());
            }
        }

        public override void Initialize()
        {

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            for(int i = 0; i < m_particles.Count; i++)
            {
                m_particles[i].Update();
                if (m_particles[i].m_opacity < 0 && m_regenerating)
                    m_particles[i] = CreateParticle();
            }

            base.Update(gameTime);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch sb)
        {
            foreach (SmokeParticle sp in m_particles)
            {
                sp.Draw(sb);
            }
        }

        private SmokeParticle CreateParticle()
        {
            return new SmokeParticle(m_position, new Vector2(0, -1), m_texture, r);
        }
    }
}
