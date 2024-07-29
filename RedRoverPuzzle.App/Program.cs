using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RedRoverPuzzle;
using RedRoverPuzzle.Interfaces;

class Program
{
    private static void Main(string[] args)
    {
        var serviceProvider = ConfigureServices();

        var logger = serviceProvider.GetService<ILogger<Program>>();

        logger?.LogInformation("Application started");

        var parser = serviceProvider.GetService<INodeParser>();
        var printer = serviceProvider.GetService<INodePrinter>();

        if (parser == null || printer == null)
        {
            logger?.LogError("Failed to initialize services.");
            return;
        }

        try
        {
            Run(parser, printer);
        }
        catch (Exception ex)
        {
            logger?.LogError($"An error occurred: {ex.Message}");
        }

        Console.WriteLine("\nThanks for Playing! Press any key to exit.");

        Console.ReadLine();

        logger?.LogInformation("Application stopped");
    }

    private static void Run(INodeParser parser, INodePrinter printer)
    {
        string input = "(id, name, email, type(id, name, customFields(c1, c2, c3)), externalId)";

        Node rootNode = parser.ParseString(input);

        printer.Print(rootNode);

        printer.PrintAlphabetically(rootNode);
    }

    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        services.AddSingleton<INodeParser, NodeParser>();
        services.AddSingleton<INodePrinter, NodePrinter>();
        services.AddSingleton<IWriter, ConsoleWriter>();

        services.AddLogging(configure => configure.AddConsole());

        return services.BuildServiceProvider();
    }
}