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

using System.Xml.Serialization;
using System.IO;
using LevelEditor;

namespace Potholy
{

    public class GameScene : Scene
    {
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        SpriteFont bigFont;


        GamePadState gpsc, gpsc2; //current
        GamePadState gpsp, gpsp2; //previous
        KeyboardState kbsc, kbsp;
        MouseState msc, msp;

        private Camera m_camera;

        SpriteBatch m_GUISpriteBatch;

        Player m_player1;
        List<Block> m_blocks = new List<Block>();
        List<Sprite> m_tiles = new List<Sprite>();
        List<Enemy> m_enemies = new List<Enemy>();
        List<Trigger> m_triggers = new List<Trigger>();
        List<Pickup> m_pickups = new List<Pickup>();

        public int m_levelNumber = 1;

        bool m_victory = false;
        bool m_death = false;
        bool m_paused = false;

        TimeSpan m_time;

        Sprite m_pauseOverlay;
        Sprite m_defeatOverlay;
        Sprite m_victoryOverlay;

        List<FireballSystem> m_fireballSystem;

        public GameScene(Game game)
            : base(game)
        {
            spriteFont = Game.Content.Load<SpriteFont>("basicfont");
            bigFont = Game.Content.Load<SpriteFont>("fonts/motorwerk50");

            m_sceneManager = (SceneManager)Game.Services.GetService(typeof(SceneManager));
            m_camera = new Camera(Game);
            m_player1 = new Player(Game);
            m_blocks = new List<Block>();

        }

