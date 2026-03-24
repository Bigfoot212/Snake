using System;

namespace Snake
{
    public class Food
    {
        public Position Position { get; private set; }
        private readonly Random _random = new Random();

        public void Respawn(int width, int height) => 
            Position = new Position(_random.Next(1, width - 2), _random.Next(1, height - 2));
    }
}
