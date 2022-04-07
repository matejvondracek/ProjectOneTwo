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
    class Screen
    {
        Physics physics;
        public List<Button> buttons = new List<Button>();
        List<RenderTexture2D> textures = new List<RenderTexture2D>();
        List<RenderString> strings = new List<RenderString>();
        
        public Screen()
        {
            physics = new Physics();
        }

        #region main loop
        public void Initialize()
        {

        }

        public void LoadContent()
        {

        }

        public Game1.Winner Update(GameTime gameTime, MouseState mouse, KeyboardState keyboard)
        {
            for (int i = 0; i <= buttons.Count - 1; i++)
            {
                buttons[i].Update(mouse);
            }

            physics.AttacksUpdate();
            physics.MoveUpdate();

            return physics.GameRules();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 0; i <= textures.Count - 1; i++)
            {
                spriteBatch.Draw(textures[i].texture, textures[i].rectangle, Color.White);
            }
            
            for (int i = 0; i <= buttons.Count - 1; i++)
            {
                buttons[i].Draw(spriteBatch);
            }

            for (int i = 0; i <= strings.Count - 1; i++)
            {
                spriteBatch.DrawString(strings[i].font, strings[i].str, strings[i].pos, strings[i].color);
            }

            physics.Draw(spriteBatch);
        }
        #endregion 


        #region adding functions
        public void AddButton(ref Button button)
        {
            buttons.Add(button);
        }

        public void AddRenderTexture2D(Texture2D texture, Rectangle rectangle)
        {
            RenderTexture2D renderTexture2D = new RenderTexture2D(texture, rectangle);
            textures.Add(renderTexture2D);
        }

        public void AddRenderString(ref string str, SpriteFont font, Vector2 pos, Color color)
        {
            RenderString renderString = new RenderString(ref str, font, pos, color);
            strings.Add(renderString);
        }

        public void AddEntity(ref Player1 player)
        {
            physics.AddEntity(ref player);
        }

        public void LoadMap()
        {
            physics.LoadMap();
        }
        #endregion
    }
}
