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


namespace LevelEditor
{
    /// <summary>
    /// 2D Camera
    /// </summary>
    public class Camera : Microsoft.Xna.Framework.GameComponent
    {
        public Transformation m_transformation;
        public Rectangle m_viewRectangle;
        public Matrix m_transformationMatrix;
        public float m_speed;



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
            m_speed = 150.0f;

            base.Initialize();
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);


            m_transformationMatrix = Matrix.CreateScale(new Vector3(m_transformation.m_scale, 1.0f))
                * Matrix.CreateRotationZ(m_transformation.m_rotation) 
                * Matrix.CreateTranslation(new Vector3(Game.GraphicsDevice.Viewport.Width * 0.5f - m_transformation.m_position.X, 
                    Game.GraphicsDevice.Viewport.Height * 0.5f - m_transformation.m_position.Y, 
                    0.0f));

            //the following lines allows the camera to be moved manually for debugging purposes
            Vector2 move = Vector2.Zero;
            if (keyState.IsKeyDown(Keys.Right))
                move += Vector2.UnitX;
            else if (keyState.IsKeyDown(Keys.Left))
                move -= Vector2.UnitX;
            if (keyState.IsKeyDown(Keys.Down))
                move += Vector2.UnitY;
            else if (keyState.IsKeyDown(Keys.Up))
                move -= Vector2.UnitY;

            if (move != Vector2.Zero)
            {
                move.Normalize();
                m_transformation.m_position += move * m_speed * gameTime.ElapsedGameTime.Milliseconds * 0.001f;
            }

            m_viewRectangle = new Rectangle((int)m_transformation.m_position.X, (int)m_transformation.m_position.Y,
                                            (int)m_transformation.m_scale.X, (int)m_transformation.m_scale.Y);


            base.Update(gameTime);
        }

        #region properties
        public Matrix Transform
        {
            get { return m_transformationMatrix; }
        }

        public Transformation Transformation
        {
            get { return m_transformation; }
            set { m_transformation = value; }
        }

        public Rectangle ViewRectangle
        {
            get { return m_viewRectangle; }
            set { m_viewRectangle = value; }
        }
        #endregion
    }
}