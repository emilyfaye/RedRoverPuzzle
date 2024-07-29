using RedRoverPuzzle.Interfaces;

namespace RedRoverPuzzle
{
    public class ConsoleWriter : IWriter
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}
