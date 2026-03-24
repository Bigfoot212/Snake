using System;

namespace Snake
{
    /// <summary>
    /// Rozhraní pro vstup. Opět kvůli decouplingu - engine se jen ptá "kam mám jet?",
    /// neřeší jestli se zmáčkla klávesa na klávesnici nebo na gamepadu.
    /// </summary>
    public interface IInputHandler
    {
        Direction GetNextDirection(Direction currentDirection);
    }
}
