using System;
using System.Collections.Generic;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectOneTwo
{
    class KeyBox : TextBox
    {
        string text;
        public Keys key;
        readonly int bezel = 10;

        public KeyBox(Vector2 a, Vector2 b, Texture2D[] _textures, SpriteFont _font, Keys defaulKey) : base(a, b, _textures, _font)
        {
            key = defaulKey;
            text = key.ToString();
            button.AddText(text, font, bezel, bezel);
        }

        protected override void HandleInput(KeyboardState keyboard)
        {
            if (keyboard.GetPressedKeyCount() > 0)
            {
                key = keyboard.GetPressedKeys()[0];
                text = key.ToString();
            }           
            button.AddText(text, font, bezel, bezel);
        }
    }
}
