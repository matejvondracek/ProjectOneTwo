using System;
using System.Collections.Generic;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ProjectOneTwo
{

    public class Player1
    {
        KeyboardState state;
        readonly int type;
        Keys left, right, down, up, jump;
        public Vector2 pos, move, knockback;
        public Texture2D image;
        public int life = 100, damage, times_dead;
        public Rectangle attack, hitbox, drawbox;
        public bool dead, is_in_jump = false;
        public float fall = 1f;
        float long_jump = 80;
        bool tries_to_jump = false;

        public Player1(int i, Keys P_up, Keys P_left, Keys P_down, Keys P_right, Keys P_jump)
        {
            type = i;
            image = Game1.Mycontent.Load<Texture2D>("MadS1");
            left = P_left;
            right = P_right;
            down = P_down;
            up = P_up;
            jump = P_jump;

            hitbox = new Rectangle(0, 0, 13 * 6, 16 * 6);
            drawbox = new Rectangle(0, 0, image.Width * 6, image.Height * 6);

            dead = false;
            times_dead = 0;
        }

        public void Update()
        {
            hitbox.X = (int)pos.X;
            hitbox.Y = (int)pos.Y;
            drawbox.X = (int)pos.X - 22 * 6; //sprite has large empty sides
            drawbox.Y = (int)pos.Y - 10 * 6;
        }
        
        public void Keyboard(KeyboardState _state)
        {
            state = _state;
            MakeVector();
            MakeAttack();
            ChangeImage();
        }

        public void MakeVector()
        {
            move = new Vector2(0, 0);

            if (is_in_jump) move += new Vector2(0, -20);

            if (state.IsKeyDown(right)) move += new Vector2(10, 0);
            if (state.IsKeyDown(left)) move += new Vector2(-10, 0);
            if (state.IsKeyDown(jump)) tries_to_jump = true;            
        }

        public void Jump()
        {
            if (tries_to_jump && (move.Y == 0))
            {
                tries_to_jump = false;
                is_in_jump = true;
            }
            else if (move.Y == 0)
            {
                is_in_jump = false;
                fall = 1f;
                long_jump = 80;
            }
            else if (tries_to_jump)
            {
                if (long_jump > 0)
                {
                    long_jump -= 1;
                    move.Y -= 3f;
                    fall -= 0.05f;
                }
                tries_to_jump = false;
            }
            else tries_to_jump = false;                      
        }

        public void GravityAcceleration()
        {
            if (move.Y != 0) if (fall < 20) fall += 0.2f; //terminal velocity              
            else fall = 1f;
        }

        public void ChangeImage()
        {
            if (state.IsKeyDown(right)) image = Game1.Mycontent.Load<Texture2D>("MadS2");
            if (state.IsKeyDown(left)) image = Game1.Mycontent.Load<Texture2D>("MadS1");
        }

        public void MakeAttack()
        {

        }

        public void Reset()
        {
            life = 100;
            fall = 1f;
            dead = false;
            if (type == 1)
            {
                pos = new Vector2(100, 600);
            }
            else if (type == 2)
            {
                pos = new Vector2(1700, 600); 
            }
        }

        public void Reset(Object source, ElapsedEventArgs e)
        {
            Reset();
        }

        public void ChangeControls(Keys P_up, Keys P_left, Keys P_down, Keys P_right, Keys P_jump)
        {
            left = P_left;
            right = P_right;
            down = P_down;
            up = P_up;
            jump = P_jump;
        }
    }
}
