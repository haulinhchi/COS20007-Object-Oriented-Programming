using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject
{
    public abstract class GameObject
    {
        public Bitmap _ObjectBitmap { get; set; }
        public DrawingOptions drawingOptions = new DrawingOptions();
        protected MazeGame _game;
        protected Window _gameWindow;
        protected Maze _maze;
        public double X { get; set; }
        public double Y { get; set; }
        public int speed = 3;
        public float scale;

        public int Width
        {
            get
            {
                return _ObjectBitmap.Width;
            }
        }

        public int Height
        {
            get
            {
                return _ObjectBitmap.Height;
            }
        }

        public GameObject(MazeGame game, Window gameWindow, Maze maze, Bitmap bitmap)
        {
            _game = game;
            scale = (gameWindow.Height / ((float)maze._mazeSize * bitmap.Width));
            drawingOptions = new DrawingOptions() { ScaleX = scale, ScaleY = scale };
            _ObjectBitmap = bitmap;
            _maze = maze;
            _gameWindow = gameWindow;
        }

        public virtual void Draw()
        {
            _gameWindow.DrawBitmap(_ObjectBitmap, X - _ObjectBitmap.Width / 2, Y - _ObjectBitmap.Height / 2, drawingOptions);
        }

        public abstract void Update(Cat cat);
        public bool CollisionTest(GameObject gameobject)
        {
            if (Math.Abs(this.X - gameobject.X) > _maze.wallWidth / 2 || Math.Abs(this.Y - gameobject.Y) > _maze.wallWidth / 2)
                return false;
            else
                return true;
        }

        public abstract void Collided();
    }
}
