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
    public class ScreenManager
    {
        readonly public Dictionary<GameState, Screen> screens = new Dictionary<GameState, Screen>();
        SpriteFont spriteFont;
        readonly SimpleFps fps = new SimpleFps();

        public enum GameState
        {
            MainMenu,
            GamePlay,
            GameOver,
            Null,
        }

        public GameState gameState;

        public enum Winner
        {
            Player1,
            Player2,
            Draw,
            None,
        }

        public Winner winner;

        public ScreenManager(GameState _gameState)
        {
            screens.Add(GameState.MainMenu, new Screen_MainMenu());
            screens.Add(GameState.GamePlay, new Screen_GamePlay());
            screens.Add(GameState.GameOver, new Screen_GameOver());
            gameState = _gameState;
            screens[gameState].ChangeTo();
            winner = Winner.None;
        }
        public void Initialize()
        {
            foreach (Screen screen in screens.Values)
            {
                screen.Initialize();
            }
        }

        public void LoadContent()
        {
            foreach (Screen screen in screens.Values)
            {
                screen.LoadContent();
            }

            spriteFont = Game1.Mycontent.Load<SpriteFont>("font");
        }

        public void Update(GameTime gameTime, KeyboardState keyboard, MouseState mouse)
        {
            GameState newState = screens[gameState].Update(gameTime, keyboard, mouse);
            if ((newState != gameState) && (newState != GameState.Null))
            {
                gameState = newState;
                screens[gameState].ChangeTo();
            }

            fps.Update(gameTime);

            if (keyboard.IsKeyDown(Keys.Escape))
                Game1.self.Exit();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            screens[gameState].Draw(gameTime, spriteBatch);
            fps.DrawFps(spriteBatch, spriteFont, new Vector2(10f, 10f), Color.GreenYellow);
        }

        public Screen GetScreen(GameState state)
        {
            return screens[state];
        }
    }
}
