using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Snake
{
    class Program
    {
        private const int WindowWidth = 32;
        private const int WindowHeight = 16;
        private const int InitialScore = 5;
        private const int GameDelayMs = 500;

        private static readonly Random RandomGenerator = new Random();

        // Coordinates representation
        struct Position
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        static void Main(string[] args)
        {
            SetupWindow();

            var head = new Position { X = WindowWidth / 2, Y = WindowHeight / 2 };
            var body = new List<Position>();
            var food = GenerateFood();
            
            var movement = Direction.Right;
            var score = InitialScore;
            var isGameOver = false;

            while (!isGameOver)
            {
                Console.Clear();
                
                DrawBorders();
                DrawFood(food);
                DrawSnake(head, body);

                if (IsCollisionWithBorders(head) || IsCollisionWithBody(head, body))
                {
                    isGameOver = true;
                    break;
                }

                if (IsEaten(head, food))
                {
                    score++;
                    food = GenerateFood();
                }

                Direction nextMove = GetInput(movement);
                movement = nextMove;

                body.Add(new Position { X = head.X, Y = head.Y });
                head = MoveHead(head, movement);

                if (body.Count > score)
                {
                    body.RemoveAt(0);
                }

                Thread.Sleep(GameDelayMs);
            }

            ShowGameOver(score);
        }

        private static void SetupWindow()
        {
            Console.WindowHeight = WindowHeight;
            Console.WindowWidth = WindowWidth;
            Console.CursorVisible = false;
        }

        private static void DrawBorders()
        {
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < WindowWidth; i++)
            {
                DrawAt(i, 0, "■");
                DrawAt(i, WindowHeight - 1, "■");
            }
            for (int i = 0; i < WindowHeight; i++)
            {
                DrawAt(0, i, "■");
                DrawAt(WindowWidth - 1, i, "■");
            }
        }

        private static void DrawAt(int x, int y, string symbol)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
        }

        private static Position GenerateFood()
        {
            return new Position
            {
                X = RandomGenerator.Next(1, WindowWidth - 2),
                Y = RandomGenerator.Next(1, WindowHeight - 2)
            };
        }

        private static void DrawFood(Position food)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            DrawAt(food.X, food.Y, "■");
        }

        private static void DrawSnake(Position head, List<Position> body)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (var part in body)
            {
                DrawAt(part.X, part.Y, "■");
            }

            Console.ForegroundColor = ConsoleColor.Red;
            DrawAt(head.X, head.Y, "■");
        }

        private static bool IsCollisionWithBorders(Position head)
        {
            return head.X <= 0 || head.X >= WindowWidth - 1 || 
                   head.Y <= 0 || head.Y >= WindowHeight - 1;
        }

        private static bool IsCollisionWithBody(Position head, List<Position> body)
        {
            return body.Any(part => part.X == head.X && part.Y == head.Y);
        }

        private static bool IsEaten(Position head, Position food)
        {
            return head.X == food.X && head.Y == food.Y;
        }

        private static Direction GetInput(Direction currentDirection)
        {
            if (!Console.KeyAvailable) return currentDirection;

            ConsoleKey key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.UpArrow when currentDirection != Direction.Down:
                    return Direction.Up;
                case ConsoleKey.DownArrow when currentDirection != Direction.Up:
                    return Direction.Down;
                case ConsoleKey.LeftArrow when currentDirection != Direction.Right:
                    return Direction.Left;
                case ConsoleKey.RightArrow when currentDirection != Direction.Left:
                    return Direction.Right;
                default:
                    return currentDirection;
            }
        }

        private static Position MoveHead(Position head, Direction direction)
        {
            switch (direction)
            {
                case Direction.Up: head.Y--; break;
                case Direction.Down: head.Y++; break;
                case Direction.Left: head.X--; break;
                case Direction.Right: head.X++; break;
            }
            return head;
        }

        private static void ShowGameOver(int score)
        {
            Console.SetCursorPosition(WindowWidth / 5, WindowHeight / 2);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Game over, Score: {score}");
        }

        enum Direction { Up, Down, Left, Right }
    }
}
