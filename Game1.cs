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
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        RenderTarget2D renderTarget;
        readonly int screenWidth, screenHeight;
        public static ContentManager Mycontent;
        public ScreenManager screenManager;
        public float screenWidthZoom, screenHeightZoom;

        //sound
        public Dictionary<string, SoundEffect> sound = new Dictionary<string, SoundEffect>();
        public Dictionary<string, SoundEffectInstance> soundInstances = new Dictionary<string, SoundEffectInstance>();
        public float effectsVolume = 1f, musicVolume = 1f;

        public static Game1 self;

        public Game1()
        {
            self = this; ///allows to call Game1 functions from other classes

            Content.RootDirectory = "Content";
            Mycontent = Content;

            graphics = new GraphicsDeviceManager(this);
            screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

            screenWidthZoom = 1920f / screenWidth;
            screenHeightZoom = 1080f / screenHeight;

            screenManager = new ScreenManager(ScreenManager.GameState.MainMenu);
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

        #region utilities
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

        public void DrawStringIn(Vector2 a, Vector2 b, SpriteBatch spriteBatch, SpriteFont font, string text, Color color)
        {
            Rectangle rectangle = new Rectangle((int)a.X, (int)a.Y, (int)(b.X - a.X), (int)(b.Y - a.Y));
            Vector2 textSize = font.MeasureString(text);
            float xScale = (rectangle.Width / textSize.X);
            float yScale = (rectangle.Height / textSize.Y);
            float textScale = Math.Min(xScale, yScale);

            int textWidth = (int)Math.Round(textSize.X * textScale);
            int textHeight = (int)Math.Round(textSize.Y * textScale);
            Vector2 textPosition;
            textPosition.X = ((rectangle.Width - textWidth) / 2) + rectangle.X;
            textPosition.Y = ((rectangle.Height - textHeight) / 2) + rectangle.Y;
            spriteBatch.DrawString(font, text, textPosition, color, 0.0f, new Vector2(), textScale, new SpriteEffects(), 0.0f);
        }

        #endregion
    }
}
