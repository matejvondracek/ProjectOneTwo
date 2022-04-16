﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using ProjectOneTwo;

namespace ProjectOneTwo
{
    class Screen_MainMenu : Screen
    {
        readonly Texture2D[] buttonSprites = new Texture2D[2];
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
            buttonSprites[0] = Game1.Mycontent.Load<Texture2D>("button1_pressed");
            buttonSprites[1] = Game1.Mycontent.Load<Texture2D>("button1_released");
            spriteFont = Game1.Mycontent.Load<SpriteFont>("aApiNyala30");
            buttonFont = Game1.Mycontent.Load<SpriteFont>("aApiNyala200");

            playButton = new Button(Game1.self.PixelVector(80, 95), Game1.self.PixelVector(240, 125), buttonSprites);
            playButton.AddText("Play on single device", buttonFont, 30, 0, Color.Black);
            quitButton = new Button(Game1.self.PixelVector(80, 155), Game1.self.PixelVector(240, 175), buttonSprites);
            quitButton.AddText("Quit", buttonFont, 10, 10, Color.Black);
            settingsButton = new Button(Game1.self.PixelVector(80, 130), Game1.self.PixelVector(155, 150), buttonSprites);
            settingsButton.AddText("Settings", buttonFont, 10, 10, Color.Black);
            creditsButton = new Button(Game1.self.PixelVector(165, 130), Game1.self.PixelVector(240, 150), buttonSprites);
            creditsButton.AddText("Credits", buttonFont, 10, 10, Color.Black);
            
            buttons.Add(playButton);
            buttons.Add(quitButton);
            buttons.Add(settingsButton);
            buttons.Add(creditsButton);
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
            DrawButtons(buttons, spriteBatch);

            spriteBatch.DrawString(spriteFont, "Pre-Alpha", new Vector2(10, 1025), Color.Black);
            
            Game1.self.DrawStringIn(a, b, spriteBatch, buttonFont, "ProjectOneTwo", Color.White);
        }

        public override void ChangeTo()
        {
            Game1.self.IsMouseVisible = true;

            EnableButtons(buttons, false);

            //animation
            titleAnimation = 120;
            a = Game1.self.PixelVector(75, 20);
            b = Game1.self.PixelVector(245, 70);
            animationFactor = 1f;

            //music
            if (Game1.self.soundInstances.ContainsKey("Main_Theme")) Game1.self.soundInstances["Main_Theme"].Stop();
        }
    }
}