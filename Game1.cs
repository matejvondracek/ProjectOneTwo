
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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        Texture2D character1, background;
        Vector2 ImagePos;
        RenderTarget2D _renderTarget;
        int screenWidth, screenHeight;
        public Game1()
        {
            Content.RootDirectory = "Content";

            graphics = new GraphicsDeviceManager(this);
            
            screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        }

        protected override void Initialize()
        {            
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.ToggleFullScreen();
            graphics.ApplyChanges();
            ImagePos.Equals(new Vector2(0, 0));


            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteFont = Content.Load<SpriteFont>("font");
            character1 = Content.Load<Texture2D>("character1");
            background = Content.Load<Texture2D>("background");

            _renderTarget = new RenderTarget2D(GraphicsDevice, 1920, 1080);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();

            //exits game
            if (state.IsKeyDown(Keys.Escape))
                Exit();

            //character movement
            if (state.IsKeyDown(Keys.Right))
                ImagePos.X += 10;
            if (state.IsKeyDown(Keys.Left))
                ImagePos.X -= 10;
            if (state.IsKeyDown(Keys.Down))
                ImagePos.Y += 10;
            if (state.IsKeyDown(Keys.Up))
                ImagePos.Y -= 10;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(_renderTarget);
            GraphicsDevice.Clear(Color.CornflowerBlue);
           

            //render all to FullHD for precision movement
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);   ///prevents blurring

            spriteBatch.Draw(background, new Rectangle(0, 0, 1920, 1080), Color.White); ///streches the picture in the rectangle
            spriteBatch.Draw(character1, new Rectangle(Convert.ToInt32(ImagePos.X), Convert.ToInt32(ImagePos.Y), 16 * 6, 16 * 6), Color.White);

            spriteBatch.End();

            //scale to users monitor resolution
            GraphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);  
            
            if (screenWidth == (screenHeight * 16 / 9))   ///for 16/9 screen ratio
                spriteBatch.Draw(_renderTarget, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
            else
            {
                if (screenWidth > (screenHeight * 16 / 9))    ///for wider screens (e.g. 21/9 ratio)
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

            base.Draw(gameTime);
        }
    }
}
