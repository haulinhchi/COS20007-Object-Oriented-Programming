using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject
{
    public class MazeGame
    {
        Maze _maze;
        public Cat _cat;
        List<GameObject> _objects = new List<GameObject>();
        Window _gameWindow;
        private SplashKitSDK.Timer _timer;
        private int fishCount;
        private int ghostmeetCount;

        private int _countdownDuration = 60;
        private DateTime _previousUpdateTime;
        private int _remainingSeconds;
        public EndScreen _endScreen;
        public EndScreen EndScreenInstance
        {
            get { return _endScreen; }
        }

        public int FishCount
        {
            get { return fishCount; }
            set { fishCount = value; }
        }

        public int GhostMeetCount
        {
            get { return ghostmeetCount; }
            set { ghostmeetCount = value;}
        }

        public bool Quit
        {
            get
            {
                return _cat.Quit;
            }
        }

        public MazeGame(Window window)
        {
            Sound _sound = new Sound();
            _sound.BackgroundMusic();

            _gameWindow = window;
            int mazeSize = 25;
            int ghostCount = 15;


            _remainingSeconds = _countdownDuration;
            _previousUpdateTime = DateTime.Now;


            fishCount = 0;
            ghostmeetCount = 0;
            _maze = new Maze(window, mazeSize);
            _cat = new Cat(this, _gameWindow, _maze);

            for (int i = 0; i < ghostCount; i++)
            {
                _objects.Add(new Ghost(this, _gameWindow, _maze));
            }

            _timer = new SplashKitSDK.Timer("Time: ");
            _timer.Start();
            _endScreen = new EndScreen(window, "");
        }

        public void Draw()
        {
            _gameWindow.Clear(SplashKitSDK.Color.Black);
            _maze.Draw();
            _cat.Draw();

            foreach (var projectile in _cat._projectiles)
            {
                projectile.Draw();
            }
            foreach (var obj in _objects)
            {
                obj.Draw();
            }
            RenderScoreTime();
            _gameWindow.Refresh(60);
        }

        public void HandleInput()
        {
            _cat.HandleInput();
        }

        public void Update()
        {
            DateTime currentTime = DateTime.Now;
            TimeSpan elapsedTime = currentTime - _previousUpdateTime;

            if (elapsedTime.TotalSeconds >= 1)
            {
                _remainingSeconds--;
                _previousUpdateTime = currentTime;
            }

            foreach (var obj in _objects)
            {
                obj.Update(_cat);
            }
            foreach (var projectile in _cat._projectiles)
            {
                projectile.Update(_cat);
            }

            if (CheckPosition(_cat))
            {
                _cat.Quit = true;
                _endScreen._endText = $"You won the game! - Score: {fishCount - ghostmeetCount} - Time taken: {_countdownDuration - _remainingSeconds} seconds";
            }

            if (_remainingSeconds < 0)
            {
                _cat.Quit = true;
                _endScreen._endText = $"Oh no, the time is up! Let' s try again! - Score: {fishCount - ghostmeetCount}";
            }


            if (fishCount - ghostmeetCount < 0)
            {
                _cat.Quit = true;
                _endScreen._endText = $"You lose! Score is negative - Time taken: {_countdownDuration - _remainingSeconds} seconds";
            }

            CheckCollisions();
        }

        public void CheckCollisions()
        {
            List<Projectile> removeProjectile = new List<Projectile>();
            List<GameObject> removeObj = new List<GameObject>();
            List<GameObject> addObj = new List<GameObject>();


            foreach (var obj in _objects)
            {
                if (_cat.CollisionTest(obj))
                {
                    removeObj.Add(obj);
                    obj.Collided();
                    break;
                }
                foreach (var projectile in _cat._projectiles)
                {
                    if (obj.CollisionTest(projectile) && obj is Ghost)
                    {
                        removeProjectile.Add(projectile);
                        removeObj.Add(obj);
                        addObj.Add(new Fish(this, _gameWindow, _maze, new Point2D() { X = obj.X, Y = obj.Y }));
                        Sound _sound = new Sound();
                        _sound.ProjectileSound();
                        break;
                    }
                }
            }

            foreach (var projectile in _cat._projectiles)
            {
                if (_maze.CheckCollision(projectile, Maze.Directions.None))
                {
                    removeProjectile.Add(projectile);
                }
            }

            foreach (var projectile in removeProjectile)
            {
                _cat._projectiles.Remove(projectile);
            }

            foreach (var obj in addObj)
            {
                _objects.Add(obj);
            }

            foreach (var obj in removeObj)
            {
                _objects.Remove(obj);
            }
        }

        public bool CheckPosition(GameObject gameobject)
        {
            if (Math.Abs(_maze.endPoint.X - gameobject.X) > _maze.wallWidth / 2 || Math.Abs(_maze.endPoint.Y - gameobject.Y) > _maze.wallWidth / 2)
                return false;
            else
                return true;
        }

        private void RenderScoreTime()
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(_remainingSeconds);
            _gameWindow.DrawText($"Time: {timeSpan.Minutes:00}:{timeSpan.Seconds:00}", SplashKitSDK.Color.Black, 80, 13);
            _gameWindow.DrawText("Score: " + (fishCount - ghostmeetCount), SplashKitSDK.Color.Black, 200, 13);
        }
    }
}