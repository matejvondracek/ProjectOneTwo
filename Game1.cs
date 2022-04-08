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
    public class Game1 : Game
    {
        static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        RenderTarget2D _renderTarget;
        readonly int screenWidth, screenHeight;
        readonly SimpleFps fps = new SimpleFps();
        public static ContentManager Mycontent;
        public Texture2D health_bar;
        public static ScreenManager screenManager;
        public static Game1 self;

        public Game1()
        {
            self = this;

            Content.RootDirectory = "Content";
            Mycontent = Content;

            graphics = new GraphicsDeviceManager(this);

            screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

            screenManager = new ScreenManager(ScreenManager.GameState.MainMenu);
        }

        protected override void Initialize()
        {
            //mouse settings
            Mouse.SetCursor(MouseCursor.Arrow);
            this.IsMouseVisible = true;

            //graphics settings
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.ToggleFullScreen();
            graphics.ApplyChanges();

            screenManager.Initialize();
           
            base.Initialize();
        }

        protected override void LoadContent()
        {
            screenManager.LoadContent();

            _renderTarget = new RenderTarget2D(GraphicsDevice, 1920, 1080);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteFont = Content.Load<SpriteFont>("font");
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

            screenManager.Update(gameTime, keyboard, mouse);          

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(_renderTarget);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //render all objects to FullHD for precision movement
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);   ///prevents blurring

            screenManager.Draw(gameTime, spriteBatch);          

            //fps
            fps.DrawFps(spriteBatch, spriteFont, new Vector2(10f, 10f), Color.GreenYellow); 

            spriteBatch.End();

            //scale to users monitor resolution
            AdjustRenderTarget();

            base.Draw(gameTime);
        }

        void AdjustRenderTarget()
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
