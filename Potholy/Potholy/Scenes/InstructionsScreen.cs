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
    public class InstructionsScene : Scene
    {
        Texture2D m_background;

        public InstructionsScene(Game game)
            : base(game)
        {
            //m_background = Game.Content.Load<Texture2D>("screens/instruktioner");
        }

        public override void Initialize()
        {

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            m_sceneManager = (SceneManager)Game.Services.GetService(typeof(SceneManager));

            if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed)
                m_sceneManager.ChangeScene(SceneManager.GameScenes.Start);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            m_spriteBatch.Begin();
            m_spriteBatch.Draw(m_background, new Rectangle(0, 0, 1280, 720), Color.White);
            m_spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void OnEnter()
        {

            base.OnEnter();
        }

        public override void OnExit()
        {

            base.OnExit();
        }
    }
}
