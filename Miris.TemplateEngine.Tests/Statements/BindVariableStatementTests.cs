using Microsoft.VisualStudio.TestTools.UnitTesting;
using Miris.TemplateEngine.Engines.RazorAlike.Statements;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Miris.TemplateEngine.Tests.Statements
{

    [TestClass]
    public class BindVariableStatementTests
    {
        readonly BindVariableStatement statement = new BindVariableStatement();

        [TestMethod]
        public void PositiveTest()
        {
            var (output, _) = statement.Execute(
                "Meu nome é @Marcus.Nome @Marcus.Sobrenome, e minha idade é @Marcus.Idade.",
                new StringReader(""),
                new StringBuilder(),
                new Dictionary<string, object>()
                {
                    { "Marcus", new { Nome = "Marcus", Sobrenome = "Miris", Idade = 33 } }
                });

            Assert.AreEqual(
                "Meu nome é Marcus Miris, e minha idade é 33.",
                output.ToString().Trim());
        }

    }
}
