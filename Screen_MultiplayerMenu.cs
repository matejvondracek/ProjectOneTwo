using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using ProjectOneTwo;

namespace ProjectOneTwo
{
    class Screen_MultiplayerMenu : Screen
    {
        Button hostButton, joinButton, backButton;
        readonly Texture2D[] buttonSprites = new Texture2D[2];
        SpriteFont spriteFont;

        public Screen_MultiplayerMenu()
        {

        }

        public override void Initialize()
        {
            buttonSprites[0] = Game1.Mycontent.Load<Texture2D>("button1");
            buttonSprites[1] = Game1.Mycontent.Load<Texture2D>("button2");
            spriteFont = Game1.Mycontent.Load<SpriteFont>("font");

            hostButton = new Button(new Vector2(20, 900), new Vector2(420, 1000), buttonSprites);
            hostButton.AddText("Host Game", spriteFont, 30, 0);

            joinButton = new Button(new Vector2(1500, 900), new Vector2(1900, 1000), buttonSprites);
            joinButton.AddText("Join Game", spriteFont, 30, 0);

            backButton = new Button(new Vector2(760, 100), new Vector2(1160, 200), buttonSprites);
            backButton.AddText("Back", spriteFont, 30, 0);
        }

        public override void LoadContent()
        {

        }

        public override ScreenManager.GameState Update(GameTime gameTime, KeyboardState keyboard, MouseState mouse)
        {
            hostButton.Update(mouse);
            if (hostButton.IsPressed())
            {
                return ScreenManager.GameState.HostGameMenu;
            }

            joinButton.Update(mouse);
            if (joinButton.IsPressed())
            {
                return ScreenManager.GameState.JoinGameMenu;
            }

            backButton.Update(mouse);
            if (backButton.IsPressed())
            {
                return ScreenManager.GameState.MainMenu;
            }
            
            return ScreenManager.GameState.Null;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            hostButton.Draw(spriteBatch);
            joinButton.Draw(spriteBatch);
            backButton.Draw(spriteBatch);
        }

        public override void ChangeTo()
        {
            Game1.self.IsMouseVisible = true;
        }
    }
}
