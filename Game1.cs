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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont, gameOverFont;
        Texture2D background, gameOverBackground;
        readonly Texture2D[] buttonSprites = new Texture2D[2];
        RenderTarget2D _renderTarget;
        readonly int screenWidth, screenHeight;
        Player1 player1, player2;
        readonly SimpleFps fps = new SimpleFps();
        public static ContentManager Mycontent;
        public Texture2D health_bar;
        string line = "idk";
        Button playButton, quitButton, restartButton, quitButton2;

        public enum GameState
        {
            MainMenu,
            GamePlay,
            GameOver,
            Test,
        }

        public enum Winner
        {
            Player1,
            Player2,
            Draw,
            None,
        }

        Winner winner;
        

        public Game1()
        {
            Content.RootDirectory = "Content";
            Mycontent = Content;

            graphics = new GraphicsDeviceManager(this);

            screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

                      
            //gameState = GameState.Test;
            winner = Winner.None;
        }

        protected override void Initialize()
        {
            //mouse settings
            Mouse.SetCursor(MouseCursor.Arrow);
            this.IsMouseVisible = true;

            //graphics settings
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            //graphics.ToggleFullScreen();
            graphics.ApplyChanges();

            player1 = new Player1(1, Keys.W, Keys.A, Keys.S, Keys.D);
            player2 = new Player1(2, Keys.Up, Keys.Left, Keys.Down, Keys.Right);

            player1.Reset();
            player2.Reset();
           
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
            buttonSprites[0] = Content.Load<Texture2D>("button1");
            buttonSprites[1] = Content.Load<Texture2D>("button2");

            _renderTarget = new RenderTarget2D(GraphicsDevice, 1920, 1080);

            //Physics.LoadMap();
            //Physics.AddEntity(ref player1);
            //Physics.AddEntity(ref player2);

            //load gameplay
            ScreenManager.AddScreen(GameState.GamePlay);
            ScreenManager.ChangeGameStateTo(GameState.GamePlay);
            ScreenManager.screen.LoadMap();
            ScreenManager.screen.AddEntity(ref player1);
            ScreenManager.screen.AddEntity(ref player2);
            ScreenManager.screen.AddRenderTexture2D(background, new Rectangle(0, 0, 1920, 1080));

            //load main menu
            ScreenManager.AddScreen(GameState.MainMenu);
            ScreenManager.ChangeGameStateTo(GameState.MainMenu);
            playButton = new Button(new Vector2(460, 500), new Vector2(1360, 700), buttonSprites);
            playButton.AddText("Local Multiplayer", spriteFont, 30, 0);
            ScreenManager.screen.AddButton(ref playButton);
            quitButton = new Button(new Vector2(460, 900), new Vector2(1360, 1000), buttonSprites);
            quitButton.AddText("Quit", spriteFont, 10, 10);
            ScreenManager.screen.AddButton(ref quitButton);

            //load game over screen
            ScreenManager.AddScreen(GameState.GameOver);
            ScreenManager.ChangeGameStateTo(GameState.GameOver);
            restartButton = new Button(new Vector2(20, 900), new Vector2(420, 1000), buttonSprites);
            restartButton.AddText("Play again", spriteFont, 30, 10);
            ScreenManager.screen.AddButton(ref restartButton);
            quitButton2 = new Button(new Vector2(1500, 900), new Vector2(1900, 1000), buttonSprites);
            quitButton2.AddText("Return to main menu", spriteFont, 30, 10);
            ScreenManager.screen.AddButton(ref quitButton2);
            ScreenManager.screen.AddRenderTexture2D(gameOverBackground, new Rectangle(0, 0, 1920, 1080));
            ScreenManager.screen.AddRenderString(ref line, gameOverFont, new Vector2(500f, 500f), Color.Red);

            //starting gamestate
            ScreenManager.ChangeGameStateTo(GameState.GamePlay);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            fps.Update(gameTime);

            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            //exits game
            if (keyboard.IsKeyDown(Keys.Escape))
                Exit();

            winner = ScreenManager.screen.Update(gameTime, mouse, keyboard);

            if (winner != Winner.None)
            {
                ScreenManager.ChangeGameStateTo(GameState.GameOver);
            }

            /*
            switch (gameState)
            {
                case GameState.GamePlay:
                    //character movement
                    player1.Keyboard(keyboard);
                    player2.Keyboard(keyboard);
                    //Physics.AttacksUpdate();
                    //Physics.MoveUpdate();
                    //winner = Physics.GameRules();
                    if (winner != Winner.None)
                    {
                        gameState = GameState.GameOver;
                    }

                    this.IsMouseVisible = false;
                    break;

                case GameState.MainMenu:
                    playButton.Update(mouse);
                    quitButton.Update(mouse);
                    if (playButton.IsPressed(mouse))
                    {
                        gameState = GameState.GamePlay;
                        winner = Winner.None;
                        player1.Reset();
                        player1.times_dead = 0;
                        player2.Reset();
                        player2.times_dead = 0;
                    }

                    if (quitButton.IsPressed(mouse)) Exit();

                    this.IsMouseVisible = true;
                    break;

                case GameState.GameOver:
                    restartButton.Update(mouse);
                    quitButton2.Update(mouse);
                    if (restartButton.IsPressed(mouse))
                    {
                        player1.Reset();
                        player1.times_dead = 0;
                        player2.Reset();
                        player2.times_dead = 0;
                        gameState = GameState.GamePlay;
                    }
                    if (quitButton2.IsPressed(mouse)) gameState = GameState.MainMenu;

                    this.IsMouseVisible = true;
                    break;

                case GameState.Test:
                    screen.Update(gameTime, mouse, state);
                    if (screen.buttons[0].IsPressed(mouse)) Exit();
                    break;
            }        */

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(_renderTarget);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //render all objects to FullHD for precision movement
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);   ///prevents blurring

            ScreenManager.screen.Draw(gameTime, spriteBatch);

            switch (winner)
            {
                case Winner.Player1:
                    line = "Player1 has won the game!";
                    break;

                case Winner.Player2:
                    line = "Player2 has won the game!";
                    break;

                case Winner.Draw:
                    line = "It somehow ended up as a draw...";
                    break;
                case Winner.None:
                    line = "It somehow ended up as a draw...";
                    break;
            }

            /*switch (gameState)
            {
                case GameState.GamePlay:
                    //background
                    spriteBatch.Draw(background, new Rectangle(0, 0, 1920, 1080), Color.White); ///on bottom

                    //characters
                    //Physics.Draw(spriteBatch); ///draws characters

                    //healthbars
                    spriteBatch.Draw(health_bar, new Rectangle(10, 100, 10, player1.life * 5), Color.White); 
                    spriteBatch.Draw(health_bar, new Rectangle(1900, 100, 10, player2.life * 5), Color.White);

                    //life counter
                    spriteBatch.DrawString(spriteFont, Convert.ToString(3 - player1.times_dead), new Vector2(5f, 610f), Color.Red);
                    spriteBatch.DrawString(spriteFont, Convert.ToString(3 - player2.times_dead), new Vector2(1895f, 610f), Color.Red);
                    break;

                case GameState.GameOver:                    
                    switch (winner)
                    {
                        case Winner.Player1:
                            line = "Player1 has won the game!";
                            break;

                        case Winner.Player2:
                            line = "Player2 has won the game!";
                            break;

                        case Winner.Draw:
                            line = "It somehow ended up as a draw...";
                            break;
                        case Winner.None:
                            line = "It somehow ended up as a draw...";
                            break;
                    }
                    spriteBatch.Draw(gameOverBackground, new Rectangle(0, 0, 1920, 1080), Color.White);
                    spriteBatch.DrawString(gameOverFont, line, new Vector2(500f, 500f), Color.Red);
                    restartButton.Draw(spriteBatch);
                    quitButton2.Draw(spriteBatch);
                    break;

                case GameState.MainMenu:
                    playButton.Draw(spriteBatch);
                    quitButton.Draw(spriteBatch);
                    break;

                case GameState.Test:
                    //screen.Draw(gameTime, spriteBatch);
                    break;
            }*/

            //fps
            fps.DrawFps(spriteBatch, spriteFont, new Vector2(10f, 10f), Color.GreenYellow); 

            spriteBatch.End();

            //scale to users monitor resolution
            AdjustRanderTarget();

            base.Draw(gameTime);
        }

        private void AdjustRanderTarget()
        {
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
        }
    }
}
