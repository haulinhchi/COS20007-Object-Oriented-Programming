using System;
using SplashKitSDK;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CustomProject
{
    public class Program
    {
        public enum GameState
        {
            Welcome,
            MazeGame,
            Instruction,
            EndScreen
        }
        public static void Main()
        {
            Window window = new Window("Pawesome Meow Maze", 750, 750);
            GameState currentState = GameState.Welcome;
            MazeGame mazegame = null;

            do
            {
                SplashKit.ProcessEvents();

                double X_mouse = SplashKit.MousePosition().X;
                double Y_mouse = SplashKit.MousePosition().Y;


                // Handle different game states
                switch (currentState)
                {
                    case GameState.Welcome:
                        Welcome welcome = new Welcome(window);

                        if ((X_mouse >= 109) && (X_mouse <= 641) && (Y_mouse >= 404) && (Y_mouse <= 505) && SplashKit.MouseClicked(MouseButton.LeftButton))
                        {
                            currentState = GameState.MazeGame;
                        }

                        if ((X_mouse >= 109) && (X_mouse <= 641) && (Y_mouse >= 537) && (Y_mouse <= 640) && SplashKit.MouseClicked(MouseButton.LeftButton))
                        {
                            currentState = GameState.Instruction;
                        }
                        break;

                    case GameState.MazeGame:
                        mazegame = new MazeGame(window);

                        while (!(window.CloseRequested || mazegame.Quit))
                        {
                            mazegame.Draw();
                            mazegame.HandleInput();
                            mazegame.Update();
                        }

                        // Check if the maze game is won or _cat.Quit is true
                        if (mazegame.CheckPosition(mazegame._cat) || mazegame.Quit)
                        {
                            currentState = GameState.EndScreen;
                        }

                        break;

                    case GameState.Instruction:
                        Instruction instruction = new Instruction(window);
                        currentState = GameState.Instruction;

                        if ((X_mouse >= 226) && (X_mouse <= 524) && (Y_mouse >= 596) && (Y_mouse <= 675) && SplashKit.MouseClicked(MouseButton.LeftButton))
                        {
                            currentState = GameState.MazeGame;
                        }
                        break;


                    case GameState.EndScreen:
                        EndScreen endScreen = new EndScreen(window, mazegame.EndScreenInstance._endText);
                        currentState = GameState.EndScreen;

                        if ((X_mouse >= 152) && (X_mouse <= 598) && (Y_mouse >= 501) && (Y_mouse <= 604) && SplashKit.MouseClicked(MouseButton.LeftButton))
                        {
                            currentState = GameState.MazeGame;
                        }
                        break;
                }

                SplashKit.Delay(10);
                SplashKit.RefreshScreen();
                SplashKit.ClearScreen();

            } while (!SplashKit.WindowCloseRequested("Pawesome Meow Maze"));
        }
    }
}