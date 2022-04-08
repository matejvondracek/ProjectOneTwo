﻿using Microsoft.Xna.Framework;
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
        Dictionary<GameState, Screen> screens = new Dictionary<GameState, Screen>();

        public enum GameState
        {
            MainMenu,
            GamePlay,
            GameOver,
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
        }

        public void Update(GameTime gameTime, KeyboardState keyboard, MouseState mouse)
        {
            if (screens[gameState].Update(gameTime, keyboard, mouse))
            {
                screens[gameState].ChangeTo();
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            screens[gameState].Draw(gameTime, spriteBatch);
        }
    }
}
