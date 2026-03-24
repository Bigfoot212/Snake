using System;
using System.Collections.Generic;
using static System.Console;

namespace Snake
{
    public class ConsoleRenderer : IRenderer
    {
        private const ConsoleColor BorderColor = ConsoleColor.White;
        private const ConsoleColor HeadColor = ConsoleColor.Red;
        private const ConsoleColor BodyColor = ConsoleColor.Green;
        private const ConsoleColor FoodColor = ConsoleColor.Cyan;

        public void Setup(int width, int height)
        {
            try { WindowWidth = width; WindowHeight = height; }
            catch (PlatformNotSupportedException) { }
            CursorVisible = false;
        }

        public void Clear() => Console.Clear();

        public void DrawHead(Position position) => DrawAt(position, HeadColor, "■");
        
        public void DrawSnakeBody(IEnumerable<Position> body)
        {
            foreach (var part in body) DrawAt(part, BodyColor, "■");
        }

        public void DrawFood(Position position) => DrawAt(position, FoodColor, "■");

        public void ClearPoint(Position position)
        {
            SetCursorPosition(position.X, position.Y);
            Write(" ");
        }

        public void DrawBorders(int width, int height)
        {
            ForegroundColor = BorderColor;
            for (int i = 0; i < width; i++)
            {
                DrawRaw(i, 0, "■");
                DrawRaw(i, height - 1, "■");
            }
            for (int i = 0; i < height; i++)
            {
                DrawRaw(0, i, "■");
                DrawRaw(width - 1, i, "■");
            }
        }

        private void DrawAt(Position pos, ConsoleColor color, string s)
        {
            ForegroundColor = color;
            SetCursorPosition(pos.X, pos.Y);
            Write(s);
        }

        private void DrawRaw(int x, int y, string s)
        {
            SetCursorPosition(x, y);
            Write(s);
        }

        public void ShowGameOver(int score, int width, int height)
        {
            SetCursorPosition(width / 5, height / 2);
            ForegroundColor = ConsoleColor.White;
            WriteLine($"Game over, Score: {score}");
        }
    }
}
