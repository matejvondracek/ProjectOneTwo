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
        readonly Keys left, right, down, up, jump, attack1;
        public Vector2 pos, move, knockback;
        public Texture2D image, A_image;
        readonly Dictionary<string, Texture2D> images = new Dictionary<string, Texture2D>();
        public int life, damage, Width, Height, times_dead, Facing, A_timer, A_image_timer;
        public Rectangle attack, hitbox;
        public bool dead, is_in_jump = false;
        public float fall = 1f;
        float long_jump = 80;
        bool tries_to_jump = false;

        public Player1(int i, Keys P_up, Keys P_left, Keys P_down, Keys P_right, Keys P_jump, Keys P_attack1)
        {
            type = i;
            image = Game1.Mycontent.Load<Texture2D>("MadS1");
            life = 100;
            left = P_left;
            right = P_right;
            down = P_down;
            up = P_up;
            attack1 = P_attack1;

            A_pressed = false;

            dead = false;
            times_dead = 0;

            Facing = 1;
        }
        
        public void LoadContent()
        {
            images.Add("MadS1", Game1.Mycontent.Load<Texture2D>("MadS1"));
            images.Add("MadS2", Game1.Mycontent.Load<Texture2D>("MadS2"));
            A_image = Game1.Mycontent.Load<Texture2D>("ForgAttack");
            image = images["MadS1"];
            Width = image.Width;
            Height = image.Width;
            hitbox = new Rectangle(0, 0, Width, Height);
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

            if (state.IsKeyDown(right))
                move += new Vector2(10, 0);
            if (state.IsKeyDown(left))
                move += new Vector2(-10, 0);
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
            if (move.Y != 0)
            {
                if (fall < 20) fall += 0.2f; //terminal velocity
            }               
            else fall = 1f;
        }

        public void ChangeImage()
        {
            if (state.IsKeyDown(right))  
            {
                image = images["MadS2"];
                Facing = 1;
            };
            if (state.IsKeyDown(left))
            {
                image = images["MadS1"];
                Facing = -1;
            }                 
        }

        public void MakeAttack()
        {
            if (state.IsKeyDown(attack1))
            { if (A_pressed == false)
                {
                    A_image_timer = 30;
                    attack = new Rectangle((int)pos.X + 120 * Facing, (int)pos.Y, 90, 90);
                    damage = 10;
                    knockback = new Vector2(10 * Facing, 1);
                    A_timer = 120;
                    A_pressed = true;
                }
                else 
                {                   
                    damage = 0;
                    knockback = new Vector2(0, 0);
                }             
            }
            else
            {
                if (A_image_timer > 0)
                {
                    attack = new Rectangle((int)pos.X + 100 * Facing, (int)pos.Y, 90, 90);
                    A_image_timer -= 1;
                }
                attack = new Rectangle(0,0,0,0);
                damage = 0;
                knockback = new Vector2(0,0);
            }
            if (A_timer > 0)
                A_timer -= 1;
            if (A_timer == 0)
                A_pressed = false;
        }

        public void Reset()
        {
            life = 100;
            if (type == 1)
            {
                pos = new Vector2(100, 600);
            }
            else if (type == 2)
            {
                pos = new Vector2(600, 400); 
            }
        }

        public void Reset(Object source, ElapsedEventArgs e)
        {
            life = 100;
            dead = false;
            if (type == 1)
            {
                pos = new Vector2(100, 600);
            }
            else if (type == 2)
            {
                pos = new Vector2(600, 400);
            }
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
