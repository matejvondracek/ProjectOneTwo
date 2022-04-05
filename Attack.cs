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

        public bool Check(Rectangle character)
        {
            if (rectangle.Intersects(character))
            {
                return true;
            }
            return false;
        }
    }
}
