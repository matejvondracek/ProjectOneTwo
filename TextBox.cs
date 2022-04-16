using System;
using System.Collections.Generic;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectOneTwo
{
    public class TextBox
    {
        protected Button button;
        readonly protected SpriteFont font;
        bool selected = false, released = false;
        string text = "";
        Color textColor;
        public TextBox(Vector2 a, Vector2 b, Texture2D[] _textures, SpriteFont _font, Color color)
        {
            button = new Button(a, b, _textures);
            font = _font;
            textColor = color;
        }

        public void Update(MouseState mouse, KeyboardState keyboard)
        {
            button.Update(mouse);
            if (button.IsPressed()) selected = true;
            else if ((mouse.LeftButton == ButtonState.Pressed) && (!button.IsTargeted())) selected = false;
            if (selected) HandleInput(keyboard);           
        }

        public void Draw(SpriteBatch sprite)
        {
            button.Draw(sprite);
        }

        protected virtual void HandleInput(KeyboardState keyboard)
        {
            if ((keyboard.GetPressedKeyCount() > 0) && released)
            {
                released = false;
                List<Keys> keys = new List<Keys>(keyboard.GetPressedKeys());
                if (keys.Contains(Keys.Back) && text.Length > 0) text = text.Remove(text.Length - 1, 1);               
                else
                {
                    for (int i = 0; i <= keys.Count - 1; i++)
                    {
                        Keys key = keys[i];
                        //capital letters
                        if ((key >= Keys.A) && (key <= Keys.Z))
                        {
                            //if (!keys.Contains(Keys.LeftShift) && !keys.Contains(Keys.RightShift)) key -= 32;
                            text += key;
                        }
                        if ((key >= Keys.NumPad0) && (key <= Keys.NumPad9))
                        {
                            text += Convert.ToString(((int)key) - 96);
                        }

                        switch (key)
                        {
                            case Keys.Enter:
                                selected = false;
                                break;
                            case Keys.OemPeriod:
                                text += '.';
                                break;
                            case Keys.OemComma:
                                text += ',';
                                break;
                            case Keys.OemMinus:
                                text += '-';
                                break;
                            case Keys.OemPlus:
                                text += '+';
                                break;
                            case Keys.OemSemicolon:
                                text += ';';
                                break;
                        }
                    }                  
                    
                }
            }
            else if (keyboard.GetPressedKeyCount() == 0)
            {
                released = true;
            }
            button.AddText(text, font, 20, 20, textColor);
        }
    }
}
