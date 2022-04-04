using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectOneTwo
{   
    public class Button
    {
        Rectangle rect;
        readonly Texture2D[] textures;
        Texture2D texture;
        string text;
        SpriteFont spriteFont;
        bool withText = false;
        Vector2 textSize, textPosition;
        float textScale;


        public Button(Rectangle _rect, Texture2D[] _textures)
        {
            rect = _rect;
            textures = _textures;
        }

        public Button(Vector2 a, Vector2 b, Texture2D[] _textures)
        {
            rect = new Rectangle(a.ToPoint(), new Point(Convert.ToInt32(b.X - a.X), Convert.ToInt32(b.Y - a.Y)));
            textures = _textures;
        }

        public void AddText(string _text, SpriteFont _spriteFont, int xBezel, int yBezel)
        {
            withText = true;
            text = _text;
            spriteFont = _spriteFont;

            textSize = spriteFont.MeasureString(text);
            Rectangle bounds = new Rectangle(rect.X + xBezel, rect.Y + yBezel, rect.Width - 2 * xBezel, rect.Height - 2 * yBezel);
            float xScale = (bounds.Width / textSize.X);
            float yScale = (bounds.Height / textSize.Y);
            textScale = Math.Min(xScale, yScale);

            int textWidth = (int)Math.Round(textSize.X * textScale);
            int textHeight = (int)Math.Round(textSize.Y * textScale);
            textPosition.X = (((bounds.Width - textWidth) / 2) + bounds.X);
            textPosition.Y = (((bounds.Height - textHeight) / 2) + bounds.Y);
        }

        public void Update(MouseState mouse)
        {
            if (rect.Contains(mouse.Position)) texture = textures[1];
            else texture = textures[0];
        }

        public bool IsPressed(MouseState mouse)
        {
            if (rect.Contains(mouse.Position) && mouse.LeftButton == ButtonState.Pressed) return true;           
            else return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rect, Color.White);
            if (withText) 
                spriteBatch.DrawString(spriteFont, text, textPosition, Color.White, 0.0f, new Vector2(), textScale, new SpriteEffects(), 0.0f);
        }
    }
}
