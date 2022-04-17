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
    class Screen_GamePlay : Screen
    {
        Player1 player1, player2;
        readonly Physics physics;
        public Texture2D health_bar, health_bar_holder;
        Texture2D background;
        SpriteFont spriteFont;

        public Screen_GamePlay()
        {
            physics = new Physics();
        }

        public override void Initialize()
        {
        }

        public override void LoadContent()
        {
            //visuals
            background = Game1.Mycontent.Load<Texture2D>("background");
            health_bar = Game1.Mycontent.Load<Texture2D>("healthbar1");
            health_bar_holder = Game1.Mycontent.Load<Texture2D>("healthbar_holder");
            spriteFont = Game1.Mycontent.Load<SpriteFont>("aApiNyala30");

            //audio
            Game1.self.sound.Add("Main_Theme", Game1.Mycontent.Load<SoundEffect>("Main_Theme"));

            //game logic
            physics.LoadMap();
            
            player1 = new Player1(1, Keys.W, Keys.A, Keys.S, Keys.D, Keys.Space, Keys.E, Keys.LeftShift);
            player2 = new Player1(2, Keys.Up, Keys.Left, Keys.Down, Keys.Right, Keys.RightControl, Keys.RightShift, Keys.NumPad0);
            player1.LoadContent();
            player2.LoadContent();
            player1.Reset();
            player2.Reset();
            
            physics.AddEntity(ref player1);
            physics.AddEntity(ref player2);
        }

        public override ScreenManager.GameState Update(GameTime gameTime, KeyboardState keyboard, MouseState mouse)
        {
            //character movement
            player1.Keyboard(keyboard);
            player2.Keyboard(keyboard);
            physics.AttacksUpdate();
            physics.MoveUpdate();
            Game1.self.screenManager.winner = physics.GameRules();
            if (Game1.self.screenManager.winner != ScreenManager.Winner.None)
            {
                return ScreenManager.GameState.GameOver;
            }

            return ScreenManager.GameState.Null;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //background
            spriteBatch.Draw(background, new Rectangle(0, 0, 1920, 1080), Color.White); ///on bottom

            //characters
            physics.Draw(spriteBatch); ///draws characters

            //healthbars
            spriteBatch.Draw(health_bar, new Rectangle(Game1.self.PixelVector(2, 17).ToPoint(), Game1.self.PixelVector(3, player1.life / 2).ToPoint()), Color.White);
            spriteBatch.Draw(health_bar, new Rectangle(Game1.self.PixelVector(315, 17).ToPoint(), Game1.self.PixelVector(3, player2.life / 2).ToPoint()), Color.White);
            spriteBatch.Draw(health_bar_holder, new Rectangle(Game1.self.PixelVector(1, 15).ToPoint(), Game1.self.PixelVector(5, 54).ToPoint()), Color.White);
            spriteBatch.Draw(health_bar_holder, new Rectangle(Game1.self.PixelVector(314, 15).ToPoint(), Game1.self.PixelVector(5, 54).ToPoint()), Color.White);

            //life counter
            spriteBatch.DrawString(spriteFont, Convert.ToString(3 - player1.times_dead), Game1.self.PixelVector(2, 70), Color.Red);
            spriteBatch.DrawString(spriteFont, Convert.ToString(3 - player2.times_dead), Game1.self.PixelVector(315, 70), Color.Red);
        }

        public override void ChangeTo()
        {
            Game1.self.IsMouseVisible = false;

            Game1.self.screenManager.winner = ScreenManager.Winner.None;
            player1.Reset();
            player1.times_dead = 0;
            player2.Reset();
            player2.times_dead = 0;

            //playing music
            var song = Game1.self.sound["Main_Theme"].CreateInstance();
            if (!Game1.self.soundInstances.ContainsKey("Main_Theme"))
            {
                Game1.self.soundInstances.Add("Main_Theme", song);               
            }
            Game1.self.soundInstances["Main_Theme"].IsLooped = true;
            Game1.self.soundInstances["Main_Theme"].Volume = 1f * Game1.self.musicVolume;
            Game1.self.soundInstances["Main_Theme"].Play();

            //changing controls
            Screen_Settings settings = (Screen_Settings)Game1.self.screenManager.GetScreen(ScreenManager.GameState.Settings);
            player1.ChangeControls(settings.up1.key, settings.left1.key, settings.down1.key, settings.right1.key, settings.jump1.key);
            player2.ChangeControls(settings.up2.key, settings.left2.key, settings.down2.key, settings.right2.key, settings.jump2.key);
        }
    }
}
