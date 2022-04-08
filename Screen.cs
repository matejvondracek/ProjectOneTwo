using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectOneTwo
{
    class Screen
    {
        public Screen()
        {

        }

        public virtual void Initialize()
        {

        }

        public virtual void LoadContent()
        {

        }

        public virtual bool Update(GameTime gameTime, KeyboardState keyboard, MouseState mouse)
        {
            return false;
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }

        public virtual void ChangeTo()
        {

        }
    }
}
