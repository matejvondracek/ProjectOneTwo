using System;
using System.Collections.Generic;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectOneTwo
{

    public class Player1
    {
        KeyboardState state;
        public readonly int type;
        Keys left, right, down, up, jump, attack1, dashButton;
        public Vector2 pos, move;
        public Texture2D image, A_image_right, A_image_left;
        readonly Dictionary<string, Texture2D> images = new Dictionary<string, Texture2D>();
        public int life, times_dead, facing = 1, A_timer, A_image_timer;
        public Rectangle hitbox, drawbox;
        public bool dead, is_in_jump = false, standing, dash_charged;
        public float fall = 1f, A_wait;
        float long_jump = 80;
        bool tries_to_jump = false, A_pressed = false;
        readonly bool stunned = false;
        public Attack attack;
        public Dash dash;

        public Player1(int i)
        {
            type = i;
        }

        public void LoadContent()
        {
            images.Add("MadS1", Game1.Mycontent.Load<Texture2D>("MadS1"));
            images.Add("MadS2", Game1.Mycontent.Load<Texture2D>("MadS2"));
            A_image_right = Game1.Mycontent.Load<Texture2D>("melee_attack_right");
            A_image_left = Game1.Mycontent.Load<Texture2D>("melee_attack_left");
            image = images["MadS1"];
            hitbox = new Rectangle(0, 0, 13 * 6, 16 * 6);
            drawbox = new Rectangle(0, 0, image.Width * 6, image.Height * 6);

            Game1.self.Sounds["footsteps_in_snow"].IsLooped = true;
            Game1.self.Sounds["footsteps_in_snow"].Volume = 0;           
            Game1.self.Sounds["footsteps_in_snow"].Play();
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

            if (state.IsKeyDown(dashButton))
            {
                if (dash == null && dash_charged)
                {
                    Vector2 direction = new Vector2();
                    if (state.IsKeyDown(up)) direction.Y = -1;
                    if (state.IsKeyDown(down)) direction.Y = 1;
                    if (state.IsKeyDown(right) | state.IsKeyDown(left)) direction.X = facing;
                    if (direction.Length() == 0) direction.X = facing;
                    dash = new Dash(direction, 30, 10);
                    dash_charged = false;

                    //sound effect
                    Game1.self.Sounds["whoosh"].Volume = 1f * Game1.self.effectsVolume;
                    Game1.self.Sounds["whoosh"].Play();
                }               
            }

            if (dash != null && dash.Update())
            {
                move = dash.vector;
                fall = 1;
            }
            else
            {
                if (dash != null)
                {
                    if (dash.vector.Y < 0)
                    {                        
                        is_in_jump = true;
                    }
                    dash = null;
                }               

                if (!stunned)
                {
                    if (state.IsKeyDown(right)) move += new Vector2(10, 0);
                    if (state.IsKeyDown(left)) move += new Vector2(-10, 0);
                    if (state.IsKeyDown(jump)) tries_to_jump = true;    
                }

                if (standing && (state.IsKeyDown(right) | state.IsKeyDown(left)))
                {
                     Game1.self.Sounds["footsteps_in_snow"].Volume = 0.2f * Game1.self.effectsVolume;
                }
                else
                {
                    Game1.self.Sounds["footsteps_in_snow"].Volume *= 0.9f;
                }
            }                   
        }

        public void MakeAttack()
        {
            if (state.IsKeyDown(attack1))
            { 
                if (A_pressed == false)
                {
                    int imageDuration = 30;
                    int damage = 10;
                    Vector2 knockback = new Vector2(facing, 1);
                    A_timer = 120;
                    A_pressed = true;
                    Rectangle rectangle = new Rectangle((int)pos.X + 100 * facing, (int)pos.Y, 90, 90);
                    Texture2D A_image;
                    if (facing > 0) A_image = A_image_right;
                    else A_image = A_image_left;
                    attack = new Attack(rectangle, damage, knockback, A_image, imageDuration);

                    //sound effect
                    Game1.self.Sounds["sword_swing"].Volume = 1f * Game1.self.effectsVolume; ;
                    Game1.self.Sounds["sword_swing"].Play();

                    A_wait = 0f;
                }
                else
                {
                    attack = null;
                }
            }
            if (A_timer > 0)
            {
                A_timer -= 1;
                A_wait = A_timer / 120f;
            }
                
            if (A_timer == 0) A_pressed = false;
        }

        public void ChangeImage()
        {
            if (state.IsKeyDown(right))
            {
                image = images["MadS2"];
                facing = 1;
            }
            if (state.IsKeyDown(left))
            {
                image = images["MadS1"];
                facing = -1;
            }
        }

        public void Jump()
        {
            if (standing)
            {
                if (tries_to_jump)
                {
                    is_in_jump = true;
                    Game1.self.Sounds["jump"].Volume = 0.7f * Game1.self.effectsVolume;
                    Game1.self.Sounds["jump"].Play();
                }
                else
                {
                    is_in_jump = false;
                    fall = 1f;
                    long_jump = 80;
                }
                if (state.IsKeyUp(dashButton)) dash_charged = true;
            }
            else if (tries_to_jump)
            {
                if (long_jump > 0)
                {
                    long_jump -= 1;
                    move.Y -= 3f;
                    fall -= 0.05f;
                }
            }
            tries_to_jump = false;
            if (dash != null) is_in_jump = false;

        }

        public void GravityAcceleration()
        {
            if (move.Y != 0) if (fall < 20) fall += 0.2f; //terminal velocity
        }

        public void Reset()
        {
            life = 100;
            fall = 1f;
            dead = false;
            if (type == 1)
            {
                pos = new Vector2(100, 600);
                facing = 1;
                image = images["MadS2"];
            }
            else if (type == 2)
            {
                pos = new Vector2(1700, 600);
                facing = -1;
                image = images["MadS1"];
            }
            Update();
        }

        public void Reset(Object source, ElapsedEventArgs e)
        {
            Reset();
        }

        public void SetControls(Keys P_up, Keys P_left, Keys P_down, Keys P_right, Keys P_jump, Keys P_attack1, Keys P_dash)
        {
            left = P_left;
            right = P_right;
            down = P_down;
            up = P_up;
            jump = P_jump;
            attack1 = P_attack1;
            dashButton = P_dash;
        }
    }
}
