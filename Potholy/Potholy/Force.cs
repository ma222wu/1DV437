using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Potholy
{
    /// <summary>
    /// A physical force to be applied to objects
    /// </summary>
    public class Force
    {
        public Vector2 m_direction;
        public Vector2 m_decayRate;

        public Force(Vector2 direction, Vector2 decayRate)
        {
            m_direction = direction;
            m_decayRate = decayRate;
        }

        public void Update(GameTime gameTime)
        {
            m_direction *= m_decayRate;
        }
    }
}
