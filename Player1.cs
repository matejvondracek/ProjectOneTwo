using System;
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
        int type;
        public Vector2 pos, move;
        public Texture2D image;

        public Player1(int i)
        {
            type = i;
            image = Game1.Mycontent.Load<Texture2D>("MadS1");

        }
        
        public void Keyboard(KeyboardState _state)
        {
            state = _state;
            MakeVector();
            MakeAttack();
        }

        public void MakeVector()
        {
            Vector2 move = new Vector2(0, 0);            
            if (type == 1)
            {
                if (state.IsKeyDown(Keys.Right))
                    move += new Vector2(10, 0);
                if (state.IsKeyDown(Keys.Left))
                    move += new Vector2(-10, 0);
                if (state.IsKeyDown(Keys.Down))
                    move += new Vector2(0, 10);
                if (state.IsKeyDown(Keys.Up))
                    move += new Vector2(0, -10);
            }          
        }

        public void ChangeImage()
        {
            if (state.IsKeyDown(Keys.Right))  
                image=Game1.Mycontent.Load<Texture2D>("MadS2");
            if (state.IsKeyDown(Keys.Left))  
                image=Game1.Mycontent.Load<Texture2D>("MadS1");
        }

        public void MakeAttack()
        {

        }
    }
}
