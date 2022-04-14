using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using Lidgren;

namespace ProjectOneTwo
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        RenderTarget2D renderTarget;
        readonly int screenWidth, screenHeight;
        public static ContentManager Mycontent;
        public ScreenManager screenManager;
        public static Game1 self;

        //online stuff
        public Server server;
        public Client client;
        public bool waiting_for_connection = true, connected = false;

        public enum Peer
        {
            Offline,
            Server,
            Client,
        }
        public Peer peer = Peer.Offline;

        public Game1()
        {
            self = this; ///allows to call Game1 function from other classes

            Content.RootDirectory = "Content";
            Mycontent = Content;

            graphics = new GraphicsDeviceManager(this);
            screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

            screenManager = new ScreenManager(ScreenManager.GameState.MainMenu);

            server = new Server();
            client = new Client();
        }

        protected override void Initialize()
        {
            //mouse settings
            Mouse.SetCursor(MouseCursor.Arrow);

            //graphics settings
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            //graphics.ToggleFullScreen();
            graphics.ApplyChanges();

            screenManager.Initialize();
           
            base.Initialize();
        }

        protected override void LoadContent()
        {
            screenManager.LoadContent();

            renderTarget = new RenderTarget2D(GraphicsDevice, 1920, 1080);
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {          
            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();      
           
            switch (peer)
            {
                case Peer.Server:
                    server.Update();
                    if (waiting_for_connection) 
                    { 
                        bool start = server.StartGame();
                        waiting_for_connection = !start;
                        connected = start;
                    }                    
                    break;

                case Peer.Client:
                    client.Update();
                    if (waiting_for_connection)
                    {
                        bool start = client.StartGame();
                        waiting_for_connection = !start;
                        connected = start;
                    }
                    break;
            }

            screenManager.Update(gameTime, keyboard, mouse);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(renderTarget);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //render all objects to FullHD for precision movement
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);   ///prevents blurring
            screenManager.Draw(gameTime, spriteBatch);          
            spriteBatch.End();

            //scale to users monitor resolution
            AdjustRenderTarget();

            base.Draw(gameTime);
        }

        #region private
        void AdjustRenderTarget()
        {
            GraphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            if ((9 * screenWidth) == (16 * screenHeight))   ///for 16/9 screen ratio
                spriteBatch.Draw(renderTarget, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
            else
            {
                if ((9 * screenWidth) > (16 * screenHeight))    ///for wider screens (e.g. 21/9 ratio)
                {
                    int renderWidth = screenHeight * 16 / 9;
                    int blackBar = (screenWidth - renderWidth) / 2;

                    spriteBatch.Draw(renderTarget, new Rectangle(blackBar, 0, renderWidth, screenHeight), Color.White);
                }
                else ///for taller screens (e.g. 4/3 ratio)
                {
                    int renderHeight = screenWidth * 9 / 16;
                    int blackBar = (screenHeight - renderHeight) / 2;

                    spriteBatch.Draw(renderTarget, new Rectangle(0, blackBar, screenWidth, renderHeight), Color.White);
                }
            }
            spriteBatch.End();
        }

        #endregion
    }
}
