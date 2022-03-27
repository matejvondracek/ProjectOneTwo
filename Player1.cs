﻿using System;
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

    public class Player1
    {
        KeyboardState state;
        readonly int type;
        readonly Microsoft.Xna.Framework.Input.Keys left, right, down, up;
        public Vector2 pos, move;
        public Texture2D image;
        public int life, damage;
        public Rectangle attack;

        public Player1(int i, Microsoft.Xna.Framework.Input.Keys P_left, Microsoft.Xna.Framework.Input.Keys P_right, Microsoft.Xna.Framework.Input.Keys P_down, Microsoft.Xna.Framework.Input.Keys P_up)
        {
            type = i;
            image = Game1.Mycontent.Load<Texture2D>("MadS1");
            pos = new Vector2(100, 100);
            life = 100;
            left = P_left;
            right = P_right;
            down = P_down;
            up = P_up;
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
            if (type == 1)
            {
                if (state.IsKeyDown(right))
                    move += new Vector2(10, 0);
                if (state.IsKeyDown(left))
                    move += new Vector2(-10, 0);
                if (state.IsKeyDown(up))
                    move += new Vector2(0, 10);
                if (state.IsKeyDown(down))
                    move += new Vector2(0, -10);
            }          
        }

        public void ChangeImage()
        {
            if (state.IsKeyDown(right))  
                image=Game1.Mycontent.Load<Texture2D>("MadS2");
            if (state.IsKeyDown(left))  
                image=Game1.Mycontent.Load<Texture2D>("MadS1");
        }

        public void MakeAttack()
        {

        }
    }
}
