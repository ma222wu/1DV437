using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace LevelEditor
{
    /// <summary>
    /// A simple helper class contating depth, scale, translation and rotation. Not to be confused with an actual transformation matrix.
    /// </summary>
    public class Transformation
    {
        #region Members

        public Vector2 m_position;
        public Vector2 m_scale;

        public float m_rotation;
        public float m_depth;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public Transformation()
        {
            m_position = Vector2.Zero;
            m_scale = Vector2.One;
            m_rotation = 0.0f;
            m_depth = 0.5f;

        }

        public Transformation(Vector2 position, Vector2 scale, float rotation, float depth)
        {
            m_position = position;
            m_scale = scale;
            m_rotation = rotation;
            m_depth = depth;

        }
        #region Properties

        public float Depth
        {
            get { return m_depth; }
            set { m_depth = value; }
        }

        public float Rotation
        {
            get { return m_rotation; }
            set { m_rotation = value; }
        }

        public Vector2 Scale
        {
            get { return m_scale; }
            set { m_scale = value; }
        }


        public Vector2 Position
        {
            get { return m_position; }
            set { m_position = value; }
        }
        #endregion
    }
}
