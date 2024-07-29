using Microsoft.Extensions.Logging;
using RedRoverPuzzle.Interfaces;
using System.Text;

namespace RedRoverPuzzle
{
    public class NodeParser : INodeParser
    {
        private readonly ILogger<NodeParser> _logger;

        public NodeParser(ILogger<NodeParser> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Parses string into hierarchical node structure
        /// </summary>
        /// <param name="input">The input string to parse</param>
        /// <returns>THe root node of the parsed structure</returns>
        public Node ParseString(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentNullException(nameof(input), "Input string cannot be null or empty");
            }

            input = input.Trim('(', ')').Replace(" ", string.Empty);

            Node root = new("root");
            Stack<Node> stack = new();
            StringBuilder sb = new();

            foreach (char c in input)
            {
                if (IsDelimiter(c))
                {
                    // add current token as a child to the root node
                    if (sb.Length > 0)
                    {
                        root.Children.Add(new(sb.ToString()));
                        sb.Clear();
                    }
                    if (c == '(')
                    {
                        // set last child as new root
                        stack.Push(root);
                        root = root.Children[^1];
                    }
                    else if (c == ')')
                    {
                        // pop stack to return to previous root
                        root = stack.Pop();
                    }
                }
                else
                {
                    // append non-delimiter characters to current token
                    sb.Append(c);
                }
            }
            if (sb.Length > 0)
            {
                // add last token as a child to the root node
                root.Children.Add(new(sb.ToString()));
            }
            return root;
        }
        private static bool IsDelimiter(char c)
        {
            return c == '(' || c == ')' || c == ',';
        }
    }
}
