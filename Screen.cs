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
        //Physics physics; not static
        public List<Button> buttons = new List<Button>();
        List<RenderTexture2D> textures = new List<RenderTexture2D>();
        List<RenderString> strings = new List<RenderString>();
        //RenderString2D[] strings; new type
        
        public Screen()
        {

        }

        #region main loop
        public void Initialize()
        {

        }

        public void LoadContent()
        {

        }

        public void Update(GameTime gameTime, MouseState mouse, KeyboardState keyboard)
        {
            for (int i = 0; i <= buttons.Count - 1; i++)
            {
                buttons[i].Update(mouse);
            }
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
        #endregion
    }
}
