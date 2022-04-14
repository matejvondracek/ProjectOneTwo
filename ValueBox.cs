using System;
using System.Collections.Generic;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectOneTwo
{
    public class ValueBox
    {
        readonly Button displayButton, upButton, downButton;
        readonly int minValue, maxValue, bezel;
        int value;
        readonly SpriteFont font;
        readonly Texture2D[] textures;

        public ValueBox(int min, int max, int _bezel, SpriteFont spriteFont, Vector2 a, Vector2 b, Texture2D[] _textures)
        {
            //setting vars
            minValue = min;
            maxValue = max;
            bezel = _bezel;
            value = (min + max) / 2;
            font = spriteFont;
            textures = _textures;

            //placing buttons
            downButton = new Button(new Vector2(a.X, a.Y), new Vector2(a.X + (b.X - a.X) / 6, b.Y), textures);
            upButton = new Button(new Vector2(a.X + (b.X - a.X) * 5 / 6, a.Y), new Vector2(b.X, b.Y), textures);
            displayButton = new Button(new Vector2(a.X + (b.X - a.X) / 6, a.Y), new Vector2(a.X + (b.X - a.X) * 5 / 6, b.Y), textures);
            upButton.AddText(">", spriteFont, _bezel, bezel);
            downButton.AddText("<", spriteFont, _bezel, bezel);
            displayButton.AddText(value.ToString(), font, bezel, bezel);
        }

        public void Update(MouseState mouse)
        {
            upButton.Update(mouse);
            downButton.Update(mouse);
            if (upButton.IsPressed())
            {
                if (value + 1 <= maxValue) value += 1;
            }
            else if (downButton.IsPressed())
            {
                if (value - 1 >= minValue) value -= 1;
            }
            displayButton.AddText(value.ToString(), font, bezel, bezel);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            displayButton.Draw(spriteBatch);
            upButton.Draw(spriteBatch);
            downButton.Draw(spriteBatch);
        }

        public void SetValue(int newValue)
        {
            if ((newValue <= maxValue) && (newValue >= minValue))
            {
                value = newValue;
            }
        }

        public int GetValue()
        {
            return value;
        }
    }
}
