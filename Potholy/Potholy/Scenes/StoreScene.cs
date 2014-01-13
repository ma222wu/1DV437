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
    enum ViewMode
    {
        Shop,
        Inventory
    }

    public class StoreScene : Scene
    {
        private Sprite m_cursor;

        KeyboardState kbsc, kbsp;
        private GamePadState gpsc, gpsp;

        Sprite m_background;
        List<Item> m_items = new List<Item>();
        

        SpriteFont fancyFont;
        SpriteFont fancyFontSmall;
        SpriteFont font50;
        SpriteFont font30;

        int m_selection;
        Vector2 m_storeItemListPosition = new Vector2(100,120);
        Vector2 m_itemStatsPosition = new Vector2(700, 120);
        float m_textHeight = 40;

        public StoreScene(Game game)
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
            m_cursor = new Sprite(Game);
            m_cursor.Initialize(tempTexture, ref tempSB);

            tempTexture = Game.Content.Load<Texture2D>("storescreen");

            m_background = new Sprite(Game);
            m_background.Initialize(tempTexture, ref tempSB);
            m_background.m_transform.m_depth = 1;

            base.Initialize();

            fancyFont = Game.Content.Load<SpriteFont>("fonts/fancyfont");
            fancyFontSmall = Game.Content.Load<SpriteFont>("fonts/fancyfontsmall");

            font50 = Game.Content.Load<SpriteFont>("fonts/font50");
            font30 = Game.Content.Load<SpriteFont>("fonts/font30");


            Random random = new Random(DateTime.Now.Ticks.GetHashCode());

            m_items.Add(new Item(100, 2,0,0,0,0,0,"Jump upgrade 1"));
            m_items.Add(new Item(200, 3, 0, 0, 0, 0, 0, "Jump upgrade 2"));
            m_items.Add(new Item(300, 0,2,0,0,0,0,"Speed upgrade 1"));
            m_items.Add(new Item(400, 0, 3, 0, 0, 0, 0, "Speed upgrade 2"));
            m_items.Add(new Item(500, 0,0,0,1,1,0,"Dash upgrade 1"));
            m_items.Add(new Item(600, 0, 0, 0, 2, 2, 0, "Dash upgrade 2"));

            
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
                if (m_selection < m_items.Count - 1)
                    m_selection++;
            }

            else if ((kbsc.IsKeyDown(Keys.Up) && !kbsp.IsKeyDown(Keys.Up)) || 
                (gpsc.DPad.Up == ButtonState.Pressed && gpsp.DPad.Up != ButtonState.Pressed))
            {
                if (m_selection > 0)
                    m_selection--;
            }

            if (!kbsc.IsKeyDown(Keys.Enter) && gpsc.Buttons.A != ButtonState.Pressed && !kbsc.IsKeyDown(Keys.Tab))
                m_justEntered = false;

            if (((kbsc.IsKeyDown(Keys.Tab) && kbsp.IsKeyUp(Keys.Tab)) || (gpsc.Buttons.Y == ButtonState.Pressed
                    && gpsp.Buttons.Y == ButtonState.Released)) && !m_justEntered)
                sceneManager.ChangeScene(SceneManager.GameScenes.LevelSelect);

            if (((kbsc.IsKeyDown(Keys.Enter) && kbsp.IsKeyUp(Keys.Enter)) || (gpsc.Buttons.A == ButtonState.Pressed
                    && gpsp.Buttons.A == ButtonState.Released)) && !m_justEntered && m_selection < m_items.Count && m_items.Count != 0)
            {
                if((Game as GameCore).m_playerStats.m_money >= m_items[m_selection].m_cost)
                {
                    (Game as GameCore).m_playerStats.m_items.Add(m_items[m_selection]);
                    (Game as GameCore).m_playerStats.m_money -= m_items[m_selection].m_cost;
                    m_items.RemoveAt(m_selection);
                    if (m_selection > m_items.Count - 1)
                        m_selection--;
                    
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


            for(int i = 0; i < m_items.Count;i++)
            {
                if(i != m_selection)
                SpriteBatch.DrawString(font30, m_items[i].m_name, m_storeItemListPosition + (Vector2.UnitY * i * m_textHeight), Color.Gold);
                else
                    SpriteBatch.DrawString(font30, m_items[i].m_name, m_storeItemListPosition + (Vector2.UnitY * i * m_textHeight), Color.Blue);
            }
            if (m_items.Count == 0)
            {
                SpriteBatch.DrawString(font30, "SOLD OUT", m_storeItemListPosition + (Vector2.UnitY * m_textHeight), Color.Blue);

            }

            if(m_items.Count != 0)
            SpriteBatch.DrawString(font30,
            "Cost: $" + m_items[m_selection].m_cost + "\n" +
            "Projectile damage : +" + m_items[m_selection].m_bulletDamage + "\n" +
            "Dash damage: +" + m_items[m_selection].m_dashDamage + "\n" +
            "Dash length: +" + m_items[m_selection].m_dashLength + "\n" +
            "Fire rate: +" + m_items[m_selection].m_gunCooldown + "\n" +
            "Jump height: +" + m_items[m_selection].m_jumpPower + "\n" +
            "Movement speed: +" + m_items[m_selection].m_runSpeed + "\n",
            m_itemStatsPosition, Color.Goldenrod);

            SpriteBatch.DrawString(font30, "You have: $" + (Game as GameCore).m_playerStats.m_money,
                new Vector2(m_storeItemListPosition.X, 550), Color.LightGreen);

            SceneManager sceneManager = (SceneManager)Game.Services.GetService(typeof(SceneManager));

            m_background.Draw(gameTime);

            SpriteBatch.End();
        }

        /// <summary>
        /// Called when entering the scene
        /// </summary>
        public override void OnEnter()
        {
            m_justEntered = true;

            RemoveAquiredItems();

            base.OnEnter();
        }

        /// <summary>
        /// Called when exiting 
        /// </summary>
        public override void OnExit()
        {


            base.OnExit();
        }

        public void RemoveAquiredItems()
        {
            for (int i = 0; i < m_items.Count; i++)
            {
                foreach (Item item in (Game as GameCore).m_playerStats.m_items)
                {
                    if (item.m_name == m_items[i].m_name)
                    {
                     
                        m_items.RemoveAt(i);
                        break;
                    }
                }
            }
        }
    }
}