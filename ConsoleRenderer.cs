using System;
using static System.Console; // Tip z fóra: zkracuje kód, nemusíme psát Console. dokola

namespace Snake
{
    public class ConsoleRenderer : IRenderer
    {
        /// <summary>
        /// Inicializace konzole. Používáme try-catch kvůli macOS nekompatibilitě se SetWindowSize.
        /// </summary>
        public void Setup(int width, int height)
        {
            try
            {
                WindowWidth = width;
                WindowHeight = height;
            }
            catch (PlatformNotSupportedException) { }
            
            CursorVisible = false;
        }

        public void Clear() => Console.Clear();

        /// <summary>
        /// Vykreslí jeden "pixel". 
        /// </summary>
        public void DrawPoint(Position position, ConsoleColor color)
        {
            ForegroundColor = color;
            SetCursorPosition(position.X, position.Y);
            Write("■");
        }

        /// <summary>
        /// Smaže konkrétní bod. To je klíčové pro plynulost bez blikání.
        /// </summary>
        public void ClearPoint(Position position)
        {
            SetCursorPosition(position.X, position.Y);
            Write(" "); // Přemažeme obsah mezerou
        }

        public void DrawBorders(int width, int height)
        {
            ForegroundColor = ConsoleColor.White;
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
