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
        readonly GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        Texture2D background;
        RenderTarget2D _renderTarget;
        readonly int screenWidth, screenHeight;
        Player1 player1, player2;
        public static ContentManager Mycontent;
        readonly SimpleFps fps = new SimpleFps();


        public Game1()
        {
            Content.RootDirectory = "Content";
            Mycontent = Content;

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
            player1 = new Player1(1);
            player2 = new Player1(2);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteFont = Content.Load<SpriteFont>("font");
            background = Content.Load<Texture2D>("background");
   
            _renderTarget = new RenderTarget2D(GraphicsDevice, 1920, 1080);

            Physics.LoadMap();
            Physics.AddEntity(ref player1);

        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            fps.Update(gameTime);

            KeyboardState state = Keyboard.GetState();

            //exits game
            if (state.IsKeyDown(Keys.Escape))
                Exit();

            //character movement           
            player1.Keyboard(state);                
            Physics.MoveUpdate();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(_renderTarget);
            GraphicsDevice.Clear(Color.CornflowerBlue);


            //render all objects to FullHD for precision movement
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);   ///prevents blurring
                spriteBatch.Draw(background, new Rectangle(0, 0, 1920, 1080), Color.White); ///on bottom
                Physics.Draw(spriteBatch);       
                fps.DrawFps(spriteBatch, spriteFont, new Vector2(10f, 10f), Color.GreenYellow); ///on top
            spriteBatch.End();

            //scale to users monitor resolution
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

            base.Draw(gameTime);
        }
    }
}
