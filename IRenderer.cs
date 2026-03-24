using System;

namespace Snake
{
    public interface IRenderer
    {
        void Setup(int width, int height);
        void Clear();
        void DrawBorders(int width, int height);
        
        // Engine už neříká barvu, renderer si ji vybere sám
        void DrawHead(Position position);
        void DrawSnakeBody(IEnumerable<Position> body);
        void DrawFood(Position position);
        void ClearPoint(Position position);
        
        void ShowGameOver(int score, int width, int height);
    }
}
