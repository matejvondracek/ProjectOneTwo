using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ProjectOneTwo
{
    class Attack
    {
        private Rectangle rectangle;
        public readonly int damage;
        public Vector2 knockback;
        public Texture2D A_image;

        public Attack(Rectangle _rectangle, int _damage, Vector2 _knockback,Texture2D _A_image)
        {
            rectangle = _rectangle;
            damage = _damage;
            knockback = _knockback;
            A_image = _A_image;
        }

        public bool Check(Vector2 pos)
        {
            if ((rectangle.X <= pos.X) && (rectangle.X + rectangle.Width >= pos.X) && (rectangle.Y <= pos.Y) && (rectangle.Y + rectangle.Height >= pos.Y))
            {
                return true;
            }
            return false;
        }
    }
}
