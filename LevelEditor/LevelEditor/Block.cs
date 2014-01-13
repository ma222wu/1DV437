using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelEditor
{
    public class Block : Sprite
    {
        public Block(Game1 game)
            : base(game)
        {

        }

        public override void Initialize(ref Microsoft.Xna.Framework.Graphics.Texture2D texture, ref Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {


            base.Initialize(ref texture, ref spriteBatch);
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
