using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace ProjectOneTwo
{
    class Attack
    {
        private Rectangle rectangle;
        private readonly int damage;

        public Attack(Rectangle _rectangle, int _damage)
        {
            rectangle = _rectangle;
            damage = _damage;
        }

        public int Check(Vector2 pos)
        {
            if ((rectangle.X <= pos.X) && (rectangle.X + rectangle.Width >= pos.X) && (rectangle.Y <= pos.Y) && (rectangle.Y + rectangle.Height >= pos.Y))
            {
                return damage;
            }
            return 0;
        }
    }
}
