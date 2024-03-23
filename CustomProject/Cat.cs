using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject
{
    public class Cat : GameObject
    {
        private Bitmap _FrontBitmap;

        private Bitmap _RearBitmap;

        private Bitmap _LeftBitmap;

        private Bitmap _RightBitmap;

        public List<Projectile> _projectiles = new List<Projectile>();

        public bool Quit { get; set; }

        public Cat(MazeGame game, Window gameWindow, Maze maze) : base(game, gameWindow, maze, new Bitmap("Front", "front.png"))
        {
            _RearBitmap = new Bitmap("Rear", "rear.png");
            _FrontBitmap = new Bitmap("Front", "front.png");
            _LeftBitmap = new Bitmap("Left", "left.png");
            _RightBitmap = new Bitmap("Right", "right.png");
            _maze = maze;
            X = _maze.wallWidth + 15;
            Y = 20;
            Quit = false;
        }

        public void HandleInput()
        {
            SplashKit.ProcessEvents();

            if (SplashKit.KeyDown(KeyCode.WKey) || SplashKit.KeyDown(KeyCode.UpKey))
            {
                if (!_maze.CheckCollision(this.X, this.Top, speed, Maze.Directions.Top))
                {
                    Y -= speed;
                }
                _ObjectBitmap = _RearBitmap;
            }

            if (SplashKit.KeyDown(KeyCode.SKey) || SplashKit.KeyDown(KeyCode.DownKey))
            {
                if (!_maze.CheckCollision(this.X, this.Bottom, speed, Maze.Directions.Down))
                {
                    Y += speed;
                }
                _ObjectBitmap = _FrontBitmap;
            }

            if (SplashKit.KeyDown(KeyCode.AKey) || SplashKit.KeyDown(KeyCode.LeftKey))
            {
                if (!_maze.CheckCollision(this.Left, this.Y, speed, Maze.Directions.Left))
                {
                    X -= speed;
                }
                _ObjectBitmap = _LeftBitmap;
            }

            if (SplashKit.KeyDown(KeyCode.DKey) || SplashKit.KeyDown(KeyCode.RightKey))
            {
                if (!_maze.CheckCollision(this.Right, this.Y, speed, Maze.Directions.Right))
                {
                    X += speed;
                } 
                _ObjectBitmap = _RightBitmap;
            }

            if (SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                _projectiles.Add(new Projectile(_game, _gameWindow, _maze, new Point2D() { X = X, Y = Y }, SplashKit.MousePosition()));
            }
            if (SplashKit.KeyTyped(KeyCode.EscapeKey))
            {
                Quit = true;
            }
        }

        public override void Draw()
        {
            base.Draw();
        }

        public override void Update(Cat cat)
        {
            throw new NotImplementedException();
        }

        public override void Collided()
        {
            throw new NotImplementedException();
        }

        public double Top
        {
            get { return this.Y - this.Height * this.scale / 2; }
        }

        public double Right
        {
            get { return this.X + this.Width * this.scale / 2; }
        }

        public double Bottom
        {
            get { return this.Y + this.Height * this.scale / 2; }
        }

        public double Left
        {
            get { return this.X - this.Width * this.scale / 2; }
        }
    }
}