using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject
{
    public class Fish : GameObject
    {
        public Fish(MazeGame game,Window gameWindow, Maze maze, Point2D position) : base(game, gameWindow, maze, new Bitmap("Front", "front.png"))
        {
            _ObjectBitmap = new Bitmap("Fish", "fish.png");
            _maze = maze;
            _gameWindow = gameWindow;
            float scale = (gameWindow.Height / ((float)maze._mazeSize * _ObjectBitmap.Width));
            X = position.X;
            Y = position.Y;
            drawingOptions = new DrawingOptions() { ScaleX = scale, ScaleY = scale };
        }

        public override void Update(Cat cat)
        {
        }

        public override void Collided()
        {
            _game.FishCount++;
            Sound _sound = new Sound();
            _sound.EatFish();
        }
    }
}
