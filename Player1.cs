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
        readonly Keys left, right, down, up,attack1;
        public Vector2 pos, move, knockback;
        public Texture2D image;
        public int life, damage, Width, Height, times_dead,Facing;
        public Rectangle attack, hitbox;
        public bool dead;
        public Point AttackPoint;

        public Player1(int i, Keys P_up, Keys P_left, Keys P_down, Keys P_right, Keys P_attack1)
        {
            type = i;
            image = Game1.Mycontent.Load<Texture2D>("MadS1");
            //pos = new Vector2(100, 100);
            life = 100;
            left = P_left;
            right = P_right;
            down = P_down;
            up = P_up;
            attack1 = P_attack1;

            Width = image.Width;
            Height = image.Width;
            hitbox = new Rectangle(0, 0, Width, Height);

            dead = false;
            times_dead = 0;

            Facing = 1;
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
            if (state.IsKeyDown(right))
                move += new Vector2(10, 0);
            if (state.IsKeyDown(left))
                move += new Vector2(-10, 0);
            if (state.IsKeyDown(up))
                move += new Vector2(0, -10);
            if (state.IsKeyDown(down))
                move += new Vector2(0, 10);
        }

        public void ChangeImage()
        {
            if (state.IsKeyDown(right))
            {
                image = Game1.Mycontent.Load<Texture2D>("MadS2");
                Facing = 1;
            };
            if (state.IsKeyDown(left))
            {
                image = Game1.Mycontent.Load<Texture2D>("MadS1");
                Facing = -1;
            } 
                
        }

        public void MakeAttack()
        {
            if (state.IsKeyDown(attack1))
            {
                AttackPoint = new Point(0, 0);
                AttackPoint.X = (int)pos.X;
                
                AttackPoint.X += 10*Facing;

                AttackPoint.Y = (int)pos.Y;
                attack = new Rectangle(AttackPoint.X, AttackPoint.Y, AttackPoint.X + 100*Facing, AttackPoint.Y - 100);
                damage = 10;
                knockback = new Vector2(10, 1);
            }
            else
            {
                attack = new Rectangle(0,0,0,0);
                damage = 0;
                knockback = new Vector2(0,0);

            };

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
    }
}
