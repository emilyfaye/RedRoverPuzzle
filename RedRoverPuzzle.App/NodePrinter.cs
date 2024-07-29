using RedRoverPuzzle.Interfaces;

namespace RedRoverPuzzle
{
    public class NodePrinter : INodePrinter
    {
        private readonly IWriter _writer;

        private const int IndentSize = 2;

        public NodePrinter(IWriter writer)
        {
            _writer = writer;
        }

        /// <summary>
        /// Prints nodes in the order they appear
        /// </summary>
        /// <param name="node">The root node to print</param>
        public void Print(Node node)
        {
            _writer.WriteLine("\nUnsorted Output:");
            PrintNode(node);
        }

        /// <summary>
        /// Prints nodes alphabetically
        /// </summary>
        /// <param name="node">The root node to printf</param>
        public void PrintAlphabetically(Node node)
        {
            _writer.WriteLine("\nSorted Output:");
            PrintNode(node, sortByName: true);
        }

        /// <summary>
        /// Recursively prints the node and its children with specified indentation and sorting
        /// </summary>
        /// <param name="node">The root node to print</param>
        /// <param name="level">The current indentation level used to format the output</param>
        /// <param name="sortByName">Indicates if the child nodes shoudl be sorted alphabetically</param>
        private void PrintNode(Node node, int level = 0, bool sortByName = false)
        {
            IEnumerable<Node> children = sortByName ? node.Children.OrderBy(n => n.Name) : node.Children;

            foreach (var child in children)
            {
                if (!string.IsNullOrEmpty(child.Name))
                {
                    string indent = new(' ', level * IndentSize);
                    _writer.WriteLine($"{indent}- {child.Name}");
                }

                PrintNode(child, level + 1, sortByName);
            }
        }
    }
}
