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
    class Screen_Credits : Screen
    {
        string text;
        SpriteFont font;
        Vector2 position;
        int animationLength;

        public Screen_Credits()
        {

        }

        public override void Initialize()
        {
            string tab = "       ";
            text = "MADE BY:\n\n";
            text += "Matěj Vondráček:\n" + 
                tab + "Game design\n" +
                tab + "Code structure\n" +
                tab + "Coding\n" +
                tab + "UI design\n\n";
            text += "Tomáš Zderadička:\n" +
                tab + "Game design\n" +
                tab + "Coding\n" +
                tab + "Game mechanics\n" +
                tab + "Characters, backgrounds and animations\n\n";
            text += "Johana Vondráčková:\n" +
                tab + "Music\n\n";
            text += "Made as a school project for Gymnázium na Vítězné pláni";
        }

        public override void LoadContent()
        {
            font = Game1.Mycontent.Load<SpriteFont>("Taisean50");
        }

        public override ScreenManager.GameState Update(GameTime gameTime, KeyboardState keyboard, MouseState mouse)
        {
            if (animationLength > 0)
            {
                animationLength -= 1;
                position.Y -= 2;
                return ScreenManager.GameState.Null;
            }
            else return ScreenManager.GameState.MainMenu;          
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, text, position, Color.Black);
        }

        public override void ChangeTo()
        {
            animationLength = 1200;
            position = new Vector2(100, 1080);
        }
    }
}
