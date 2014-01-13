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
    public enum PickupType
    {
        Money
    }

    /// <summary>
    /// Pickup
    /// </summary>
    public class Pickup : Sprite
    {
        public CollisionBody m_hitBox;
        string m_textureName;
        Sprite debugBox;
        public PickupType m_pickupType;

        public Pickup(Game game)
            : base(game)
        {
        
        }

        public virtual void  Initialize(Texture2D texture, ref SpriteBatch spriteBatch, PickupType pickuptype)
        {
            m_hitBox = new CollisionBody(Game, new Vector2(texture.Width, texture.Height), Vector2.Zero);
            m_pickupType = pickuptype;

 	         base.Initialize(texture, ref spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            m_hitBox.Update(gameTime, m_transform.m_position);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            base.Draw(gameTime);
        }
    }
}