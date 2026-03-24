using System;
using System.Diagnostics;
using System.Threading;

namespace Snake
{
    public class GameEngine
    {
        // Magic numbers nahrazeny konstantami s jasným významem
        private const int DefaultInitialLength = 5;
        
        private readonly int _width;
        private readonly int _height;
        private readonly int _delay;
        
        private readonly IRenderer _renderer;
        private readonly IInputHandler _input;
        private readonly Snake _snake;
        private readonly Food _food;

        private Direction _direction = Direction.Right;
        private bool _isGameOver;

        public GameEngine(int width, int height, int delay, IRenderer renderer, IInputHandler input)
        {
            _width = width;
            _height = height;
            _delay = delay;
            _renderer = renderer;
            _input = input;

            _snake = new Snake(new Position(width / 2, height / 2), DefaultInitialLength);
            _food = new Food();
            _food.Respawn(width, height);
        }

        public void Run()
        {
            _renderer.Setup(_width, _height);
            _renderer.Clear();
            _renderer.DrawBorders(_width, _height);

            while (!_isGameOver)
            {
                RenderFrame();
                UpdateState();
                
                WaitForNextFrame();
            }

            _renderer.ShowGameOver(_snake.Length, _width, _height);
        }

        private void WaitForNextFrame()
        {
            var sw = Stopwatch.StartNew();
            while (sw.ElapsedMilliseconds < _delay)
            {
                _direction = _input.GetNextDirection(_direction);
                Thread.Sleep(1); // Neužírat 100% CPU při čekání
            }
        }

        private void RenderFrame()
        {
            // Engine už neřeší barvy, jen říká CO se má kreslit
            _renderer.DrawFood(_food.Position);
            _renderer.DrawSnakeBody(_snake.Body);
            _renderer.DrawHead(_snake.Head);
        }

        private void UpdateState()
        {
            // Uložíme si, kde končil ocas v minulém tahu
            // Pokud se pohneme a nevyrosteme, Renderer ho smaže
            Position? tailToClear = _snake.Body.Count > 0 ? _snake.Body[0] : null;

            _snake.Move(_direction);

            // Pokud jsme nevyrostli, Renderer smaže starý konec ocasu
            if (_snake.Body.Count >= _snake.Length && tailToClear.HasValue)
            {
                _renderer.ClearPoint(tailToClear.Value);
            }

            if (HasSnakeEatenFood())
            {
                _snake.Grow();
                _food.Respawn(_width, _height);
            }

            if (_snake.IsDead(_width, _height)) 
                _isGameOver = true;
        }

        private bool HasSnakeEatenFood()
        {
            return _snake.Head.X == _food.Position.X && 
                   _snake.Head.Y == _food.Position.Y;
        }
    }
}
