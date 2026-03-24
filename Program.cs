namespace Snake
{
    class Program
    {
        static void Main()
        {
            // Podle Clean Code je Program.cs jen entry-point, kde se 
            // poskládají (vstříknou) závislosti.
            var engine = new GameEngine(
                width: 32, 
                height: 16, 
                delay: 150, // Zrychlil jsem to, 500ms bylo moc pomalé
                renderer: new ConsoleRenderer(), 
                input: new ConsoleInputHandler()
            );
            
            engine.Run();
        }
    }
}
