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
    /// Enemy
    /// </summary>
    public class Enemy : Sprite
    {
        private float m_fSpin = 0;
        private float m_fGravity = 0;
        private float m_fJumpPower = 20;
        private bool m_bLanded = false;

        public Vector2 m_aimDirection = Vector2.Zero;

        Sprite debugBox;

        public CollisionBody m_hitBox;
        public CollisionBody m_lowerLeftHitbox;
        public CollisionBody m_lowerRightHitbox;

        public bool m_walkDirection = false;

        public List<Force> m_miscForces = new List<Force>();

        public List<FrameAnimation> m_animations = new List<FrameAnimation>();
        public PlayerAnimations m_activeAnimation;

        float m_maxGravity = 50;

        public float m_health = 100;

        public Enemy(Game game)
            : base(game)
        {

        }

        /// <summary>
        /// Initialize. Sets texture.
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="spriteBatch"></param>
        public virtual void Initialize(Texture2D texture, ref SpriteBatch spriteBatch, Vector2 position)
        {

            base.Initialize(texture, ref spriteBatch);

            this.m_transform.m_position = position;

            m_hitBox = new CollisionBody(Game, new Vector2(texture.Height, texture.Height), position);

            m_lowerLeftHitbox = new CollisionBody(Game, new Vector2(texture.Height, texture.Height), position + new Vector2(-texture.Height, texture.Height));

            m_lowerRightHitbox = new CollisionBody(Game, new Vector2(texture.Height, texture.Height), position + new Vector2(texture.Height, texture.Height));

            m_animations.Add(new FrameAnimation(Game));
            m_animations[m_animations.Count - 1].Initialize(Game.Content.Load<Texture2D>("enemyRun"), ref spriteBatch);

            debugBox = new Sprite(Game);
            debugBox.Initialize(Game.Content.Load<Texture2D>("errorgrid"), ref spriteBatch);
            debugBox.m_sourceRectangle = m_hitBox.Rectangle;
            debugBox.m_sourceRectangleHotSpot = this.m_sourceRectangleHotSpot;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="gameTime">gameTime</param>
        public override void Update(GameTime gameTime)
        {
            m_transform.m_position = new Vector2(m_transform.m_position.X, m_transform.m_position.Y + m_fGravity);

            if(m_fGravity < m_maxGravity)
                m_fGravity += 0.5f;

            debugBox.m_transform.m_position = new Vector2(m_hitBox.Rectangle.Location.X, m_hitBox.Rectangle.Location.Y); //updates the debugBox
            debugBox.m_sourceRectangle = m_hitBox.Rectangle;

            if (m_walkDirection)
                m_transform.m_position += Vector2.UnitX*  1;
            else if(!m_walkDirection)
                m_transform.m_position -= Vector2.UnitX * 1;

            if (m_health <= 0)
            {
                m_transform.m_scale *= Vector2.One * 0.9f;
            }

            for (int i = 0; i < m_miscForces.Count; i++)
            {
                m_transform.m_position += m_miscForces[i].m_direction;
                m_miscForces[i].Update(gameTime);
                if (m_miscForces[i].m_direction == Vector2.Zero)
                    m_miscForces.RemoveAt(i);
            }

            m_animations[(int)m_activeAnimation].Update(gameTime, m_transform.m_position);


            m_hitBox.Update(gameTime, this.m_transform.m_position + new Vector2(0, 0));
            m_lowerLeftHitbox.Update(gameTime, this.m_transform.m_position + new Vector2(-m_hitBox.m_size.X, m_hitBox.m_size.Y));
            m_lowerRightHitbox.Update(gameTime, this.m_transform.m_position + new Vector2(m_hitBox.m_size.X, m_hitBox.m_size.Y));



            base.Update(gameTime);
        }

        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public override void Draw(GameTime gameTime)
        {
            m_flipX = !m_walkDirection;

            m_animations[(int)m_activeAnimation].m_flipX = m_flipX;

            m_animations[(int)m_activeAnimation].Draw(gameTime);

            //base.Draw(gameTime);

            //debugBox.Draw(gameTime);
        }


        /// <summary>
        /// Stops the enemy's fall and adjusts his height to the object he landed on
        /// </summary>
        /// <param name="height">the height of the object the enemy lands on</param>
        public void Land(float height)
        {
            m_fGravity = 0;
            m_transform.m_position = new Vector2(m_transform.m_position.X, height - (m_hitBox.m_size.Y - 1));
            m_bLanded = true;
        }

        /// <summary>
        /// Sets the enemy's gravity to a negative value, making him jump
        /// </summary>
        public void Jump()
        {
            m_fGravity = -m_fJumpPower;
            m_transform.m_position = new Vector2(m_transform.m_position.X, m_transform.m_position.Y - 10);
            m_bLanded = false;
        }

    }
}