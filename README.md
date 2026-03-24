# Snake Game - Refactoring Project

Tento projekt je ukázkou komplexní refaktorizace legacy kódu hry Snake (C#) podle principů knihy **Clean Code** (Robert C. Martin).

## Zadání a Cíl
Cílem bylo vzít monolitický, špatně čitelný kód s holandským názvoslovím a transformovat jej do čisté, objektově orientované a udržovatelné podoby. Velký důraz byl kladen na **decoupling** (oddělení herní logiky od GUI).

## Provedené Refaktorizace

### 1. Čitelnost a Naming
- Přechod z nizozemštiny do angličtiny.
- Dodržení C# jmenných konvencí (PascalCase, camelCase).
- Odstranění "Magic Numbers" a jejich nahrazení konstantami.

### 2. Dekompozice (SRP)
Původní monolit v `Program.cs` byl rozbit do logických celků:
- `Snake`: Zapouzdřená logika pohybu a růstu.
- `Food`: Logika generování jídla.
- `GameEngine`: Orchestrace herní smyčky bez závislosti na UI.

### 3. Decoupling (Abstrakce)
Byla zavedena rozhraní `IRenderer` a `IInputHandler`. Díky tomu:
- Herní engine neví nic o existenci konzole ani o barvách C#.
- Je možné snadno přidat jiný grafický výstup (např. WPF, MonoGame) bez úpravy logiky hada.

### 4. Optimalizace a UX
- Implementace **flicker-free** vykreslování (překreslují se jen změněné pixely).
- Použití `Stopwatch` pro přesné časování pohybu.
- Typová bezpečnost pomocí `enum Direction` a `struct Position`.

## Jak spustit
Projekt vyžaduje **.NET SDK** (verze 8.0 nebo novější - doporučeno 10.0).
1. Otevřete terminál ve složce projektu.
2. Spusťte hru příkazem:
   ```bash
   dotnet run
   ```

## Struktura projektu
- `Program.cs`: Entry-point a Dependency Injection.
- `GameEngine.cs`: Jádro herní smyčky.
- `Snake.cs`: Třída herního objektu hada.
- `IRenderer.cs` / `ConsoleRenderer.cs`: Abstrakce a implementace grafického výstupu.
- `IInputHandler.cs` / `ConsoleInputHandler.cs`: Abstrakce a implementace vstupů.
