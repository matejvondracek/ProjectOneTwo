using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace ProjectOneTwo
{
    public class ScreenManager
    {
        readonly public Dictionary<GameState, Screen> Screens = new Dictionary<GameState, Screen>();
        SpriteFont spriteFont;
        readonly SimpleFps fps = new SimpleFps();

        public enum GameState
        {
            MainMenu,
            Settings,
            Credits,
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
            Screens.Add(GameState.MainMenu, new Screen_MainMenu());
            Screens.Add(GameState.GamePlay, new Screen_GamePlay());
            Screens.Add(GameState.GameOver, new Screen_GameOver());
            Screens.Add(GameState.Settings, new Screen_Settings());
            Screens.Add(GameState.Credits, new Screen_Credits());
            gameState = _gameState;
            
            winner = Winner.None;
        }
        public void Initialize()
        {
            foreach (Screen screen in Screens.Values)
            {
                screen.Initialize();
            }
        }

        public void LoadContent()
        {
            foreach (Screen screen in Screens.Values)
            {
                screen.LoadContent();
            }

            spriteFont = Game1.Mycontent.Load<SpriteFont>("aApiNyala50");

            Screens[gameState].ChangeTo();
        }

        public void Update(GameTime gameTime, KeyboardState keyboard, MouseState mouse)
        {
            GameState newState = Screens[gameState].Update(gameTime, keyboard, mouse);
            if ((newState != gameState) && (newState != GameState.Null))
            {
                gameState = newState;
                Screens[gameState].ChangeTo();
            }

            fps.Update(gameTime);

            if (keyboard.IsKeyDown(Keys.Escape))
                Game1.self.Exit();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Screens[gameState].Draw(gameTime, spriteBatch);
            Screen_Settings settings = (Screen_Settings)GetScreen(GameState.Settings);
            if (settings.fpsToggle.on)
            {
                fps.DrawFps(spriteBatch, spriteFont, new Vector2(10f, 10f), Color.GreenYellow);
            }            
        }

        public Screen GetScreen(GameState state)
        {
            return Screens[state];
        }
    }
}
