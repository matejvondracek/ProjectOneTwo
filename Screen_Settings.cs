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
    class Screen_Settings : Screen
    {
        Button backButton;
        ValueBox masterVolumeBox, effectsVolumeBox, musicVolumeBox;
        public Dictionary<string, KeyBox> Keyboxes = new Dictionary<string, KeyBox>();
        public ToggleButton fpsToggle;
        SpriteFont spriteFont, buttonFont;
        readonly Texture2D[] buttonSprites = new Texture2D[2], shortButtonSprites = new Texture2D[2];

        public Screen_Settings()
        {

        }

        public override void Initialize()
        {
        }

        public override void LoadContent()
        {
            buttonSprites[0] = Game1.Mycontent.Load<Texture2D>("button1_snow");
            buttonSprites[1] = Game1.Mycontent.Load<Texture2D>("button1");

            shortButtonSprites[0] = Game1.Mycontent.Load<Texture2D>("button3_snow");
            shortButtonSprites[1] = Game1.Mycontent.Load<Texture2D>("button3");

            spriteFont = Game1.Mycontent.Load<SpriteFont>("aApiNyala30");
            buttonFont = Game1.Mycontent.Load<SpriteFont>("aApiNyala200");

            backButton = new Button(Game1.PixelVector(10, 150), Game1.PixelVector(85, 170), buttonSprites);
            backButton.AddText("Back", buttonFont, 30, 0, Color.Black);

            masterVolumeBox = new ValueBox(0, 10, 2, buttonFont, Color.Black, new Vector2(20, 200), new Vector2(350, 250), buttonSprites);
            masterVolumeBox.SetValue(10);
            effectsVolumeBox = new ValueBox(0, 10, 2, buttonFont, Color.Black, new Vector2(20, 300), new Vector2(350, 350), buttonSprites);
            effectsVolumeBox.SetValue(10);
            musicVolumeBox = new ValueBox(0, 10, 2, buttonFont, Color.Black, new Vector2(20, 400), new Vector2(350, 450), buttonSprites);
            musicVolumeBox.SetValue(10);


            Color color = Color.Black;
            Keyboxes.Add("up1", new KeyBox(Game1.PixelVector(115, 30), Game1.PixelVector(150, 50), shortButtonSprites, buttonFont, color, Keys.W));
            Keyboxes.Add("up2", new KeyBox(Game1.PixelVector(115, 100), Game1.PixelVector(150, 120), shortButtonSprites, buttonFont, color, Keys.Up));
            Keyboxes.Add("left1", new KeyBox(Game1.PixelVector(80, 50), Game1.PixelVector(115, 70), shortButtonSprites, buttonFont, color, Keys.A));
            Keyboxes.Add("left2", new KeyBox(Game1.PixelVector(80, 120), Game1.PixelVector(115, 140), shortButtonSprites, buttonFont, color, Keys.Left));
            Keyboxes.Add("down1", new KeyBox(Game1.PixelVector(115, 50), Game1.PixelVector(150, 70), shortButtonSprites, buttonFont, color, Keys.S));
            Keyboxes.Add("down2", new KeyBox(Game1.PixelVector(115, 120), Game1.PixelVector(150, 140), shortButtonSprites, buttonFont, color, Keys.Down));
            Keyboxes.Add("right1", new KeyBox(Game1.PixelVector(150, 50), Game1.PixelVector(185, 70), shortButtonSprites, buttonFont, color, Keys.D));
            Keyboxes.Add("right2", new KeyBox(Game1.PixelVector(150, 120), Game1.PixelVector(185, 140), shortButtonSprites, buttonFont, color, Keys.Right));
            Keyboxes.Add("jump1", new KeyBox(Game1.PixelVector(250, 10), Game1.PixelVector(285, 30), shortButtonSprites, buttonFont, color, Keys.Space));
            Keyboxes.Add("jump2", new KeyBox(Game1.PixelVector(250, 80), Game1.PixelVector(285, 100), shortButtonSprites, buttonFont, color, Keys.RightControl));
            Keyboxes.Add("attack1", new KeyBox(Game1.PixelVector(250, 50), Game1.PixelVector(285, 70), shortButtonSprites, buttonFont, color, Keys.E));
            Keyboxes.Add("attack2", new KeyBox(Game1.PixelVector(250, 120), Game1.PixelVector(285, 140), shortButtonSprites, buttonFont, color, Keys.RightShift));
            Keyboxes.Add("dash1", new KeyBox(Game1.PixelVector(250, 30), Game1.PixelVector(285, 50), shortButtonSprites, buttonFont, color, Keys.LeftShift));
            Keyboxes.Add("dash2", new KeyBox(Game1.PixelVector(250, 100), Game1.PixelVector(285, 120), shortButtonSprites, buttonFont, color, Keys.NumPad0));

            fpsToggle = new ToggleButton(Game1.PixelVector(10, 120), Game1.PixelVector(45, 140), shortButtonSprites, true);
            fpsToggle.DefineText("FPS on", "FPS off", buttonFont, 5, Color.Black);
        }

        public  override ScreenManager.GameState Update(GameTime gameTime, KeyboardState keyboard, MouseState mouse)
        {
            backButton.Update(mouse);

            //sound
            masterVolumeBox.Update(mouse);
            effectsVolumeBox.Update(mouse);
            musicVolumeBox.Update(mouse);
            SoundEffect.MasterVolume = (float)masterVolumeBox.GetValue() / 10;
            Game1.self.effectsVolume = (float)effectsVolumeBox.GetValue() / 10;
            Game1.self.musicVolume = (float)musicVolumeBox.GetValue() / 10;

            Game1.self.Sounds["The_Lobby_Music"].Volume = 1f * Game1.self.musicVolume;

            //controls
            foreach (KeyBox keyBox in Keyboxes.Values)
            {
                keyBox.Update(mouse, keyboard);
            }

            fpsToggle.Update(mouse);           

            if (backButton.IsPressed()) 
            {               
                //return
                return ScreenManager.GameState.MainMenu;
            }
            

            return ScreenManager.GameState.Null;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Color color = Color.White;
            spriteBatch.DrawString(spriteFont, "Master volume:", new Vector2(20, 150), Color.White);
            masterVolumeBox.Draw(spriteBatch);

            spriteBatch.DrawString(spriteFont, "SFX volume:", new Vector2(20, 250), Color.White);
            effectsVolumeBox.Draw(spriteBatch);

            spriteBatch.DrawString(spriteFont, "Music volume:", new Vector2(20, 350), Color.White);
            musicVolumeBox.Draw(spriteBatch);

            backButton.Draw(spriteBatch);

            foreach (KeyBox keyBox in Keyboxes.Values)
            {
                keyBox.Draw(spriteBatch);
            }

            fpsToggle.Draw(spriteBatch);

            Game1.DrawStringIn(new Vector2(500, 90), new Vector2(1100, 190), spriteBatch, buttonFont, "Player 1:", color);
            Game1.DrawStringIn(new Vector2(500, 500), new Vector2(1100, 600), spriteBatch, buttonFont, "Player 2:", color);

            Game1.DrawStringIn(new Vector2(1300, 100), new Vector2(1500, 200), spriteBatch, buttonFont, "Jump", color);
            Game1.DrawStringIn(new Vector2(1300, 200), new Vector2(1500, 300), spriteBatch, buttonFont, "Dash", color);
            Game1.DrawStringIn(new Vector2(1300, 300), new Vector2(1500, 400), spriteBatch, buttonFont, "Attack", color);

            Game1.DrawStringIn(new Vector2(1300, 500), new Vector2(1500, 600), spriteBatch, buttonFont, "Jump", color);
            Game1.DrawStringIn(new Vector2(1300, 600), new Vector2(1500, 700), spriteBatch, buttonFont, "Dash", color);
            Game1.DrawStringIn(new Vector2(1300, 700), new Vector2(1500, 800), spriteBatch, buttonFont, "Attack", color);
        }


        public override void ChangeTo()
        {
            Game1.self.IsMouseVisible = true;

        }
    }
}
