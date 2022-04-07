using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using ProjectOneTwo;

namespace ProjectOneTwo
{
    static class ScreenManager
    {
        static List<Screen> screens = new List<Screen>();
        static List<Game1.GameState> gameStates = new List<Game1.GameState>();
        static int currentState;
        public static Screen screen;

        public static void AddScreen(Game1.GameState gameState)
        {
            screens.Add(new Screen());
            gameStates.Add(gameState);
        }

        public static void ChangeGameStateTo(Game1.GameState gameState)
        {
            if (gameStates.Contains(gameState))
            {
                for (int i = 0; i <= gameStates.Count - 1; i++)
                {
                    if (gameStates[i] == gameState)
                    {
                        currentState = i;
                        break;
                    }
                }
            }
            else throw new Exception("screen manager does not contain desired screen!");

            screen = screens[currentState]; ///should be ref but it's fine for now i guess
        }

        public static Game1.GameState GetGameState()
        {
            return gameStates[currentState];
        }
    }
}