        /// <summary>
        /// Initializes everything 
        /// </summary>
        public override void Initialize()
        {
            m_camera.Initialize(new Vector2(0));

            spriteBatch = SpriteBatch;
            
            m_player1.Initialize(Game.Content.Load<Texture2D>("player"),ref spriteBatch);

            m_fireballSystem = new List<FireballSystem>();
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent. Initializes the skySphere
        /// </summary>
        protected override void LoadContent()
        {
            m_GUISpriteBatch = new SpriteBatch(GraphicsDevice);

            m_pauseOverlay = new Sprite(Game);
            m_pauseOverlay.Initialize(Game.Content.Load<Texture2D>("pauseOverlay"), ref m_GUISpriteBatch);

            m_defeatOverlay = new Sprite(Game);
            m_defeatOverlay.Initialize(Game.Content.Load<Texture2D>("defeatOverlay"), ref m_GUISpriteBatch);

            m_victoryOverlay = new Sprite(Game);
            m_victoryOverlay.Initialize(Game.Content.Load<Texture2D>("victoryOverlay"), ref m_GUISpriteBatch);
        }

        /// <summary>
        /// Calls .Update on all objects that require it and checks for collisions between players and blocks, and enemies and bullets.
        /// </summary>
        /// <param name="gameTime">deltaTime</param>
        public override void Update(GameTime gameTime)
        {
            msc = Mouse.GetState();
            gpsc = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular);
            gpsc2 = GamePad.GetState(PlayerIndex.Two);
            kbsc = Keyboard.GetState();

            if ((gpsc.Buttons.Start == ButtonState.Pressed && gpsp.Buttons.Start == ButtonState.Released) || (kbsc.IsKeyDown(Keys.Escape) && kbsp.IsKeyUp(Keys.Escape)))
            {
                m_paused = !m_paused;
            }

            if (m_player1.m_health <= 0)
                m_death = true;

            if (m_paused || m_victory || m_death)
            {
                if ((gpsc.Buttons.Back == ButtonState.Pressed && gpsp.Buttons.Back == ButtonState.Released) || (kbsc.IsKeyDown(Keys.Back) && kbsp.IsKeyUp(Keys.Back)))
                {
                    Reset();
                    (Game as GameCore).m_sceneManager.ChangeScene(SceneManager.GameScenes.LevelSelect);
                }
            }

            

            if (!m_victory && !m_death && !m_paused)
            {
                foreach (Sprite s in m_tiles)
                    s.Update(gameTime);

                for (int i = 0; i < m_pickups.Count; i++)
                {
                    m_pickups[i].Update(gameTime);

                    if (m_player1.m_hitBox.Rectangle.Intersects(m_pickups[i].m_hitBox.Rectangle))
                    {
                        switch (m_pickups[i].m_pickupType)
                        {
                            case PickupType.Money:
                                (Game as GameCore).m_playerStats.m_money += 100;
                                (Game as GameCore).m_audioManager.PlaySoundFX("coin");
                                break;
                        }
                        m_pickups.RemoveAt(i);
                    }
                }

                m_time += gameTime.ElapsedGameTime;
                
                if (kbsc.IsKeyDown(Keys.A))
                {
                    m_player1.Move(Vector2.UnitX * -1);
                }
                else if (kbsc.IsKeyDown(Keys.D))
                {
                    m_player1.Move(Vector2.UnitX * 1);
                }
                else if(gpsc.ThumbSticks.Left.X != 0)
                    m_player1.Move(Vector2.UnitX * gpsc.ThumbSticks.Left.X);


                if (((kbsc.IsKeyDown(Keys.Space) && kbsp.IsKeyUp(Keys.Space)) || (gpsc.Triggers.Left > 0.5f && gpsp.Triggers.Left < 0.5f)) 
                    && (m_player1.m_bLanded || m_player1.m_slidingLeft || m_player1.m_slidingRight))
                {
                    m_player1.Jump();

                }
                if ((msc.LeftButton == ButtonState.Pressed && msp.LeftButton == ButtonState.Released) || (gpsc.Triggers.Right > 0.5f && gpsp.Triggers.Right < 0.5f))
                {
                    m_player1.Fire();
                }

                if (gpsc.Buttons.RightShoulder == ButtonState.Pressed || msc.RightButton == ButtonState.Pressed)
                {
                    m_player1.Melee();
                }




                m_player1.m_flipX = m_player1.Center.X > m_player1.m_crosshairs.Center.X;

                if (gpsc.ThumbSticks.Left.X == 0 && (kbsc.IsKeyUp(Keys.A) && kbsc.IsKeyUp(Keys.D)))
                    m_player1.m_activeAnimation = PlayerAnimations.Idle;
                else
                    m_player1.m_activeAnimation = PlayerAnimations.Run;

                if (m_player1.m_slidingLeft)
                {
                    m_player1.m_activeAnimation = PlayerAnimations.Slide;
                    m_player1.m_flipX = false;
                }
                else if (m_player1.m_slidingRight)
                {
                    m_player1.m_activeAnimation = PlayerAnimations.Slide;
                    m_player1.m_flipX = true;
                }
                if (!m_player1.CanMelee)
                {
                    m_player1.m_activeAnimation = PlayerAnimations.Melee;
                }

                if (msc != msp)
                {
                    m_player1.m_aimDirection = new Vector2(msc.X, msc.Y) - new Vector2(Game.GraphicsDevice.Viewport.Width * 0.5f, Game.GraphicsDevice.Viewport.Height * 0.5f);
                    m_player1.m_aimDirection.Normalize();
                }

                m_player1.Update(gameTime, gpsc);

                for (int i = 0; i < m_player1.m_bullets.Count; i++)
                {
                    m_player1.m_bullets[i].m_transform.m_rotation += 1f;
                    m_player1.m_bullets[i].Update(gameTime);
                    foreach(Block b in m_blocks)
                    {
                        if (m_player1.m_bullets[i].m_hitBox.Rectangle.Intersects(b.m_hitBox.Rectangle))
                        {
                            m_player1.m_bullets.RemoveAt(i);
                            break;
                        }
                    }
                }

                for (int i = 0; i < m_triggers.Count; i++)
                {
                    m_triggers[i].Update(gameTime);

                    if (m_player1.m_hitBox.Rectangle.Intersects(m_triggers[i].m_sourceRectangle))
                    {
                        if (m_triggers[i].m_scriptLines[0] == "goal")
                        {
                            (Game as GameCore).m_audioManager.PlaySoundFX("victory");
                            m_victory = true;
                            (Game as GameCore).m_playerStats.m_money += m_player1.m_money;
                            if ((Game as GameCore).m_playerStats.m_levelsCleared < m_levelNumber)
                            {
                                (Game as GameCore).m_playerStats.m_levelsCleared = m_levelNumber;
                            }
                        }
                        if (m_triggers[i].m_scriptLines[0] == "death")
                        {
                            m_death = true;
                            (Game as GameCore).m_audioManager.PlaySoundFX("death");
                        }
                    }
                }

                for (int i = 0; i < m_enemies.Count; i++)
                {
                    m_enemies[i].Update(gameTime);

                    if (m_enemies[i].m_hitBox.Rectangle.Intersects(m_player1.m_hitBox.Rectangle))
                    {
                        if (m_player1.CanMelee)
                        {
                            (Game as GameCore).m_audioManager.PlaySoundFX("pain");
                            m_player1.m_health -= 1;
                            Vector2 knockVector = (m_player1.m_transform.m_position - m_enemies[i].m_transform.m_position);
                            knockVector.Normalize();
                            knockVector.Y = 0;
                            m_player1.m_miscForces.Add(new Force(knockVector * 50, new Vector2(0.9f)));
                        }
                        else
                        {
                            Vector2 knockVector = (m_enemies[i].m_transform.m_position - m_player1.m_transform.m_position);
                            knockVector.Normalize();
                            knockVector.Y = 0;
                            m_enemies[i].m_miscForces.Add(new Force(knockVector * 50, new Vector2(0.9f)));
                        }
                    }

                    bool leftIntersectsSomething = false;
                    bool rightIntersectsSomething = false;

                    foreach (Block b in m_blocks)
                    {
                        if (m_enemies[i].m_hitBox.Rectangle.Intersects(b.m_hitBox.Rectangle))
                        {
                            if (m_enemies[i].m_transform.m_position.Y  < b.m_transform.m_position.Y//if player is below
                                )
                            {
                                m_enemies[i].Land(b.m_transform.m_position.Y);
                            }
                            else if (m_enemies[i].Center.X < b.Center.X)
                            {
                                m_enemies[i].m_walkDirection = !m_enemies[i].m_walkDirection;
                            }
                            else if (m_enemies[i].Center.X > b.Center.X)
                            {
                                m_enemies[i].m_walkDirection = !m_enemies[i].m_walkDirection;
                            }
                        }

                        if (m_enemies[i].m_lowerLeftHitbox.Rectangle.Intersects(b.m_hitBox.Rectangle))
                        {
                            leftIntersectsSomething = true;
                        }
                        if (m_enemies[i].m_lowerRightHitbox.Rectangle.Intersects(b.m_hitBox.Rectangle))
                        {
                            rightIntersectsSomething = true;
                        }
                    }

                    if (!leftIntersectsSomething)
                        m_enemies[i].m_walkDirection = true;
                    if (!rightIntersectsSomething)
                        m_enemies[i].m_walkDirection = false;

                    for (int j = 0; j < m_player1.m_bullets.Count; j++)
                    {
                        if (m_player1.m_bullets[j].m_hitBox.Rectangle.Intersects(m_enemies[i].m_hitBox.Rectangle))
                        {
                            (Game as GameCore).m_audioManager.PlaySoundFX("enemydeath");
                            m_fireballSystem.Add(new FireballSystem(Game.Content.Load<Texture2D>("particles/fire"), m_enemies[i].m_transform.m_position));

                            m_player1.m_bullets.RemoveAt(j);
                            m_enemies.RemoveAt(i);
                
                            break;
                        }
                    }
                }

                #region player/block and enemy/block collision

                bool intersectedSomething = false;
                int stairStepThreshold = 1; //if the player runs up against a wall lower than this, he will climp up on it

                foreach (Block b in m_blocks)
                {
                    b.Update(gameTime); //might as well update the blocks here since we're looping through them anyway

                    if (m_player1.m_hitBox.Rectangle.Intersects(b.m_hitBox.Rectangle))
                    {
                        intersectedSomething = true;

                        if (m_player1.m_transform.m_position.Y + m_player1.m_hitBox.m_size.Y > b.m_transform.m_position.Y//if player is below
                            && m_player1.m_previousHitBox.m_position.Y + m_player1.m_hitBox.m_size.Y - 1 < b.m_hitBox.m_position.Y //and was previously above (also have to add -1 because rects are integers and FUCKFUCKFUCKFCUKFCUFKFUCK
                            )
                        {
                            m_player1.Land(b.m_transform.m_position.Y);
                            m_player1.m_updatePrevHitbox = false;
                            m_player1.m_slidingRight = false;
                            m_player1.m_slidingLeft = false;
                        }

                        if (m_player1.m_transform.m_position.Y < b.m_transform.m_position.Y + b.m_hitBox.m_size.Y //if player is above
                            && m_player1.m_previousHitBox.m_position.Y > b.m_hitBox.m_position.Y + b.m_hitBox.m_size.Y - 1  //and was previously below (also have to add -1 because rects are integers and FUCKFUCKFUCKFCUKFCUFKFUCK
                            )
                        {
                            m_player1.m_fGravity = 0;
                            m_player1.m_transform.m_position.Y = b.m_transform.m_position.Y + b.m_hitBox.m_size.Y;
                        }

                        if (m_player1.m_transform.m_position.X <= b.m_transform.m_position.X + b.m_hitBox.m_size.X)
                        {
                            if (m_player1.m_previousHitBox.m_position.X + 1 >= b.m_hitBox.m_position.X + b.m_hitBox.m_size.X//have to add +1 to m_player1.m_previousHitBox.m_position.X because rects have only integers
                                && (m_player1.m_transform.m_position.Y + m_player1.m_hitBox.m_size.Y) - b.m_hitBox.m_position.Y > stairStepThreshold) 
                            {
                                m_player1.m_transform.m_position.X = b.m_transform.m_position.X + b.m_hitBox.m_size.X;
                                m_player1.m_updatePrevHitbox = false;
                            }
                        }


                        if (m_player1.m_transform.m_position.X + m_player1.m_hitBox.m_size.X >= b.m_transform.m_position.X)
                        {
                            if (m_player1.m_previousHitBox.m_position.X + m_player1.m_hitBox.m_size.X - 1 <= b.m_hitBox.m_position.X//have to add -1 to m_player1.m_previousHitBox.m_position.X because rects have only integers
                                 && (m_player1.m_transform.m_position.Y + m_player1.m_hitBox.m_size.Y) - b.m_hitBox.m_position.Y > stairStepThreshold)
                            {
                                m_player1.m_transform.m_position.X = b.m_hitBox.m_position.X - m_player1.m_hitBox.m_size.X;
                                m_player1.m_updatePrevHitbox = false;
                            }
                        }

                        if (m_player1.m_previousHitBox.Rectangle.Intersects(b.m_hitBox.Rectangle))
                            m_player1.Land(b.m_transform.m_position.Y);
                    }
                }

                if (!intersectedSomething)
                {
                    m_player1.m_bLanded = false;
                    m_player1.m_slidingRight = false;
                    m_player1.m_slidingLeft = false;

                    if(m_player1.m_activeAnimation != PlayerAnimations.Melee)
                        m_player1.m_activeAnimation = PlayerAnimations.Fall;
                }

                foreach (Block b in m_blocks)//loop through em again because FUCK IT
                {
                    if (!m_player1.m_bLanded)
                    {
                        if (m_player1.m_leftEdge.Rectangle.Intersects(b.m_hitBox.Rectangle))
                        {
                            m_player1.m_slidingLeft = true;
                            m_player1.m_activeAnimation = PlayerAnimations.Slide;
                        }
                        else if (m_player1.m_rightEdge.Rectangle.Intersects(b.m_hitBox.Rectangle))
                        {
                            m_player1.m_slidingRight = true;
                            m_player1.m_activeAnimation = PlayerAnimations.Slide;
                        }
                    }
                }
                #endregion

                m_camera.m_transformation.m_position = m_player1.m_transform.m_position * m_camera.m_transformation.m_scale;
                m_camera.Update(gameTime);

                m_player1.UpdateVisuals(gameTime);
                for (int i = 0; i < m_fireballSystem.Count; i++)
                {
                    m_fireballSystem[i].Update();
                }

            }

            kbsp = kbsc;
            gpsp = gpsc;
            gpsp2 = gpsc2;
            msp = msc;
        }

        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="gameTime">gameTime</param>
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(0,0,44));
            
