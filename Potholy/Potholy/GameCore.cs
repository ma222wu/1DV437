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
using System.Xml.Serialization;
using System.IO;

namespace Potholy
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameCore : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public SceneManager m_sceneManager;

        public SimpleAudioManager m_audioManager;

        public PlayerStats m_playerStats = new PlayerStats();
        
        public int m_screenWidth = 1280;
        public int m_screenHeight = 720;

        public GameCore()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = m_screenWidth;
            graphics.PreferredBackBufferHeight = m_screenHeight;
            graphics.SynchronizeWithVerticalRetrace = true;
            this.IsMouseVisible = true;

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {

            try { Load(); }
            catch {  }

            m_sceneManager = new SceneManager(this);
            m_sceneManager.Initialize();
            Services.AddService(typeof(SceneManager), m_sceneManager);

            m_sceneManager.ChangeScene(SceneManager.GameScenes.Start);


            base.Initialize();
            m_audioManager = new SimpleAudioManager(this);
            m_audioManager.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            m_sceneManager.Update(gameTime);


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            m_sceneManager.Draw(gameTime);


            base.Draw(gameTime);
        }

        public void Load()
        {
            XmlSerializer serializer = new XmlSerializer(m_playerStats.GetType());
            StreamReader stream;

            stream = new StreamReader("save1");
            m_playerStats = (PlayerStats)serializer.Deserialize(stream);
            stream.Close();
        }

        public void Save()
        {
            XmlSerializer serializer = new XmlSerializer(m_playerStats.GetType());
            StreamWriter stream;

            stream = new StreamWriter("save1");
            serializer.Serialize(stream,m_playerStats);
            stream.Close();

        }
    }
}
