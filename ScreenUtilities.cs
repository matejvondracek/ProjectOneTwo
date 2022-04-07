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
    class RenderTexture2D
    {
        public Texture2D texture;
        public Rectangle rectangle;
        public RenderTexture2D(Texture2D _texture, Rectangle _rectangle)
        {
            texture = _texture;
            rectangle = _rectangle;
        }
    }

    class RenderString
    {
        public string str;
        public SpriteFont font;
        public Vector2 pos;
        public Color color;


        public RenderString(ref string _str, SpriteFont _font, Vector2 _pos, Color _color)
        {
            str = _str;
            font = _font;
            pos = _pos;
            color = _color;
        }
    }
}
