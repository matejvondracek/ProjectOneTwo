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
    public class Screen
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

        public virtual ScreenManager.GameState Update(GameTime gameTime, KeyboardState keyboard, MouseState mouse)
        {
            return ScreenManager.GameState.Null;
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }

        public virtual void ChangeTo()
        {
            
        }

        protected void EnableButtons(List<Button> buttons, bool b)
        {
            foreach (Button button in buttons)
            {
                if (button != null) button.enabled = b;
            }
        }

        protected void UpdateButtons(List<Button> buttons, MouseState mouse)
        {
            foreach (Button button in buttons)
            {
                button.Update(mouse);
            }
        }

        protected void DrawButtons(List<Button> buttons, SpriteBatch spriteBatch)
        {
            foreach (Button button in buttons)
            {
                button.Draw(spriteBatch);
            }
        }
    }
}
