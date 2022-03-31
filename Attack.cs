using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace ProjectOneTwo
{
    class Attack
    {
        private Rectangle rectangle;
        public readonly int damage;
        public Vector2 knockback;

        public Attack(Rectangle _rectangle, int _damage, Vector2 _knockback)
        {
            rectangle = _rectangle;
            damage = _damage;
            knockback = _knockback;
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
