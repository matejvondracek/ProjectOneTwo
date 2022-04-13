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
using System.Net;

namespace ProjectOneTwo
{
    class Screen_HostGameMenu : Screen
    {
        //screen
        Button startButton, backButton;
        readonly Texture2D[] buttonSprites = new Texture2D[2];
        SpriteFont spriteFont, bigFont;
        string line = "";

        //server
        string ipAdress = "";

        public Screen_HostGameMenu()
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
            bigFont = Game1.Mycontent.Load<SpriteFont>("font2");

            startButton = new Button(new Vector2(1500, 900), new Vector2(1900, 1000), buttonSprites);
            startButton.AddText("Start", spriteFont, 30, 0);

            backButton = new Button(new Vector2(760, 900), new Vector2(1160, 1000), buttonSprites);
            backButton.AddText("Back", spriteFont, 30, 0);
        }

        public override ScreenManager.GameState Update(GameTime gameTime, KeyboardState keyboard, MouseState mouse)
        {
            startButton.Update(mouse);
            if (startButton.IsPressed())
            {
                if (Game1.self.client.connected) line = "Cannot both host and join game!";
                else if (Game1.self.server.running)
                {
                    Game1.self.server.Shutdown();
                    Game1.self.peer = Game1.Peer.Offline;

                    //changing the button
                    startButton.AddText("Start", spriteFont, 30, 0);
                }
                else
                {
                    Game1.self.server.Start();
                    ipAdress = GetLocalIPAddress();
                    line = "Local IP Adress: " + ipAdress;
                    Game1.self.peer = Game1.Peer.Server;

                    //changing the button
                    startButton.AddText("Cancel", spriteFont, 30, 0);                   
                }
            }

            backButton.Update(mouse);
            if (backButton.IsPressed())
            {
                return ScreenManager.GameState.MultiplayerMenu;
            }

            return ScreenManager.GameState.Null;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            startButton.Draw(spriteBatch);
            backButton.Draw(spriteBatch);

            spriteBatch.DrawString(bigFont, line, new Vector2(500f, 500f), Color.Red);
        }

        public override void ChangeTo()
        {

        }

        private static string GetLocalIPAddress()
        {
            string hostName = Dns.GetHostName();
            return Dns.GetHostEntry(hostName).AddressList[1].ToString();
        }
    }
}
