﻿using Microsoft.Xna.Framework;
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
    class Screen_GamePlay : Screen
    {
        Player1 player1, player2;
        readonly Physics physics;
        public Texture2D health_bar;
        Texture2D background;
        SpriteFont spriteFont;

        public Screen_GamePlay()
        {
            physics = new Physics();
        }

        public override void Initialize()
        {
            player1 = new Player1(1, Keys.W, Keys.A, Keys.S, Keys.D);
            player2 = new Player1(2, Keys.Up, Keys.Left, Keys.Down, Keys.Right);

            player1.Reset();
            player2.Reset();
        }

        public override void LoadContent()
        {
            background = Game1.Mycontent.Load<Texture2D>("background");
            health_bar = Game1.Mycontent.Load<Texture2D>("healthbar1");
            spriteFont = Game1.Mycontent.Load<SpriteFont>("font");

            physics.LoadMap();
            physics.AddEntity(ref player1);
            physics.AddEntity(ref player2);
        }

        public override bool Update(GameTime gameTime, KeyboardState keyboard, MouseState mouse)
        {
            //character movement
            player1.Keyboard(keyboard);
            player2.Keyboard(keyboard);
            physics.AttacksUpdate();
            physics.MoveUpdate();
            Game1.screenManager.winner = physics.GameRules();
            if (Game1.screenManager.winner != ScreenManager.Winner.None)
            {
                Game1.screenManager.gameState = ScreenManager.GameState.GameOver;
                return true;
            }

            return false;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //background
            spriteBatch.Draw(background, new Rectangle(0, 0, 1920, 1080), Color.White); ///on bottom

            //characters
            physics.Draw(spriteBatch); ///draws characters

            //healthbars
            spriteBatch.Draw(health_bar, new Rectangle(10, 100, 10, player1.life * 5), Color.White);
            spriteBatch.Draw(health_bar, new Rectangle(1900, 100, 10, player2.life * 5), Color.White);

            //life counter
            spriteBatch.DrawString(spriteFont, Convert.ToString(3 - player1.times_dead), new Vector2(5f, 610f), Color.Red);
            spriteBatch.DrawString(spriteFont, Convert.ToString(3 - player2.times_dead), new Vector2(1895f, 610f), Color.Red);
        }

        public override void ChangeTo()
        {
            Game1.self.IsMouseVisible = false;

            Game1.screenManager.winner = ScreenManager.Winner.None;
            player1.Reset();
            player1.times_dead = 0;
            player2.Reset();
            player2.times_dead = 0;
        }
    }
}
