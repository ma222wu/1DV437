using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace LevelEditor
{
    /// <summary>
    /// A rectangle with script-code
    /// </summary>
    public class Trigger : Sprite
    {
        public List<string> m_scriptLines;
        public Trigger(Game game)
            : base(game)
        {
            m_scriptLines = new List<string>();
            m_scriptLines.Add("");
        }

        public virtual void Initialize(ref Microsoft.Xna.Framework.Graphics.Texture2D texture, ref Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Vector2 position)
        {


            base.Initialize(ref texture, ref spriteBatch);

            this.m_transform.m_position = position;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
