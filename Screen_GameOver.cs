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
        SpriteFont spriteFont, gameOverFont;
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
            spriteFont = Game1.Mycontent.Load<SpriteFont>("font");
            gameOverFont = Game1.Mycontent.Load<SpriteFont>("font2");
            buttonSprites[0] = Game1.Mycontent.Load<Texture2D>("button1");
            buttonSprites[1] = Game1.Mycontent.Load<Texture2D>("button2");
            gameOverBackground = Game1.Mycontent.Load<Texture2D>("background2");

            restartButton = new Button(new Vector2(20, 900), new Vector2(420, 1000), buttonSprites);
            restartButton.AddText("Play again", spriteFont, 30, 10);
            quitButton2 = new Button(new Vector2(460, 900), new Vector2(1360, 1000), buttonSprites);
            quitButton2.AddText("Return to main menu", spriteFont, 30, 10);
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
            spriteBatch.Draw(gameOverBackground, new Rectangle(0, 0, 1920, 1080), Color.White);
            spriteBatch.DrawString(gameOverFont, line, new Vector2(500f, 500f), Color.Red);
            restartButton.Draw(spriteBatch);
            quitButton2.Draw(spriteBatch);
        }

        public override void ChangeTo()
        {
            Game1.self.IsMouseVisible = true;
        }
    }
}
