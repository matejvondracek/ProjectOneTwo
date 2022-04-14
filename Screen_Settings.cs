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
        SpriteFont spriteFont;
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
            spriteFont = Game1.Mycontent.Load<SpriteFont>("font");

            backButton = new Button(new Vector2(20, 900), new Vector2(420, 1000), buttonSprites);
            backButton.AddText("Back", spriteFont, 30, 0);

            masterVolumeBox = new ValueBox(0, 10, 0, spriteFont, new Vector2(20, 200), new Vector2(350, 250), buttonSprites);
            masterVolumeBox.SetValue(10);
            effectsVolumeBox = new ValueBox(0, 10, 0, spriteFont, new Vector2(20, 300), new Vector2(350, 350), buttonSprites);
            effectsVolumeBox.SetValue(10);
            musicVolumeBox = new ValueBox(0, 10, 0, spriteFont, new Vector2(20, 400), new Vector2(350, 450), buttonSprites);
            musicVolumeBox.SetValue(10);
        }

        public  override ScreenManager.GameState Update(GameTime gameTime, KeyboardState keyboard, MouseState mouse)
        {
            backButton.Update(mouse);
            masterVolumeBox.Update(mouse);
            effectsVolumeBox.Update(mouse);
            musicVolumeBox.Update(mouse);

            SoundEffect.MasterVolume = (float)masterVolumeBox.GetValue() / 10;
            Game1.self.effectsVolume = (float)effectsVolumeBox.GetValue() / 10;
            Game1.self.musicVolume = (float)musicVolumeBox.GetValue() / 10;

            if (backButton.IsPressed()) return ScreenManager.GameState.MainMenu;

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
            
        }


        public override void ChangeTo()
        {
            Game1.self.IsMouseVisible = true;

        }
    }
}
