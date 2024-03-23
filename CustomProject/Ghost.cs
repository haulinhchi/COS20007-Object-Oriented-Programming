using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject
{
    public class Ghost : GameObject
    {
        public Ghost(MazeGame game, Window gameWindow, Maze maze) : base(game, gameWindow, maze, new Bitmap("Front", "front.png"))
        {
            _ObjectBitmap = new Bitmap("Ghost", "ghost.png");
            _maze = maze;

            float scale = (gameWindow.Height / ((float)maze._mazeSize * _ObjectBitmap.Width));

            do
            {
                X = (int)(SplashKit.Rnd() * maze._mazeSize);
                Y = (int)(SplashKit.Rnd() * maze._mazeSize);
            } while (maze.mazeMatrix[(int)X, (int)Y] || X < 0.2 * maze._mazeSize || Y < 0.2 * maze._mazeSize);
            X *= maze.wallWidth;
            Y *= maze.wallWidth;
            drawingOptions = new DrawingOptions() { ScaleX = scale, ScaleY = scale };
        }

        public override void Update(Cat _cat)
        {
            double proximityValue = _maze.wallWidth * 2;

            if ((Math.Abs(_cat.X - this.X) <= proximityValue && Math.Abs(_cat.Y - this.Y) <= proximityValue)
                || (Math.Abs(_cat.X + this.X) <= proximityValue && Math.Abs(_cat.Y + this.Y) <= proximityValue))
            {
                // Follow cat

                if (!_maze.CheckCollision(this, Maze.Directions.Right) && _cat.X > this.X)
                {
                    X += speed * 0.7;
                }

                if (!_maze.CheckCollision(this, Maze.Directions.Left) && _cat.X < this.X)
                {
                    X -= speed * 0.7;
                }

                if (!_maze.CheckCollision(this, Maze.Directions.Down) && _cat.Y > this.Y)
                {
                    Y += speed * 0.7;
                }

                if (!_maze.CheckCollision(this, Maze.Directions.Top) && _cat.Y < this.Y)
                {
                    Y -= speed * 0.7;
                }
            }
        }

        public override void Collided()
        {
            _game.GhostMeetCount++;
            Sound _sound = new Sound();
            _sound.EatGhost();
        }
    }
}