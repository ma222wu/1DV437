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
    /// Animated sprite that takes horizonatally aligned sheets
    /// </summary>
    public class FrameAnimation : Sprite
    {
        private int activeFrame;
        private bool m_stop = false;
        public List<Rectangle> m_frames;
        protected TimeSpan elapsedTime;
        protected Rectangle currentFrame;
        public long m_frameDelay;


        /// <summary>
        /// Contructor - sets default values
        /// </summary>
        /// <param name="game">Game</param>
        public FrameAnimation(Game game)
            : base(game)
        {
            activeFrame = 0;
            m_frameDelay = 100;
            elapsedTime = TimeSpan.Zero;
            m_frames = new List<Rectangle>();
            m_texture = Game.Content.Load<Texture2D>("cursor");
            
        }

        public override void Initialize(Texture2D texture, ref SpriteBatch spritebatch)
        {
            this.m_texture = texture;
            this.spriteBatch = spritebatch;

            for (int i = 0; i < texture.Width / texture.Height; i++)
            {
                m_frames.Add(new Rectangle(texture.Height * i, 0, texture.Height, texture.Height));
            }

            base.Initialize();
        }

        public virtual void Initialize(Texture2D texture, ref SpriteBatch spritebatch, int frameWidth, int frameHeight)
        {
            this.m_texture = texture;
            this.spriteBatch = spritebatch;

            for (int i = 0; i < frameWidth / frameHeight; i++)
            {
                m_frames.Add(new Rectangle(frameWidth * i, 0, frameWidth, frameHeight));
            }

            base.Initialize();
        }

        /// <summary>
        /// Update
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            
            if(!m_stop)
            {
                elapsedTime += gameTime.ElapsedGameTime;

                if (elapsedTime > TimeSpan.FromMilliseconds(m_frameDelay))
                {
                    elapsedTime = TimeSpan.Zero;
                    activeFrame++;
                    if (activeFrame == m_frames.Count)
                    {
                        activeFrame = 0;
                    }
                }
            }
            
            base.Update(gameTime);
        }

        public virtual void Update(GameTime gameTime, Vector2 position)
        {

            if (!m_stop)
            {
                elapsedTime += gameTime.ElapsedGameTime;

                if (elapsedTime > TimeSpan.FromMilliseconds(m_frameDelay))
                {
                    elapsedTime = TimeSpan.Zero;
                    activeFrame++;
                    if (activeFrame == m_frames.Count)
                    {
                        activeFrame = 0;
                    }
                }
            }

            this.m_transform.m_position = position;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            m_sourceRectangle = m_frames[activeFrame];

            base.Draw(gameTime);
        }

    }
}