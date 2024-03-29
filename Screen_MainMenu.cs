﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace ProjectOneTwo
{
    class Screen_MainMenu : Screen
    {
        readonly Texture2D[] buttonSprites = new Texture2D[2], largeButtonSprites = new Texture2D[2];
        Texture2D background;
        SpriteFont spriteFont, buttonFont;
        Button playButton, quitButton, settingsButton, creditsButton;
        readonly List<Button> buttons = new List<Button>();
        int titleAnimation; 
        float animationFactor = 1f;
        Vector2 a, b;

        public Screen_MainMenu()
        {

        }

        public override void Initialize()
        {
        }

        public override void LoadContent()
        {
            buttonSprites[0] = Game1.self.Content.Load<Texture2D>("ui/button1_snow");
            buttonSprites[1] = Game1.self.Content.Load<Texture2D>("ui/button1");
            largeButtonSprites[0] = Game1.self.Content.Load<Texture2D>("ui/button2_snow");
            largeButtonSprites[1] = Game1.self.Content.Load<Texture2D>("ui/button2");

            spriteFont = Game1.self.Content.Load<SpriteFont>("fonts/aApiNyala30");
            buttonFont = Game1.self.Content.Load<SpriteFont>("fonts/aApiNyala200");

            playButton = new Button(Game1.PixelVector(80, 105), Game1.PixelVector(240, 125), largeButtonSprites);
            playButton.AddText("Play on single device", buttonFont, 30, 10, Color.Black);
            quitButton = new Button(Game1.PixelVector(80, 155), Game1.PixelVector(240, 175), largeButtonSprites);
            quitButton.AddText("Quit", buttonFont, 10, 10, Color.Black);
            settingsButton = new Button(Game1.PixelVector(80, 130), Game1.PixelVector(155, 150), buttonSprites);
            settingsButton.AddText("Settings", buttonFont, 10, 10, Color.Black);
            creditsButton = new Button(Game1.PixelVector(165, 130), Game1.PixelVector(240, 150), buttonSprites);
            creditsButton.AddText("Credits", buttonFont, 10, 10, Color.Black);
            
            buttons.Add(playButton);
            buttons.Add(quitButton);
            buttons.Add(settingsButton);
            buttons.Add(creditsButton);

            background = Game1.self.Content.Load<Texture2D>("backgrounds/background_Default_smoothed");
        }

        public override ScreenManager.GameState Update(GameTime gameTime, KeyboardState keyboard, MouseState mouse)
        {
            //title animation
            if (titleAnimation == 0)
            {
                animationFactor = -animationFactor;
                titleAnimation = 120;
            }
            else
            {
                a.X -= animationFactor;
                a.Y -= animationFactor;
                b.X += animationFactor;
                b.Y += animationFactor;
                titleAnimation -= 1;
            }

            UpdateButtons(buttons, mouse);

            if (playButton.IsPressed()) 
            {
                Game1.self.screenManager.winner = ScreenManager.Winner.None;
                return ScreenManager.GameState.GamePlay;
            }

            if (quitButton.IsPressed()) Game1.self.Exit();

            if (settingsButton.IsPressed()) return ScreenManager.GameState.Settings;

            if (creditsButton.IsPressed()) return ScreenManager.GameState.Credits;

            EnableButtons(buttons, true);
            return ScreenManager.GameState.Null;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, 1920, 1080), Color.White);
            
            DrawButtons(buttons, spriteBatch);

            spriteBatch.DrawString(spriteFont, "Demo", new Vector2(10, 1025), Color.Black);
            
            Game1.DrawStringIn(a, b, spriteBatch, buttonFont, "Magical Flying Snow Warriors", Color.White);
        }

        public override void ChangeTo()
        {
            Game1.self.IsMouseVisible = true;

            EnableButtons(buttons, false);

            //animation
            titleAnimation = 120;
            a = Game1.PixelVector(55, 0);
            b = Game1.PixelVector(265, 60);
            animationFactor = 1f;

            //music
            if (Game1.self.Sounds.ContainsKey("The_Game")) Game1.self.Sounds["The_Game"].Stop();

            Game1.self.Sounds["The_Lobby_Music"].IsLooped = true;
            Game1.self.Sounds["The_Lobby_Music"].Volume = 1f * Game1.self.musicVolume;
            Game1.self.Sounds["The_Lobby_Music"].Play();
        }
    }
}
