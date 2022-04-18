﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectOneTwo
{
    public class Attack
    {
        private Rectangle rectangle;
        public int damage;
        public Vector2 knockback;
        public Texture2D A_image;
        public int imageDuration;

        public Attack(Rectangle _rectangle, int _damage, Vector2 _knockback, Texture2D _A_image, int _imageDuration)
        {
            rectangle = _rectangle;
            damage = _damage;
            knockback = _knockback;
            A_image = _A_image;
            imageDuration = _imageDuration;
        }

        public bool Check(Rectangle character)
        {
            if (rectangle.Intersects(character))
            {
                return true;
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (imageDuration > 0)
            {
                imageDuration -= 1;
                spriteBatch.Draw(A_image, rectangle, Color.White);
            }           
        }
    }
}
