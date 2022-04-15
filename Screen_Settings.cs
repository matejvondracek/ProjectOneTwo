using Microsoft.Xna.Framework;
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
        public KeyBox up1, up2, left1, left2, down1, down2, right1, right2, jump1, jump2;
        SpriteFont spriteFont, buttonFont;
        readonly Texture2D[] buttonSprites = new Texture2D[2];

        public Screen_Settings()
        {

        }

        public override void Initialize()
        {
        }

        public override void LoadContent()
        {
            buttonSprites[0] = Game1.Mycontent.Load<Texture2D>("button1");
            buttonSprites[1] = Game1.Mycontent.Load<Texture2D>("button2");
            spriteFont = Game1.Mycontent.Load<SpriteFont>("aApiNyala30");
            buttonFont = Game1.Mycontent.Load<SpriteFont>("aApiNyala200");

            backButton = new Button(new Vector2(20, 900), new Vector2(420, 1000), buttonSprites);
            backButton.AddText("Back", buttonFont, 30, 0);

            masterVolumeBox = new ValueBox(0, 10, 2, buttonFont, new Vector2(20, 200), new Vector2(350, 250), buttonSprites);
            masterVolumeBox.SetValue(10);
            effectsVolumeBox = new ValueBox(0, 10, 2, buttonFont, new Vector2(20, 300), new Vector2(350, 350), buttonSprites);
            effectsVolumeBox.SetValue(10);
            musicVolumeBox = new ValueBox(0, 10, 2, buttonFont, new Vector2(20, 400), new Vector2(350, 450), buttonSprites);
            musicVolumeBox.SetValue(10);

            up1 = new KeyBox(new Vector2(700, 200), new Vector2(900, 300), buttonSprites, buttonFont, Keys.W);
            up2 = new KeyBox(new Vector2(700, 600), new Vector2(900, 700), buttonSprites, buttonFont, Keys.Up);
            left1 = new KeyBox(new Vector2(500, 300), new Vector2(700, 400), buttonSprites, buttonFont, Keys.A);
            left2 = new KeyBox(new Vector2(500, 700), new Vector2(700, 800), buttonSprites, buttonFont, Keys.Left);
            down1 = new KeyBox(new Vector2(700, 300), new Vector2(900, 400), buttonSprites, buttonFont, Keys.S);
            down2 = new KeyBox(new Vector2(700, 700), new Vector2(900, 800), buttonSprites, buttonFont, Keys.Down);
            right1 = new KeyBox(new Vector2(900, 300), new Vector2(1100, 400), buttonSprites, buttonFont, Keys.D);
            right2 = new KeyBox(new Vector2(900, 700), new Vector2(1100, 800), buttonSprites, buttonFont, Keys.Right);
            jump1 = new KeyBox(new Vector2(1200, 300), new Vector2(1400, 400), buttonSprites, buttonFont, Keys.Space);
            jump2 = new KeyBox(new Vector2(1200, 700), new Vector2(1400, 800), buttonSprites, buttonFont, Keys.RightControl);
        }

        public  override ScreenManager.GameState Update(GameTime gameTime, KeyboardState keyboard, MouseState mouse)
        {
            backButton.Update(mouse);
            masterVolumeBox.Update(mouse);
            effectsVolumeBox.Update(mouse);
            musicVolumeBox.Update(mouse);

            //controls
            up1.Update(mouse, keyboard);
            up2.Update(mouse, keyboard);
            left1.Update(mouse, keyboard);
            left2.Update(mouse, keyboard);
            down1.Update(mouse, keyboard);
            down2.Update(mouse, keyboard);
            right1.Update(mouse, keyboard);
            right2.Update(mouse, keyboard);
            jump1.Update(mouse, keyboard);
            jump2.Update(mouse, keyboard);

            SoundEffect.MasterVolume = (float)masterVolumeBox.GetValue() / 10;
            Game1.self.effectsVolume = (float)effectsVolumeBox.GetValue() / 10;
            Game1.self.musicVolume = (float)musicVolumeBox.GetValue() / 10;

            if (backButton.IsPressed()) 
            {               
                //return
                return ScreenManager.GameState.MainMenu;
            }
            

            return ScreenManager.GameState.Null;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(spriteFont, "Master volume:", new Vector2(20, 150), Color.White);
            masterVolumeBox.Draw(spriteBatch);

            spriteBatch.DrawString(spriteFont, "SFX volume:", new Vector2(20, 250), Color.White);
            effectsVolumeBox.Draw(spriteBatch);

            spriteBatch.DrawString(spriteFont, "Music volume:", new Vector2(20, 350), Color.White);
            musicVolumeBox.Draw(spriteBatch);

            backButton.Draw(spriteBatch);

            up1.Draw(spriteBatch);
            up2.Draw(spriteBatch);
            left1.Draw(spriteBatch);
            left2.Draw(spriteBatch);
            down1.Draw(spriteBatch);
            down2.Draw(spriteBatch);
            right1.Draw(spriteBatch);
            right2.Draw(spriteBatch);
            jump1.Draw(spriteBatch);
            jump2.Draw(spriteBatch);

            spriteBatch.DrawString(spriteFont, "Player 1:", new Vector2(500, 200), Color.White);
            spriteBatch.DrawString(spriteFont, "Player 2:", new Vector2(500, 600), Color.White);
        }


        public override void ChangeTo()
        {
            Game1.self.IsMouseVisible = true;

        }
    }
}
