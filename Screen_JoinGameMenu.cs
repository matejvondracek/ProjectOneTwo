using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using Lidgren.Network;
namespace ProjectOneTwo
{
    class Screen_JoinGameMenu : Screen
    {
        Button joinButton, backButton;
        readonly List<Button> buttons = new List<Button>();
        TextBox textBox;
        readonly Texture2D[] buttonSprites = new Texture2D[2];
        SpriteFont spriteFont;
        string ipAdress = "", line = "";

        public Screen_JoinGameMenu()
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

            joinButton = new Button(new Vector2(1500, 900), new Vector2(1900, 1000), buttonSprites);
            joinButton.AddText("Start", spriteFont, 30, 0);

            backButton = new Button(new Vector2(760, 900), new Vector2(1160, 1000), buttonSprites);
            backButton.AddText("Back", spriteFont, 30, 0);

            buttons.Add(joinButton);
            buttons.Add(backButton);

            textBox = new TextBox(new Vector2(1000, 700), new Vector2(1900, 800), buttonSprites, spriteFont);
        }

        public override ScreenManager.GameState Update(GameTime gameTime, KeyboardState keyboard, MouseState mouse)
        {
            backButton.Update(mouse);
            joinButton.Update(mouse);
            textBox.Update(mouse, keyboard);
            ipAdress = textBox.text;

            if (joinButton.IsPressed())
            {
                if (Game1.self.client.connected)
                {
                    Game1.self.client.Disconnect();
                    Game1.self.peer = Game1.Peer.Offline;

                    //changing the button
                    joinButton.AddText("Join", spriteFont, 30, 0);
                }
                else
                {
                    if (Game1.self.server.running) line = "Cannot both host and join game!";
                    else if (Game1.self.client.Connect(ipAdress))
                    {
                        joinButton.AddText("Disconnect", spriteFont, 30, 0);
                        line = "";
                        Game1.self.peer = Game1.Peer.Client;
                    }
                    else
                    {
                        line = "Incorrect IP Adress!";
                    }              
                }
            }

            if (backButton.IsPressed()) return ScreenManager.GameState.MultiplayerMenu;
            EnableButtons(buttons, true);
            return ScreenManager.GameState.Null;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            joinButton.Draw(spriteBatch);
            backButton.Draw(spriteBatch);
            textBox.Draw(spriteBatch);

            spriteBatch.DrawString(spriteFont, line, new Vector2(500f, 500f), Color.Red);
        }

        public override void ChangeTo()
        {
            EnableButtons(buttons, false);
        }
    }
}
