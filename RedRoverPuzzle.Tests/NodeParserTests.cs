using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using RedRoverPuzzle.Interfaces;

namespace RedRoverPuzzle.Tests
{
    public class NodeParserTests
    {
        private readonly INodeParser _parser;
        private readonly Mock<ILogger<NodeParser>> _mockLogger;

        public NodeParserTests()
        {
            _mockLogger = new Mock<ILogger<NodeParser>>();
            _parser = new NodeParser(_mockLogger.Object);
        }

        [Fact]
        public void ParseString_ReturnsValidStructure()
        {
            string input = "(id, name, email, type(id, name, customFields(c1, c2, c3)), externalId)";
            Node root = _parser.ParseString(input);

            Assert.NotNull(root);
            Assert.Equal(5, root.Children.Count);
            Assert.Equal("id", root.Children[0].Name);
            Assert.Equal("name", root.Children[1].Name);
            Assert.Equal("email", root.Children[2].Name);
            Assert.Equal("type", root.Children[3].Name);
            Assert.Equal("externalId", root.Children[4].Name);

            Node typeNode = root.Children[3];
            Assert.Equal(3, typeNode.Children.Count);
            Assert.Equal("id", typeNode.Children[0].Name);
            Assert.Equal("name", typeNode.Children[1].Name);
            Assert.Equal("customFields", typeNode.Children[2].Name);

            Node customFieldsNode = typeNode.Children[2];
            Assert.Equal(3, customFieldsNode.Children.Count);
            Assert.Equal("c1", customFieldsNode.Children[0].Name);
            Assert.Equal("c2", customFieldsNode.Children[1].Name);
            Assert.Equal("c3", customFieldsNode.Children[2].Name);
        }

        [Fact]
        public void ParseString_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _parser.ParseString(string.Empty));
        }
    }
}