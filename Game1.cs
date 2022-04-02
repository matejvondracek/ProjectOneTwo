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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont, gameOverFont;
        Texture2D background, gameOverBackground;
        RenderTarget2D _renderTarget;
        readonly int screenWidth, screenHeight;
        Player1 player1, player2;     
        readonly SimpleFps fps = new SimpleFps();
        string status, winner;

        public static ContentManager Mycontent;
        public Texture2D health_bar;

        public Game1()
        {
            Content.RootDirectory = "Content";
            Mycontent = Content;

            graphics = new GraphicsDeviceManager(this);

            screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.ToggleFullScreen();
            graphics.ApplyChanges();
            player1 = new Player1(1, Keys.W, Keys.A, Keys.S, Keys.D);
            player2 = new Player1(2, Keys.Up, Keys.Left, Keys.Down, Keys.Right);

            player1.Reset();
            player2.Reset();

            status = "Playing";

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteFont = Content.Load<SpriteFont>("font");
            gameOverFont = Content.Load<SpriteFont>("font2");
            background = Content.Load<Texture2D>("background");
            gameOverBackground = Content.Load<Texture2D>("background2");
            health_bar = Content.Load<Texture2D>("healthbar1");

   
            _renderTarget = new RenderTarget2D(GraphicsDevice, 1920, 1080);

            Physics.LoadMap();
            Physics.AddEntity(ref player1);
            Physics.AddEntity(ref player2);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            fps.Update(gameTime);

            KeyboardState state = Keyboard.GetState();

            //exits game
            if (state.IsKeyDown(Keys.Escape))
                Exit();

            //character movement
            if (status == "Playing")
            {                           
                player1.Keyboard(state);
                player2.Keyboard(state);
                Physics.AttacksUpdate();
                Physics.MoveUpdate();
                string winner = Physics.GameRules();
                if (winner != "")
                {
                    status = "GameOver";
                }
            }
            


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(_renderTarget);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //render all objects to FullHD for precision movement
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);   ///prevents blurring

            if (status == "Playing")
            {
                //background
                spriteBatch.Draw(background, new Rectangle(0, 0, 1920, 1080), Color.White); ///on bottom

                //characters
                Physics.Draw(spriteBatch); ///draws characters

                //healthbars
                spriteBatch.Draw(health_bar, new Rectangle(10, 100, 10, player1.life * 5), Color.White); 
                spriteBatch.Draw(health_bar, new Rectangle(1900, 100, 10, player2.life * 5), Color.White);

                //life counter
                spriteBatch.DrawString(spriteFont, Convert.ToString(3 - player1.times_dead), new Vector2(5f, 610f), Color.Red);
                spriteBatch.DrawString(spriteFont, Convert.ToString(3 - player2.times_dead), new Vector2(1895f, 610f), Color.Red);
            }
            else if (status == "GameOver")
            {
                string line;
                if (winner == "player1")
                {
                    line = "Player1 has won the game!";
                }
                else if (winner == "player2")
                {
                    line = "Player2 has won the game!";
                }
                else
                {
                    line = "It somehow ended up as a draw...";
                }
                spriteBatch.Draw(gameOverBackground, new Rectangle(0, 0, 1920, 1080), Color.White);
                spriteBatch.DrawString(gameOverFont, line, new Vector2(500f, 500f), Color.Red);
            }

            //fps
            fps.DrawFps(spriteBatch, spriteFont, new Vector2(10f, 10f), Color.GreenYellow); 

            spriteBatch.End();

            //scale to users monitor resolution
            GraphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            if ((9 * screenWidth) == (16 * screenHeight))   ///for 16/9 screen ratio
                spriteBatch.Draw(_renderTarget, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
            else
            {
                if ((9 * screenWidth) > (16 * screenHeight))    ///for wider screens (e.g. 21/9 ratio)
                {
                    int renderWidth = screenHeight * 16 / 9;
                    int blackBar = (screenWidth - renderWidth) / 2;

                    spriteBatch.Draw(_renderTarget, new Rectangle(blackBar, 0, renderWidth, screenHeight), Color.White);
                }
                else ///for taller screens (e.g. 4/3 ratio)
                {
                    int renderHeight = screenWidth * 9 / 16;
                    int blackBar = (screenHeight - renderHeight) / 2;


                    spriteBatch.Draw(_renderTarget, new Rectangle(0, blackBar, screenWidth, renderHeight), Color.White);
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
