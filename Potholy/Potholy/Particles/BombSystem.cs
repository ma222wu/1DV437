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
    public class BombSystem : ParticleSystem
    {
        ExplosionSystem m_explosionSystem;
        FireballSystem m_fireballSystem;
        SmokeSystem m_smokeSystem;

        TimeSpan m_elapsedTime;
        TimeSpan m_lifeTime = new TimeSpan(0,0,5);

        public bool m_dead;

        public BombSystem(Camera c, Vector2 position, Texture2D explosion,Texture2D fireball,Texture2D smoke)
            : base(position)
        {
            m_explosionSystem = new ExplosionSystem(explosion, position + Vector2.One * 1.2f, Vector2.Zero, 50);
            m_fireballSystem = new FireballSystem(fireball, position);
            m_smokeSystem = new SmokeSystem(smoke, c, position + Vector2.One * 2f, false);
        }

        public override void Initialize()
        {
            

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {

            if (m_elapsedTime < m_lifeTime)
            {
                m_explosionSystem.Update();
                m_fireballSystem.Update();
                m_smokeSystem.Update(gameTime);
            }
            else
                m_dead = true;

            m_elapsedTime += gameTime.ElapsedGameTime;
            


            base.Update(gameTime);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch sb)
        {
            if (m_elapsedTime < m_lifeTime)
            {
                sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                m_smokeSystem.Draw(gameTime, sb);
                m_explosionSystem.Draw(sb);
                sb.End();

                sb.Begin(SpriteSortMode.Deferred, BlendState.Additive);
                m_fireballSystem.Draw(sb);
                sb.End();
            }
        }
    }
}
