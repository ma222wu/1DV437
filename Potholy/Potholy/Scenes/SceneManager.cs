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
    /// SceneManager
    /// </summary>
    public class SceneManager : Microsoft.Xna.Framework.DrawableGameComponent
    {

        public GamePadState gpsc, gpsc2; //current
        public GamePadState gpsp, gpsp2; //previous

        public enum GameScenes
        {
            Start,
            Game,
            GameOver,
            Victory,
            Instructions,
            Class,
            LevelSelect,
            Store,

            COUNT
        }

        public List<GameScene> monsterScenes;
        private Scene[] m_scenes;
        public Scene[] Scenes
        {
            get { return m_scenes; }
            set { m_scenes = value; }
        }

        private Scene m_activeScene;
        public Scene m_previousScene;

        public SceneManager(Game game)
            : base(game)
        {


        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            monsterScenes = new List<GameScene>();

            //Creates an array of scenes
            m_scenes = new Scene[(int)GameScenes.COUNT];

            //Creates scenes
            m_scenes[(int)GameScenes.Start] = new StartScene(Game);
            m_scenes[(int)GameScenes.Start].Initialize();

            m_scenes[(int)GameScenes.Game] = new GameScene(Game);
            m_scenes[(int)GameScenes.Game].Initialize();

            m_scenes[(int)GameScenes.Instructions] = new InstructionsScene(Game);
            m_scenes[(int)GameScenes.Instructions].Initialize();

            m_scenes[(int)GameScenes.LevelSelect] = new LevelSelectScene(Game);
            m_scenes[(int)GameScenes.LevelSelect].Initialize();

            m_scenes[(int)GameScenes.Store] = new StoreScene(Game);
            m_scenes[(int)GameScenes.Store].Initialize();


            ChangeScene(GameScenes.Start);

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            gpsc = GamePad.GetState(PlayerIndex.One);

            if (m_activeScene != null && m_activeScene.Enabled)
                m_activeScene.Update(gameTime);


            base.Update(gameTime);
            gpsp = GamePad.GetState(PlayerIndex.One);
        }

        /// <summary>
        /// Allows the game component to draw its scenes
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            if (m_activeScene != null && m_activeScene.Visible)
                m_activeScene.Draw(gameTime);
            
            
            base.Draw(gameTime);
        }

        /// <summary>
        /// Changes the scene
        /// </summary>
        /// <param name="scene">The name (enum) of the scene to change to</param>
        public virtual void ChangeScene(GameScenes scene)
        {
            
            if (m_activeScene != null)
            {
                m_previousScene = m_activeScene;
                m_activeScene.OnExit();
                m_activeScene.Hide();
            }

            m_activeScene = m_scenes[(int)scene];
            m_activeScene.OnEnter();
            m_activeScene.Show();

        }
    }
}