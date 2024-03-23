using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject
{
    public class Projectile : GameObject
    {
        private Vector2D Velocity { get; set; }

        public Projectile(MazeGame game, Window gameWindow, Maze maze, Point2D catPosition, Point2D mousePosition) : base(game, gameWindow, maze, new Bitmap("Projectile", "projectile.png"))
         {
            _maze = maze;
            _gameWindow = gameWindow;

            float scale = (gameWindow.Height / ((float)maze._mazeSize * _ObjectBitmap.Width));

            X = catPosition.X;
            Y = catPosition.Y;
            Point2D fromPt = catPosition;
            Point2D toPt = mousePosition;
            Vector2D dir;
            dir = SplashKit.UnitVector(SplashKit.VectorPointToPoint(fromPt, toPt));
            Velocity = SplashKit.VectorMultiply(dir, speed);
            drawingOptions = new DrawingOptions() { ScaleX = scale, ScaleY = scale };
        }

        public override void Update(Cat _cat)
        {
            X += Velocity.X;
            Y += Velocity.Y;
        }

        public override void Collided()
        {
            throw new NotImplementedException();
        }
    }
}
