using System;
using System.Diagnostics; // Pro Stopwatch
using System.Threading;

namespace Snake
{
    /// <summary>
    /// Herní engine - srdce aplikace. Podle zadání Clean Code by měl orchestrovat hru,
    /// ale nevědět nic o konkrétním GUI (používá IRenderer).
    /// </summary>
    public class GameEngine
    {
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

            _snake = new Snake(new Position(width / 2, height / 2), 5);
            _food = new Food();
            _food.Respawn(width, height);
        }

        public void Run()
        {
            _renderer.Setup(_width, _height);
            _renderer.Clear();
            _renderer.DrawBorders(_width, _height); // Border kreslíme jen jednou (tip z fóra)

            while (!_isGameOver)
            {
                RenderFrame();
                UpdateState();
                
                // Místo Thread.Sleep použijeme Stopwatch pro přesnější timing vstupu
                var sw = Stopwatch.StartNew();
                while (sw.ElapsedMilliseconds < _delay)
                {
                    _direction = _input.GetNextDirection(_direction);
                }
            }

            _renderer.ShowGameOver(_snake.Length, _width, _height);
        }

        /// <summary>
        /// Optimalizované vykreslování: Nekreslíme vše znovu, 
        /// kreslíme jen hlavu, jídlo a mažeme poslední článek ocasu.
        /// Tím odstraníme blikání.
        /// </summary>
        private void RenderFrame()
        {
            _renderer.DrawPoint(_food.Position, ConsoleColor.Cyan);
            
            // Kreslíme články těla, které přibyly (hlavu v minulé pozici)
            foreach (var part in _snake.Body) 
                _renderer.DrawPoint(part, ConsoleColor.Green);
            
            _renderer.DrawPoint(_snake.Head, ConsoleColor.Red);
        }

        private void UpdateState()
        {
            // Před pohybem si uložíme pozici ocasu, abychom ho mohli smazat
            Position tailToClear = _snake.Body.Count > 0 ? _snake.Body[0] : _snake.Head;

            _snake.Move(_direction);

            // Pokud jsme nevyrostli, smažeme starý ocas z obrazovky
            if (_snake.Body.Count >= _snake.Length)
            {
                _renderer.ClearPoint(tailToClear);
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
