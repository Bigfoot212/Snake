using System;

namespace Snake
{
    public class ConsoleInputHandler : IInputHandler
    {
        public Direction GetNextDirection(Direction current)
        {
            if (!Console.KeyAvailable) return current;

            var key = Console.ReadKey(true).Key;
            
            // Logika "nemůžeš jet do protisměru" je tady správně zapouzdřená
            return key switch
            {
                ConsoleKey.UpArrow when current != Direction.Down => Direction.Up,
                ConsoleKey.DownArrow when current != Direction.Up => Direction.Down,
                ConsoleKey.LeftArrow when current != Direction.Right => Direction.Left,
                ConsoleKey.RightArrow when current != Direction.Left => Direction.Right,
                _ => current
            };
        }
    }
}
