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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Potholy
{
    public enum PlayerAnimations
    {
        Run,
        Idle,
        Slide,
        Fall,
        Melee
    }

    /// <summary>
    /// Player class
    /// </summary>
    public class Player : Sprite
    {

        public float m_fGravity = 0;
        private float m_fBulletSpeed = 50;
        private float m_fJumpPower = 15;
        public bool m_bLanded = false;
        public bool m_slidingLeft;
        public bool m_slidingRight;
        
        /// <summary>
        /// Temporary money to be saved in PlayerStats on victory
        /// </summary>
        public int m_money;

        public CollisionBody m_previousHitBox;

        public Sprite m_crosshairs;
        public Vector2 m_aimDirection = Vector2.Zero;

        Sprite debugBox;

        public CollisionBody m_hitBox;
        //private List<Bullet> m_bullets;

        int m_SMGFireRate = 10;
        int m_SMGCooldown = 10;

        Texture2D m_bulletTexture;
        List<Texture2D> m_bulletTextures;

        public float m_health = 1;

        //public Vector2 m_miscForce = new Vector2();

        public List<Force> m_miscForces = new List<Force>();

        public bool m_updatePrevHitbox = true;

        public List<Bullet> m_bullets;

        public List<FrameAnimation> m_animations = new List<FrameAnimation>();
            
        public PlayerAnimations m_activeAnimation;

        List<Item> m_items = new List<Item>();

        public CollisionBody m_leftEdge;
        public CollisionBody m_rightEdge;
        public float m_edgeBoxWidth = 1;
        public float m_edgeBoxVerticalOffset = 0.75f;

        public float m_meleeRate = 300;//milliseconds
        public float m_meleeTimer;

        //public float m_jumpPower = 0;
        public float m_runSpeed = 10;
        public float m_bulletDamage = 10;
        public float m_dashDamage = 10;
        public float m_dashLength = 100;
        public float m_gunCooldown = 0;

        public Player(Game game)
            : base(game)
        {
            //Bullets = new List<Bullet>(16);
            m_bulletTextures = new List<Texture2D>(64);
        }

        /// <summary>
        /// Fills the list of bulletTextures, initializes the player's animations and changes to the 'walk'-animation
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="spriteBatch"></param>
        public override void Initialize(Texture2D texture, ref SpriteBatch spriteBatch)
        {

            debugBox = new Sprite(Game);

            debugBox.Initialize(Game.Content.Load<Texture2D>("errorgrid"), ref spriteBatch);

            this.spriteBatch = spriteBatch;

            base.Initialize(texture, ref spriteBatch);

            m_crosshairs = new Sprite(Game);
            m_crosshairs.Initialize(Game.Content.Load<Texture2D>("crosshair"), ref spriteBatch);
            m_crosshairs.m_sourceRectangleHotSpot = m_crosshairs.Center;
            m_bullets = new List<Bullet>();

            m_hitBox = new CollisionBody(Game, new Vector2(texture.Height, texture.Height), m_transform.m_position);
            m_previousHitBox = new CollisionBody(Game, new Vector2(texture.Height, texture.Height), m_transform.m_position);

            m_leftEdge = new CollisionBody(Game, new Vector2(m_edgeBoxWidth, texture.Height * m_edgeBoxVerticalOffset), m_transform.m_position);
            m_rightEdge = new CollisionBody(Game, new Vector2(m_edgeBoxWidth, texture.Height * m_edgeBoxVerticalOffset), m_transform.m_position);

            debugBox.m_sourceRectangle = m_hitBox.Rectangle;

            m_animations.Add(new FrameAnimation(Game));
            m_animations[m_animations.Count - 1].Initialize(Game.Content.Load<Texture2D>("playerRun"), ref spriteBatch);

            m_animations.Add(new FrameAnimation(Game));
            m_animations[m_animations.Count - 1].Initialize(Game.Content.Load<Texture2D>("playerIdle"), ref spriteBatch);
            m_animations[m_animations.Count - 1].m_frameDelay = 666;

            m_animations.Add(new FrameAnimation(Game));
            m_animations[m_animations.Count - 1].Initialize(Game.Content.Load<Texture2D>("playerSlideLeft"), ref spriteBatch);

            m_animations.Add(new FrameAnimation(Game));
            m_animations[m_animations.Count - 1].Initialize(Game.Content.Load<Texture2D>("playerFall"), ref spriteBatch);

            m_animations.Add(new FrameAnimation(Game));
            m_animations[m_animations.Count - 1].Initialize(Game.Content.Load<Texture2D>("playerCharge"), ref spriteBatch);
            m_animations[m_animations.Count - 1].m_frameDelay = 33;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="gameTime">gameTime</param>
        /// <param name="gpsc">current gamePadState</param>
        /// <param name="gpsp">previous keyboardState</param>
        /// <param name="kbsc">current keyboardPadState</param>
        /// <param name="kbsp">previous keyboardState</param>
        /// <param name="screenWidth">width of the screen</param>
        /// <param name="screenHeight">height of the screen</param>
        /// <param name="editMode"></param>
        public virtual void Update(GameTime gameTime, GamePadState gpsc)
        {
            //if (HitBox.m_position != Transform.m_position)
            //    m_previousHitBox.m_position = HitBox.m_position;

            if (m_updatePrevHitbox)
                m_previousHitBox.m_position = m_hitBox.m_position;
            else
                m_updatePrevHitbox = true;

            m_meleeTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            #region gravity stuffs
            if (!m_bLanded) //if the player has not landed, he is pulled downards by the gravity
            {
                if (m_slidingRight || m_slidingLeft)
                {
                    m_transform.m_position = new Vector2(m_transform.m_position.X, m_transform.m_position.Y + m_fGravity);
                    m_fGravity += 0.2f;
                }
                else
                {
                    m_transform.m_position = new Vector2(m_transform.m_position.X, m_transform.m_position.Y + m_fGravity);
                    m_fGravity += 0.5f;
                }
            }

            #endregion

            for (int i = 0; i < m_miscForces.Count; i++)
            {
                m_transform.m_position += m_miscForces[i].m_direction;
                m_miscForces[i].Update(gameTime);
                if (m_miscForces[i].m_direction == Vector2.Zero)
                    m_miscForces.RemoveAt(i);
            }

            if (gpsc.ThumbSticks.Right.X != 0 && gpsc.ThumbSticks.Right.Y != 0)
            {
                m_aimDirection = new Vector2(gpsc.ThumbSticks.Right.X, -gpsc.ThumbSticks.Right.Y);
                m_aimDirection.Normalize();
            }
            m_crosshairs.m_transform.m_position = Center + m_aimDirection * 100;




            m_hitBox.Update(gameTime, m_transform.m_position + new Vector2(0, 0));
            m_leftEdge.Update(gameTime, m_transform.m_position + new Vector2(-m_edgeBoxWidth, m_hitBox.m_size.Y * (1 - m_edgeBoxVerticalOffset)));
            m_rightEdge.Update(gameTime, m_transform.m_position + new Vector2(m_hitBox.m_size.X + m_edgeBoxWidth, m_hitBox.m_size.Y * (1 - m_edgeBoxVerticalOffset)));



            base.Update(gameTime);


        }

        public virtual void UpdateVisuals(GameTime gameTime)
        {
            m_animations[(int)m_activeAnimation].Update(gameTime, m_transform.m_position);

            m_crosshairs.Update(gameTime);

        }

        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public override void Draw(GameTime gameTime)
        {
            for (int i = 0; i < m_bullets.Count; i++)
            {
                m_bullets[i].Draw(gameTime);
            }

            m_animations[(int)m_activeAnimation].m_flipX = m_flipX;

            m_animations[(int)m_activeAnimation].Draw(gameTime);

            m_crosshairs.Draw(gameTime);
        }

        /// <summary>
        /// Creates new bullets and sends them flying in the direction of the player's left stick
        /// </summary>
        /// <param name="direction">direction the bullets will fly in</param>
        /// <param name="texture"> obsolete</param>
        public void Fire()
        {
                m_bullets.Add(new Bullet(Game, m_aimDirection * m_fBulletSpeed));
                m_bullets[m_bullets.Count - 1].Initialize(Game.Content.Load<Texture2D>("bullet"), ref spriteBatch);
                m_bullets[m_bullets.Count - 1].Center = m_crosshairs.Center;
                (Game as GameCore).m_audioManager.PlaySoundFX("laser");
        }

        /// <summary>
        /// Stops the player's fall and adjusts his height to the object he landed on
        /// </summary>
        /// <param name="height">the height of the object the player lands on</param>
        public void Land(float height)
        {
            m_fGravity = 0;
            m_transform.m_position = new Vector2(m_transform.m_position.X, height - (m_hitBox.m_size.Y - 1));
            m_bLanded = true;
            for (int i = 0; i < m_miscForces.Count; i++)
                m_miscForces[i].m_direction.Y = 0;

        }

        /// <summary>
        /// Sets the player's gravity to a negative value, making him jump
        /// </summary>
        public void Jump()
        {
            if (m_slidingLeft)
            {
                m_fGravity = -m_fJumpPower*0.5f;
                m_miscForces.Add(new Force(new Vector2(40, 0), Vector2.One * 0.9f));
                m_transform.m_position = new Vector2(m_transform.m_position.X + 10, m_transform.m_position.Y);
            }
            else if (m_slidingRight)
            {
                m_fGravity = -m_fJumpPower * 0.5f;
                m_miscForces.Add(new Force(new Vector2(-40, 0), Vector2.One * 0.9f));

                m_transform.m_position = new Vector2(m_transform.m_position.X - 10, m_transform.m_position.Y);
            }
            else
            {
                m_fGravity = -m_fJumpPower;
                m_transform.m_position = new Vector2(m_transform.m_position.X, m_transform.m_position.Y - 10);
            }
                m_bLanded = false;

                (Game as GameCore).m_audioManager.PlaySoundFX("jump");
        }

        public void Melee()
        {
            if (m_meleeTimer > m_meleeRate)
            {
                m_meleeTimer = 0;

                Force f = new Force(Vector2.Zero, Vector2.One * 0.7f);
                if (Center.X > m_crosshairs.Center.X)
                    f.m_direction.X = -1 * m_dashLength;
                else
                    f.m_direction.X = 1 * m_dashLength;

                m_miscForces.Add(f);
                (Game as GameCore).m_audioManager.PlaySoundFX("dash");
            }
        }

        /// <summary>
        /// Method for when user moves player
        /// </summary>
        public void Move(Vector2 direction)
        {
            direction.Normalize();

            m_transform.m_position += new Vector2(direction.X * m_runSpeed, 0);
            m_activeAnimation = PlayerAnimations.Run;
            m_flipX = true;
        }

        public void SetStatsFromItems()
        {
            for (int i = 0; i < (Game as GameCore).m_playerStats.m_items.Count; i++)
            {
                m_fJumpPower += (Game as GameCore).m_playerStats.m_items[i].m_jumpPower;
                m_runSpeed += (Game as GameCore).m_playerStats.m_items[i].m_runSpeed;
                m_bulletDamage += (Game as GameCore).m_playerStats.m_items[i].m_bulletDamage;
                m_dashDamage += (Game as GameCore).m_playerStats.m_items[i].m_dashDamage;
                m_dashLength += (Game as GameCore).m_playerStats.m_items[i].m_dashLength;
                m_gunCooldown -= (Game as GameCore).m_playerStats.m_items[i].m_gunCooldown;
            }
        }

        public bool CanMelee
        {
            get { return m_meleeTimer > m_meleeRate; }
        }

    }
}