using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject
{
    public class Maze
    {
        public enum Directions
        {
            Top,
            Down,
            Left,
            Right,
            None
        };

        public bool[,] mazeMatrix;
        Window _gameWindow;
        public int _mazeSize;
        public double wallWidth;
        public Point2D startPoint;
        public Point2D endPoint;
        public Maze(Window window, int size)
        {
            _gameWindow = window;
            _mazeSize = size;
            wallWidth = _gameWindow.Height / (double)size;
            mazeMatrix = new bool[size, size];
            InitialiseMaze();
            CreateMazePath(1, 1);
            mazeMatrix[1, 0] = false;
            mazeMatrix[_mazeSize - 2, _mazeSize - 1] = false;
            startPoint = new Point2D() { X = 1 * wallWidth, Y = 1 * wallWidth };
            endPoint = new Point2D() { X = (_mazeSize - 2) * wallWidth, Y = (_mazeSize - 1) * wallWidth };
        }

        private void InitialiseMaze()
        {
            for (int i = 0; i < _mazeSize; i++)
            {
                for (int j = 0; j < _mazeSize; j++)
                {
                    mazeMatrix[i, j] = true;
                }
            }
        }

        private void CreateMazePath(int row, int col)
        {
            Directions[] directionSequence = { Directions.Top, Directions.Down, Directions.Left, Directions.Right };
            Random random = new Random();
            directionSequence = directionSequence.OrderBy(x => random.Next()).ToArray();

            foreach (var direction in directionSequence)
            {
                switch (direction)
                {
                    case Directions.Top:
                        if (row - 2 <= 0)
                            continue;

                        if (mazeMatrix[row - 2, col] != false)
                        {
                            mazeMatrix[row - 2, col] = false;
                            mazeMatrix[row - 1, col] = false;
                            CreateMazePath(row - 2, col);
                        }
                        break;

                    case Directions.Down:
                        if (row + 2 >= _mazeSize - 1)
                            continue;

                        if (mazeMatrix[row + 2, col] != false)
                        {
                            mazeMatrix[row + 2, col] = false;
                            mazeMatrix[row + 1, col] = false;
                            CreateMazePath(row + 2, col);
                        }
                        break;

                    case Directions.Left:
                        if (col - 2 <= 0)
                            continue;

                        if (mazeMatrix[row, col - 2] != false)
                        {
                            mazeMatrix[row, col - 2] = false;
                            mazeMatrix[row, col - 1] = false;
                            CreateMazePath(row, col - 2);
                        }
                        break;

                    case Directions.Right:
                        if (col + 2 >= _mazeSize - 1)
                            continue;

                        if (mazeMatrix[row, col + 2] != false)
                        {
                            mazeMatrix[row, col + 2] = false;
                            mazeMatrix[row, col + 1] = false;
                            CreateMazePath(row, col + 2);
                        }
                        break;
                }
            }
        }

        public void Draw()
        {
            for (int i = 0; i < _mazeSize; i++)
            {
                for (int j = 0; j < _mazeSize; j++)
                {
                    if (mazeMatrix[i, j] == true)
                    {
                        _gameWindow.FillRectangle(Color.Wheat, i * wallWidth, j * wallWidth, wallWidth * 1.1, wallWidth * 1.1);
                    }
                    else
                    {
                        _gameWindow.FillRectangle(Color.LightCoral, i * wallWidth, j * wallWidth, wallWidth * 1.1, wallWidth * 1.1);
                    }
                }
            }
        }

        // Function to check collisions
        public bool CheckCollision(GameObject gameobject, Directions d)
        {
            int checkx = 0, checky = 0;
            switch (d)
            {
                case Directions.Down:
                    checkx = (int)(gameobject.X / wallWidth);
                    checky = (int)((gameobject.Y + gameobject.speed) / wallWidth);
                    break;
                case Directions.Top:
                    checkx = (int)(gameobject.X / wallWidth);
                    checky = (int)((gameobject.Y - gameobject.speed) / wallWidth);
                    break;
                case Directions.Right:
                    checkx = (int)((gameobject.X + gameobject.speed) / wallWidth);
                    checky = (int)(gameobject.Y / wallWidth);
                    break;
                case Directions.Left:
                    checkx = (int)((gameobject.X - gameobject.speed) / wallWidth);
                    checky = (int)(gameobject.Y / wallWidth);
                    break;
                case Directions.None:
                    checkx = (int)(gameobject.X / wallWidth);
                    checky = (int)(gameobject.Y / wallWidth);
                    break;
            }
            if (checkx >= 0 && checkx < _mazeSize && checky >= 0 && checky < _mazeSize)
            {
                if (mazeMatrix[checkx, checky])
                {
                    return true;
                }
                return false;
            }
            return true;
        }

        public bool CheckCollision(double X, double Y, double speed, Directions d)
        {
            int checkx = 0, checky = 0;
            switch (d)
            {
                case Directions.Down:
                    checkx = (int)(X / wallWidth);
                    checky = (int)((Y + speed) / wallWidth);
                    break;
                case Directions.Top:
                    checkx = (int)(X / wallWidth);
                    checky = (int)((Y - speed) / wallWidth);
                    break;
                case Directions.Right:
                    checkx = (int)((X + speed) / wallWidth);
                    checky = (int)(Y / wallWidth);
                    break;
                case Directions.Left:
                    checkx = (int)((X - speed) / wallWidth);
                    checky = (int)(Y / wallWidth);
                    break;
                case Directions.None:
                    checkx = (int)(X / wallWidth);
                    checky = (int)(Y / wallWidth);
                    break;
            }
            if (checkx >= 0 && checkx < _mazeSize && checky >= 0 && checky < _mazeSize)
            {
                if (mazeMatrix[checkx, checky])
                {
                    return true;
                }
                return false;
            }
            return true;
        }
    }
}