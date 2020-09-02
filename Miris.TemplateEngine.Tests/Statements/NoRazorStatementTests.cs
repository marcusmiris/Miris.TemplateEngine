using Microsoft.VisualStudio.TestTools.UnitTesting;
using Miris.TemplateEngine.Engines.RazorAlike.Statements;
using System.Text;

namespace Miris.TemplateEngine.Tests.Statements
{
    [TestClass]
    public class NoRazorStatementTests
    {
        [TestMethod]
        public void ExecutePositiveTests()
        {
            var candidate = new NoRazorStatement();
            var currentLine = "Minha vó tem muitos jarros";

            var (output, _) = candidate.Execute(
                currentLine,
                templateReader: null,
                new StringBuilder());

            Assert.AreEqual(currentLine, output.ToString().Trim());
        }
    }
}
