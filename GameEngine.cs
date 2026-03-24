using System;
using System.Threading;

namespace Snake
{
    public class GameEngine
    {
        private const int Width = 32;
        private const int Height = 16;
        private const int Delay = 500;

        private readonly Snake _snake;
        private readonly Food _food;
        private Direction _direction = Direction.Right;
        private bool _isGameOver;

        public GameEngine()
        {
            _snake = new Snake(new Position(Width / 2, Height / 2), 5);
            _food = new Food();
            _food.Respawn(Width, Height);
        }

        public void Run()
        {
            SetupConsole();

            while (!_isGameOver)
            {
                RenderFrame();
                UpdateState();
                Thread.Sleep(Delay);
            }

            ShowGameOver();
        }

        private void SetupConsole()
        {
            Console.WindowWidth = Width;
            Console.WindowHeight = Height;
            Console.CursorVisible = false;
        }

        private void RenderFrame()
        {
            Console.Clear();
            DrawBorders();
            
            Console.ForegroundColor = ConsoleColor.Cyan;
            DrawAt(_food.Position, "■");
            
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (var part in _snake.Body) DrawAt(part, "■");
            
            Console.ForegroundColor = ConsoleColor.Red;
            DrawAt(_snake.Head, "■");
        }

        private void DrawBorders()
        {
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < Width; i++)
            {
                DrawAt(new Position(i, 0), "■");
                DrawAt(new Position(i, Height - 1), "■");
            }
            for (int i = 0; i < Height; i++)
            {
                DrawAt(new Position(0, i), "■");
                DrawAt(new Position(Width - 1, i), "■");
            }
        }

        private void DrawAt(Position pos, string s)
        {
            Console.SetCursorPosition(pos.X, pos.Y);
            Console.Write(s);
        }

        private void UpdateState()
        {
            _direction = GetInput(_direction);
            _snake.Move(_direction);

            if (_snake.Head.X == _food.Position.X && _snake.Head.Y == _food.Position.Y)
            {
                _snake.Grow();
                _food.Respawn(Width, Height);
            }

            if (_snake.IsDead(Width, Height)) _isGameOver = true;
        }

        private Direction GetInput(Direction current)
        {
            if (!Console.KeyAvailable) return current;

            var key = Console.ReadKey(true).Key;
            return key switch
            {
                ConsoleKey.UpArrow when current != Direction.Down => Direction.Up,
                ConsoleKey.DownArrow when current != Direction.Up => Direction.Down,
                ConsoleKey.LeftArrow when current != Direction.Right => Direction.Left,
                ConsoleKey.RightArrow when current != Direction.Left => Direction.Right,
                _ => current
            };
        }

        private void ShowGameOver()
        {
            Console.SetCursorPosition(Width / 5, Height / 2);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Game over, Score: {_snake.Length}");
        }
    }
}