            SamplerState ss = new SamplerState();
            ss.AddressU = TextureAddressMode.Wrap;
            ss.AddressV = TextureAddressMode.Wrap;
            ss.Filter = TextureFilter.Anisotropic;
           

            SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, ss, null, null, null,
                m_camera.m_transformationMatrix);

            m_player1.Draw(gameTime);

            foreach (Pickup p in m_pickups)
                p.Draw(gameTime);

            foreach (Sprite s in m_tiles)
                s.Draw(gameTime);

            SpriteBatch.End();

            SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, ss, null, null, null,
                m_camera.m_transformationMatrix);

            for (int i = 0; i < m_fireballSystem.Count; i++)
            {
                m_fireballSystem[i].Draw(SpriteBatch);
            }

            SpriteBatch.End();

            ss = new SamplerState();
            ss.AddressU = TextureAddressMode.Wrap;
            ss.AddressV = TextureAddressMode.Clamp;
            ss.Filter = TextureFilter.Anisotropic;

            SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, ss, null, null, null,
                m_camera.m_transformationMatrix);

                foreach (Enemy e in m_enemies)
                    e.Draw(gameTime);

            SpriteBatch.End();

            m_GUISpriteBatch.Begin();
           
            m_GUISpriteBatch.DrawString(spriteFont, "Health:" + m_player1.m_health.ToString(), new Vector2(10, 20), Color.Gold);
            m_GUISpriteBatch.DrawString(spriteFont, "Time:" + m_time.ToString(), new Vector2(10, 40), Color.Gold);

            if (m_victory)
            {
                m_victoryOverlay.Draw(gameTime);
                m_GUISpriteBatch.DrawString(bigFont, "Final time:", new Vector2(450, 220), Color.Orange);
                m_GUISpriteBatch.DrawString(bigFont, m_time.ToString(), new Vector2(400, 280), Color.Orange);
            }
            if(m_death)
                m_defeatOverlay.Draw(gameTime);

            if (m_paused)
            {
                m_pauseOverlay.Draw(gameTime);
            }

            m_GUISpriteBatch.End();
            


            base.Draw(gameTime);

        }

        /// <summary>
        /// Called when the scene is made active. Resets the player's health and what level he is on
        /// </summary>
        public override void OnEnter()
        {
            m_sceneManager = (SceneManager)Game.Services.GetService(typeof(SceneManager));

            LoadLevel("Content/xml/level" + m_levelNumber + ".xml");

            (Game as GameCore).m_audioManager.PlayMusic("power");

            m_camera.m_transformation.m_scale = Vector2.One*0.5f;

            m_player1.SetStatsFromItems();

            base.OnEnter();
        }

        /// <summary>
        /// Called when going to another scene
        /// </summary>
        public override void OnExit()
        {


            base.OnExit();
        }

        public void LoadLevel(string fileName)
        {
            Texture2D tempTexture = Game.Content.Load<Texture2D>("debugBlock");

            m_tiles.Clear();
            m_blocks.Clear();
            m_enemies.Clear();
            m_triggers.Clear();
            m_pickups.Clear();

            LevelData levelData = new LevelData();

            XmlSerializer serializer = new XmlSerializer(levelData.GetType());
            StreamReader stream;

            stream = new StreamReader(fileName);
            levelData = (LevelData)serializer.Deserialize(stream);


            stream.Close();

            foreach (Rectangle r in levelData.m_clips)
            {
                m_blocks.Add(new Block(Game));
                m_blocks[m_blocks.Count - 1].Initialize(tempTexture, ref spriteBatch, new Vector2(r.X, r.Y), new Vector2(r.Width, r.Height));
                m_blocks[m_blocks.Count - 1].m_sourceRectangle = r;

                m_blocks[m_blocks.Count - 1].m_transform.m_depth = 0;
            }

            foreach (TriggerData trd in levelData.m_triggers)
            {
                if (trd.lines[0] == "money")
                {
                    m_pickups.Add(new Pickup(Game));
                    m_pickups[m_pickups.Count-1].Initialize(Game.Content.Load<Texture2D>("money"), ref spriteBatch,PickupType.Money);
                    m_pickups[m_pickups.Count - 1].m_transform.m_position = new Vector2(trd.rectangle.X, trd.rectangle.Y);

                }
                else
                {
                    m_triggers.Add(new Trigger(Game));
                    m_triggers[m_triggers.Count - 1].Initialize(ref tempTexture, ref spriteBatch, new Vector2(trd.rectangle.X, trd.rectangle.Y));
                    m_triggers[m_triggers.Count - 1].m_sourceRectangle = trd.rectangle;
                    m_triggers[m_triggers.Count - 1].m_transform.m_depth = 0;
                    m_triggers[m_triggers.Count - 1].Alpha = 0.3f;
                    m_triggers[m_triggers.Count - 1].m_scriptLines = trd.lines;
                }
            }

            foreach (TileData td in levelData.m_tiles)
            {

                m_tiles.Add(new Sprite(Game));
                m_tiles[m_tiles.Count - 1].Initialize(Game.Content.Load<Texture2D>(td.textureName), ref spriteBatch);
                m_tiles[m_tiles.Count - 1].m_transform.m_depth = td.depth;
                m_tiles[m_tiles.Count - 1].m_transform.m_position = td.position;
                m_tiles[m_tiles.Count - 1].m_sourceRectangle.Width = (int)td.dimensions.X;
                m_tiles[m_tiles.Count - 1].m_sourceRectangle.Height = (int)td.dimensions.Y;
            }

            foreach (EnemyData ee in levelData.m_enemies)
            {
                m_enemies.Add(new Enemy(Game));
                if(ee.textureName != "")
                m_enemies[m_enemies.Count - 1].Initialize(Game.Content.Load<Texture2D>(ee.textureName), ref spriteBatch, ee.position);
                else
                    m_enemies[m_enemies.Count - 1].Initialize(Game.Content.Load<Texture2D>("enemy"), ref spriteBatch, ee.position);
                m_enemies[m_enemies.Count - 1].m_transform.m_depth = 0;
            }
        }

        public void Reset()
        {
            m_player1 = new Player(Game);
            m_player1.Initialize(Game.Content.Load<Texture2D>("player"),ref spriteBatch);
            m_player1.m_transform.m_position = Vector2.Zero;
            LoadLevel("Content/xml/level"+m_levelNumber+".xml");
            m_victory = false;
            m_death = false;
            m_paused = false;
        }
    }
}