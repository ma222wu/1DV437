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
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class StartScene : Scene
    {
        private FrameAnimation m_cursor;

        KeyboardState kbsc,kbsp;
        private GamePadState gpsc, gpsp;

        VideoPlayer videoPlayer;
        Video video;
        Texture2D videoTexture;
        bool videoStopped = false;
        bool firstTime = true;

        Sprite m_background;

        private int m_selection;

        int selectedSlogan = 0;
        SpriteFont fancyFont;
        SpriteFont fancyFontSmall;
        SpriteFont font50;
        SpriteFont font30;
        List<String> slogans = new List<string>();


        public StartScene(Game game)
            : base(game)
        {
            
        }

        /// <summary>
        /// Initialize
        /// </summary>
        public override void Initialize()
        {
            SpriteBatch tempSB = SpriteBatch;
            Texture2D tempTexture = Game.Content.Load<Texture2D>("cursor");
            m_cursor = new FrameAnimation(Game);
            m_cursor.Initialize(tempTexture, ref tempSB);
            m_cursor.m_transform.m_position = new Vector2(100, 500);
            m_cursor.m_frameDelay = 50;
            for (int i = 0; i < (tempTexture.Width / tempTexture.Height); i++)
                m_cursor.m_frames.Add(new Rectangle(tempTexture.Height * i, 0, tempTexture.Height, tempTexture.Height));

            videoPlayer = new VideoPlayer();

            video = Game.Content.Load<Video>("splashe2");

            tempTexture = Game.Content.Load<Texture2D>("titlescreen");

            m_background = new Sprite(Game);
            m_background.Initialize(tempTexture, ref tempSB);
            m_background.m_transform.m_depth = 1;

            Components.Add(m_cursor);
            base.Initialize();

            fancyFont = Game.Content.Load<SpriteFont>("fonts/fancyfont");
            fancyFontSmall = Game.Content.Load<SpriteFont>("fonts/fancyfontsmall");

            font50 = Game.Content.Load<SpriteFont>("fonts/font50");
            font30 = Game.Content.Load<SpriteFont>("fonts/font30");

            #region Add slogans
            slogans.Add("You a cop? You have to tell him if you're a cop.");
            slogans.Add("\"Hey, you kids wanna see a dead body?!\"");
            slogans.Add("It's okay. He's a doctor.");
            slogans.Add("CAUTION! WARM.");
            slogans.Add("FROM THE TOP ROPE!");
            slogans.Add("Too dumb for DirectX, too cheap for Unity.");
            slogans.Add("Hold still! His vision is based on movement.");
            slogans.Add("All taste, zero calories!");
            slogans.Add("\"NO ONE LEAVES HERE ALIVE 'TIL I GET MY CHOPPER!\"");
            slogans.Add("");



            #endregion

            Random random = new Random(DateTime.Now.Ticks.GetHashCode());
            selectedSlogan = random.Next(0, slogans.Count);
        }

        /// <summary>
        /// Plays the video and stops it if it's done playing. Handles input and moves the cursor accordingly.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            SceneManager sceneManager = (SceneManager)Game.Services.GetService(typeof(SceneManager));
            gpsc = GamePad.GetState(PlayerIndex.One);
            kbsc = Keyboard.GetState();

            if ((videoPlayer.PlayPosition >= video.Duration - TimeSpan.FromSeconds(1)) || 
                (gpsc.Buttons.Start == ButtonState.Pressed && gpsp.Buttons.Start != ButtonState.Pressed
                || (kbsc.IsKeyDown(Keys.Escape))))
            {
                videoStopped = true;
                (Game as GameCore).m_audioManager.PlayMusic("adia");
            }

            if (kbsc.IsKeyDown(Keys.Down) && !kbsp.IsKeyDown(Keys.Down))
            {
                if (m_selection < 2)
                    m_selection++;
            }

            else if (kbsc.IsKeyDown(Keys.Up) && !kbsp.IsKeyDown(Keys.Up))
            {
                if (m_selection > 0)
                    m_selection--;
            }

            if (gpsc.DPad.Down == ButtonState.Pressed && gpsp.DPad.Down != ButtonState.Pressed)
            {
                if (m_selection < 2)
                    m_selection++;
            }
            else if (gpsc.DPad.Up == ButtonState.Pressed && gpsp.DPad.Up != ButtonState.Pressed)
            {
                if (m_selection > 0)
                    m_selection--;
            }



            m_cursor.m_transform.m_position = new Vector2(400, 457 + 70 * m_selection);


            if (!Keyboard.GetState().IsKeyDown(Keys.Enter))
                m_justEntered = false;

            if (videoStopped && ((Keyboard.GetState().IsKeyDown(Keys.Enter) && !m_justEntered) || (gpsc.Buttons.A == ButtonState.Pressed
                    && gpsp.Buttons.A != ButtonState.Pressed)))
            {
                switch (m_selection)
                {
                    case 0:
                        //m_audioManager.Stop("music_menu");
                        sceneManager.ChangeScene(SceneManager.GameScenes.LevelSelect);
                        break;

                    case 1:
                        sceneManager.ChangeScene(SceneManager.GameScenes.Store);
                        break;

                    case 2:
                        Game.Exit();
                        break;
                }
            }
            kbsp = kbsc;
            gpsp = gpsc;
            base.Update(gameTime);
        }

        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="gameTime">Tiempo del Juego</param>
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);


            SceneManager sceneManager = (SceneManager)Game.Services.GetService(typeof(SceneManager));

            if (videoPlayer.State != MediaState.Stopped)
                videoTexture = videoPlayer.GetTexture();

            if (videoStopped)
            {
                videoPlayer.Stop();
                m_background.Draw(gameTime);
                m_cursor.Draw(gameTime);
            }

            Rectangle screen = new Rectangle(GraphicsDevice.Viewport.X,
                GraphicsDevice.Viewport.Y,
                1280,
                720);



            if (videoTexture != null && !videoStopped)
                SpriteBatch.Draw(videoTexture, screen, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);

            if (videoTexture != null && !videoStopped)
            {
                string devName = "MARKUS LOK AXELSSON";
                Rectangle tsa = GraphicsDevice.Viewport.TitleSafeArea;
#if WINDOWS || XBOX
                if (videoPlayer.PlayPosition >= TimeSpan.FromSeconds(4.3f))
                {
                    SpriteBatch.DrawString(fancyFont, slogans[selectedSlogan], new Vector2(tsa.Width * 0.5f - (fancyFont.MeasureString(slogans[selectedSlogan]).X * 0.5f - tsa.X), tsa.Height * 0.9f), Color.White);
                    SpriteBatch.DrawString(font50, devName, new Vector2(tsa.Width * 0.5f - (font50.MeasureString(devName).X * 0.5f - tsa.X), tsa.Height * 0.8f), Color.White);

                }
                SpriteBatch.Draw(videoTexture, screen, Color.White);

                if (videoPlayer.PlayPosition >= TimeSpan.FromSeconds(4.3f))
                {
                    SpriteBatch.DrawString(fancyFont, slogans[selectedSlogan], new Vector2(tsa.Width * 0.5f - (fancyFont.MeasureString(slogans[selectedSlogan]).X * 0.5f - tsa.X), tsa.Height * 0.9f), Color.White);
                    SpriteBatch.DrawString(font50, devName, new Vector2(tsa.Width * 0.5f - (font50.MeasureString(devName).X * 0.5f - tsa.X), tsa.Height * 0.8f), Color.White);


                }


#endif
            }

            SpriteBatch.End();
        }

        /// <summary>
        /// Called when entering the scene
        /// </summary>
        public override void OnEnter()
        {
            m_justEntered = true;
            if (!videoStopped)
                videoPlayer.Play(video);
            firstTime = false;
            //m_audioManager.PlaySoundFX("music_menu");
            base.OnEnter();
        }

        /// <summary>
        /// Called when exiting 
        /// </summary>
        public override void OnExit()
        {


            base.OnExit();
        }
    }
}