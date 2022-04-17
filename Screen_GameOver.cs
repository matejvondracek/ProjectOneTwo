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
    class Screen_GameOver : Screen
    {
        SpriteFont buttonFont;
        Button restartButton, quitButton2;
        readonly Texture2D[] buttonSprites = new Texture2D[2];
        Texture2D gameOverBackground;

        public Screen_GameOver()
        {

        }

        public override void Initialize()
        {

        }

        public override void LoadContent()
        {
            buttonFont = Game1.Mycontent.Load<SpriteFont>("aApiNyala200");
            buttonSprites[0] = Game1.Mycontent.Load<Texture2D>("button1_pressed");
            buttonSprites[1] = Game1.Mycontent.Load<Texture2D>("button1_released");
            gameOverBackground = Game1.Mycontent.Load<Texture2D>("background2");

            restartButton = new Button(Game1.self.PixelVector(80, 155), Game1.self.PixelVector(155, 175), buttonSprites);
            restartButton.AddText("Play again", buttonFont, 30, 10, Color.Black);
            quitButton2 = new Button(Game1.self.PixelVector(165, 155), Game1.self.PixelVector(240, 175), buttonSprites);
            quitButton2.AddText("Main menu", buttonFont, 30, 10, Color.Black);
        }

        public override ScreenManager.GameState Update(GameTime gameTime, KeyboardState keyboard, MouseState mouse)
        {
            restartButton.Update(mouse);
            quitButton2.Update(mouse);
            if (restartButton.IsPressed())
            {
                return ScreenManager.GameState.GamePlay;
            }
            if (quitButton2.IsPressed()) 
            {
                return ScreenManager.GameState.MainMenu;
            }

            return ScreenManager.GameState.Null;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(gameOverBackground, new Rectangle(0, 0, 1920, 1080), Color.White);
            
            Game1.DrawStringIn(Game1.self.PixelVector(75, 20), Game1.self.PixelVector(245, 70), spriteBatch, buttonFont, "Game Over", Color.Red);

            string line = "";
            switch (Game1.self.screenManager.winner)
            {
                case ScreenManager.Winner.Player1:
                    line = "Player1 has won the game!";
                    break;

                case ScreenManager.Winner.Player2:
                    line = "Player2 has won the game!";
                    break;

                case ScreenManager.Winner.Draw:
                    line = "It somehow ended up as a draw...";
                    break;
                case ScreenManager.Winner.None:
                    line = "It somehow ended up as a draw...";
                    break;
            }
            
            Game1.DrawStringIn(Game1.self.PixelVector(75, 80), Game1.self.PixelVector(245, 120), spriteBatch, buttonFont, line, Color.Red);
            restartButton.Draw(spriteBatch);
            quitButton2.Draw(spriteBatch);
        }

        public override void ChangeTo()
        {
            Game1.self.IsMouseVisible = true;

            //turning down music
            Game1.self.Sounds["Main_Theme"].Volume = 0.3f * Game1.self.musicVolume;
            Game1.self.Sounds["howling_wind"].Stop();
        }
    }
}
