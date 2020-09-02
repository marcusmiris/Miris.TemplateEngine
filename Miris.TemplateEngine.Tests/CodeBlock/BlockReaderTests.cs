using Microsoft.VisualStudio.TestTools.UnitTesting;
using Miris.TemplateEngine.Engines.RazorAlike.CodeBlock;
using System.IO;

namespace Miris.TemplateEngine.Tests.CodeBlock
{

    [TestClass]
    public class BlockReaderTests
    {

        [TestMethod]
        public void SingleBlockPositiveTests()
        {
            var reader = new StringReader(@"
            {
                BLOCK
            }!");

            var blockReader = BlockReader.ReadBlock(reader);
            Assert.AreEqual("BLOCK", blockReader.ReadToEnd().Trim());
            Assert.AreEqual('!', reader.Read());
        }

        [TestMethod]
        public void MultipleBlockPositiveTests()
        {
            var reader = new StringReader(@"
            {
                { BLOCK }
            }!");

            var blockReader = BlockReader.ReadBlock(reader);
            Assert.AreEqual("{ BLOCK }", blockReader.ReadToEnd().Trim());
            Assert.AreEqual('!', reader.Read());
        }


    }
}
