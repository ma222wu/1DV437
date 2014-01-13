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
    /// <summary>
    /// Camera
    /// </summary>
    public class Camera : Microsoft.Xna.Framework.GameComponent
    {
        public Transformation m_transformation;
        private Rectangle m_viewRectangle;
        public Matrix m_transformationMatrix;



        public Camera(Game game)
            : base(game)
        {}

        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="position">Initial position</param>
        public virtual void Initialize(Vector2 position)
        {
            m_transformation = new Transformation();
            m_transformation.m_position = position;

            base.Initialize();
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {


            m_transformationMatrix = Matrix.CreateScale(new Vector3(m_transformation.m_scale, 1.0f))
                * Matrix.CreateRotationZ(m_transformation.m_rotation) 
                * Matrix.CreateTranslation(new Vector3(Game.GraphicsDevice.Viewport.Width * 0.5f - m_transformation.m_position.X, 
                    Game.GraphicsDevice.Viewport.Height * 0.5f - m_transformation.m_position.Y, 
                    0.0f));

            m_viewRectangle = new Rectangle((int)m_transformation.m_position.X, (int)m_transformation.m_position.Y,
                                            (int)m_transformation.m_scale.X, (int)m_transformation.m_scale.Y);


            base.Update(gameTime);
        }

    }
}