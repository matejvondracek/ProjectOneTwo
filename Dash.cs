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
    public class Dash
    {
        public Vector2 vector;
        public int duration;

        public Dash(Vector2 direction, float speed, int _duration)
        {
            duration = _duration;
            if (direction.X != 0 && direction.Y != 0) duration -= 5;
            vector = direction * speed;
        }

        public bool Update()
        {
            if (duration > 0)
            {
                duration -= 1;
                return true;
            }
            else return false;
        }
    }
}
