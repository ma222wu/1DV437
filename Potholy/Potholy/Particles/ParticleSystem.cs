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
    public class ParticleSystem
    {
        protected Camera m_camera;

        protected Vector2 m_position;

        public ParticleSystem(Vector2 position)
        {
            m_position = position;

        }

        public virtual void Initialize()
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }
    }
}
