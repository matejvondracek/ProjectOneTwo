using System;
using System.Collections.Generic;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectOneTwo
{
    public class ToggleButton
    {
        readonly Button button;
        public bool on;
        string onText = "", offText = "";
        SpriteFont spriteFont;
        int textBezel;

        public ToggleButton(Vector2 a, Vector2 b, Texture2D[] _textures, bool defaultState)
        {
            button = new Button(a, b, _textures);
            on = defaultState;
        }

        public void DefineText(string on, string off, SpriteFont font, int bezel)
        {
            onText = on;
            offText = off;
            spriteFont = font;
            textBezel = bezel;
        }

        public void Update(MouseState mouse)
        {
            button.Update(mouse);
            if (button.IsPressed()) on = !on;
            if (spriteFont != null)
            {
                string text;
                if (on) text = onText;
                else text = offText;
                button.AddText(text, spriteFont, textBezel, textBezel);
            }           
        }

        public void Draw(SpriteBatch spriteBatch)
        {           
            button.Draw(spriteBatch);
        }
    }
}
