﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectOneTwo
{
    class KeyBox : TextBox
    {
        string text;
        public Keys key;
        readonly int bezel = 10;
        Color textColor;

        public KeyBox(Vector2 a, Vector2 b, Texture2D[] _textures, SpriteFont _font, Color color, Keys defaulKey) : base(a, b, _textures, _font, color)
        {
            key = defaulKey;
            text = key.ToString();
            button.AddText(text, font, bezel, bezel, color);
            textColor = color;
        }

        protected override void HandleInput(KeyboardState keyboard)
        {
            if (keyboard.GetPressedKeyCount() > 0)
            {
                key = keyboard.GetPressedKeys()[0];
                text = key.ToString();
            }           
            button.AddText(text, font, bezel, bezel, textColor);
        }
    }
}
