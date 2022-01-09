
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

        public Game1()
        {
            Content.RootDirectory = "Content";

            graphics = new GraphicsDeviceManager(this);
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
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

            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);   ///against blur

            //render all to FullHD for precision movement  
            spriteBatch.Draw(background, new Rectangle(0, 0, 1920, 1080), Color.White); ///streches the picture in the rectangle
            spriteBatch.Draw(character1, new Rectangle(Convert.ToInt32(ImagePos.X), Convert.ToInt32(ImagePos.Y), 16 * 6, 16 * 6), Color.White);
            ///spriteBatch.DrawString(spriteFont, "hra", new Vector2(350, 200), Color.Black);

            //scale to users monitor resolution
            int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            int screenWidth = screenHeight * 16 / 9;
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
