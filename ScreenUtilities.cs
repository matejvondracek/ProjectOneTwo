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
        string str;
        public RenderString(string _str)
        {
            str = _str;
        }
    }
}
