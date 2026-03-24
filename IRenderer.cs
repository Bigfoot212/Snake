using System;

namespace Snake
{
    /// <summary>
    /// Rozhraní pro vykreslování. Podle zadání (Clean Code) chceme herní logiku 
    /// úplně oddělit od GUI. Engine nesmí vědět, že kreslí do konzole.
    /// </summary>
    public interface IRenderer
    {
        void Setup(int width, int height);
        void Clear();
        void DrawPoint(Position position, ConsoleColor color);
        void DrawBorders(int width, int height);
        void ShowGameOver(int score, int width, int height);
        
        // Nová metoda pro optimalizaci - maže konkrétní bod místo celé obrazovky
        void ClearPoint(Position position);
    }
}
