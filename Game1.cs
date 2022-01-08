
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
        Texture2D image;
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
            image = Content.Load<Texture2D>("test");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            // ukoncí hru
            KeyboardState state = Keyboard.GetState();
         
         if (state.IsKeyDown(Keys.Escape))
                Exit();
         if (state.IsKeyDown(Keys.Right))
                    ImagePos.X += 10;
         if (state.IsKeyDown(Keys.Left))
                ImagePos.X -= 10;
         if (state.IsKeyDown(Keys.Down))
                ImagePos.Y -= 10;
            if (state.IsKeyDown(Keys.Up))
                ImagePos.Y += 10;






            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.DrawString(spriteFont, "hra", new Vector2(350, 200), Color.Black);
            spriteBatch.Draw(image,ImagePos, Color.White); //barva
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
