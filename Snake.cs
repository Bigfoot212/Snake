using System;
using System.Collections.Generic;
using System.Linq;

namespace Snake
{
    public class Snake
    {
        public Position Head { get; private set; }
        
        // Zapouzdření - vracíme ReadOnly, aby nikdo zvenku nemohl měnit články
        public IReadOnlyList<Position> Body => _body.AsReadOnly();
        private readonly List<Position> _body = new List<Position>();
        
        public int Length { get; private set; }

        public Snake(Position startPosition, int initialLength)
        {
            Head = startPosition;
            Length = initialLength;
        }

        public void Move(Direction direction)
        {
            _body.Add(Head);
            Head = GetNextPosition(direction);
            if (_body.Count > Length) _body.RemoveAt(0);
        }

        public void Grow() => Length++;

        public bool IsDead(int width, int height)
        {
            return IsOutOfBounds(width, height) || IsHittingItself();
        }

        private bool IsOutOfBounds(int width, int height)
        {
            return Head.X <= 0 || Head.X >= width - 1 || 
                   Head.Y <= 0 || Head.Y >= height - 1;
        }

        private bool IsHittingItself()
        {
            return _body.Any(p => p.X == Head.X && p.Y == Head.Y);
        }

        private Position GetNextPosition(Direction dir) => dir switch
        {
            Direction.Up => new Position(Head.X, Head.Y - 1),
            Direction.Down => new Position(Head.X, Head.Y + 1),
            Direction.Left => new Position(Head.X - 1, Head.Y),
            Direction.Right => new Position(Head.X + 1, Head.Y),
            _ => Head
        };
    }
}
