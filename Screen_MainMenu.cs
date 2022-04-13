using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using ProjectOneTwo;

namespace ProjectOneTwo
{
    class Screen_MainMenu : Screen
    {
        readonly Texture2D[] buttonSprites = new Texture2D[2];
        SpriteFont spriteFont;
        Button playButton, quitButton, onlineButton;
        readonly List<Button> buttons = new List<Button>();

        public Screen_MainMenu()
        {

        }

        public override void Initialize()
        {
        }

        public override void LoadContent()
        {
            buttonSprites[0] = Game1.Mycontent.Load<Texture2D>("button1");
            buttonSprites[1] = Game1.Mycontent.Load<Texture2D>("button2");
            spriteFont = Game1.Mycontent.Load<SpriteFont>("font");

            onlineButton = new Button(new Vector2(460, 200), new Vector2(1360, 400), buttonSprites);
            onlineButton.AddText("Online Multiplayer", spriteFont, 30, 0);

            playButton = new Button(new Vector2(460, 500), new Vector2(1360, 700), buttonSprites);
            playButton.AddText("Local Multiplayer", spriteFont, 30, 0);

            quitButton = new Button(new Vector2(460, 900), new Vector2(1360, 1000), buttonSprites);
            quitButton.AddText("Quit", spriteFont, 10, 10);
            
            buttons.Add(playButton);
            buttons.Add(quitButton);
        }

        public override ScreenManager.GameState Update(GameTime gameTime, KeyboardState keyboard, MouseState mouse)
        {
            playButton.Update(mouse);
            quitButton.Update(mouse);
            onlineButton.Update(mouse);
            if (onlineButton.IsPressed())
            {
                Game1.self.screenManager.winner = ScreenManager.Winner.None;
                return ScreenManager.GameState.MultiplayerMenu;
            }

            playButton.Update(mouse);            
            if (playButton.IsPressed())
            {
                Game1.self.screenManager.winner = ScreenManager.Winner.None;
                return ScreenManager.GameState.GamePlay;
            }

            quitButton.Update(mouse);
            if (quitButton.IsPressed()) Game1.self.Exit();

            quitButton.enabled = true;
            EnableButtons(buttons, true);
            return ScreenManager.GameState.Null;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            playButton.Draw(spriteBatch);
            quitButton.Draw(spriteBatch);
            onlineButton.Draw(spriteBatch);
        }

        public override void ChangeTo()
        {
            Game1.self.IsMouseVisible = true;

            EnableButtons(buttons, false);
        }
    }
}
