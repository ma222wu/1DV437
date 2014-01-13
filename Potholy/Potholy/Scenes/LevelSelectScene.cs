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
    public class LevelSelectScene : Scene
    {
        private FrameAnimation m_cursor;

        KeyboardState kbsc,kbsp;
        private GamePadState gpsc, gpsp;

        Sprite m_background;

        private int m_selection;

        SpriteFont fancyFont;
        SpriteFont fancyFontSmall;
        SpriteFont font50;
        SpriteFont font30;
        List<String> slogans = new List<string>();


        public LevelSelectScene(Game game)
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

            tempTexture = Game.Content.Load<Texture2D>("levelselectscreen");

            m_background = new Sprite(Game);
            m_background.Initialize(tempTexture, ref tempSB);
            m_background.m_transform.m_depth = 1;

            Components.Add(m_cursor);
            base.Initialize();

            fancyFont = Game.Content.Load<SpriteFont>("fonts/fancyfont");
            fancyFontSmall = Game.Content.Load<SpriteFont>("fonts/fancyfontsmall");

            font50 = Game.Content.Load<SpriteFont>("fonts/font50");
            font30 = Game.Content.Load<SpriteFont>("fonts/font30");


            Random random = new Random(DateTime.Now.Ticks.GetHashCode());
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

            if ((kbsc.IsKeyDown(Keys.Down) && !kbsp.IsKeyDown(Keys.Down)) ||
                (gpsc.DPad.Down == ButtonState.Pressed && gpsp.DPad.Down != ButtonState.Pressed))
            {
                if (m_selection < 3)
                    m_selection++;
            }

            else if ((kbsc.IsKeyDown(Keys.Up) && !kbsp.IsKeyDown(Keys.Up)) ||
                (gpsc.DPad.Up == ButtonState.Pressed && gpsp.DPad.Up != ButtonState.Pressed))
            {
                if (m_selection > 0)
                    m_selection--;
            }



            m_cursor.m_transform.m_position = new Vector2(420, 360 + 55 * m_selection);

            if (((kbsc.IsKeyDown(Keys.Tab) && kbsp.IsKeyUp(Keys.Tab)) || (gpsc.Buttons.Y == ButtonState.Pressed
                    && gpsp.Buttons.Y == ButtonState.Released)) && !m_justEntered)
                sceneManager.ChangeScene(SceneManager.GameScenes.Store);

            if (!Keyboard.GetState().IsKeyDown(Keys.Enter) && gpsc.Buttons.A != ButtonState.Pressed)
                m_justEntered = false;

            if (((Keyboard.GetState().IsKeyDown(Keys.Enter)) || (gpsc.Buttons.A == ButtonState.Pressed
                    && gpsp.Buttons.A != ButtonState.Released)) && !m_justEntered)
            {
                ((sceneManager.Scenes[(int)SceneManager.GameScenes.Game]) as GameScene).m_levelNumber = m_selection;
                sceneManager.ChangeScene(SceneManager.GameScenes.Game);
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

            m_background.Draw(gameTime);
            m_cursor.Draw(gameTime);
            

            Rectangle screen = new Rectangle(GraphicsDevice.Viewport.X,
                GraphicsDevice.Viewport.Y,
                1280,
                720);

            SpriteBatch.End();
        }

        /// <summary>
        /// Called when entering the scene
        /// </summary>
        public override void OnEnter()
        {
            m_justEntered = true;

            (Game as GameCore).Save();

            base.OnEnter();
        }

        /// <summary>
        /// Called when exiting 
        /// </summary>
        public override void OnExit()
        {
            (Game as GameCore).Save();

            base.OnExit();
        }
    }
}