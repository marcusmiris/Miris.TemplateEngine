using Microsoft.VisualStudio.TestTools.UnitTesting;
using Miris.TemplateEngine.Engines.RazorAlike.Statements;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Miris.TemplateEngine.Tests.Statements
{
    [TestClass]
    public class SingleLineIfStatementTests
    {

        [TestMethod]
        public void AcceptPositiveTests()
        {
            var ifStatement = new SingleLineIfStatement();

            Assert.IsTrue(ifStatement.Accept("@if (true) {}"));
        }

        [TestMethod]
        public void AcceptNegativeTests()
        {
            var ifStatement = new SingleLineIfStatement();

            Assert.IsFalse(ifStatement.Accept("@if (true) Your ID é: @user.id."), "Should not accept without brakets.");
            Assert.IsFalse(ifStatement.Accept("@if (@boolVariable) Your ID é: @user.id."), "Should not accept @ (at) variable reference.");
            Assert.IsFalse(ifStatement.Accept("@if (@obj.BoolProperty) Your ID é: @user.id."), "Should not accept @ (at) variable reference.");
            Assert.IsFalse(ifStatement.Accept("@if (@variable == 0) Your ID é: @user.id."), "Should not accept @ (at) variable reference.");
            Assert.IsFalse(ifStatement.Accept("@if (0 == variable) Your ID é: @user.id."), "Should not accept @ (at) variable reference.");
        }

        [TestMethod]
        public void ExecutePositiveTest_TrueConstantStatement()
        {
            var output = new StringBuilder();
            var templateReader = new StringReader("@if (true) { B }");

            var ifStatement = new SingleLineIfStatement();
            (output, _) = ifStatement.Execute(templateReader, output);

            Assert.AreEqual("B", output.ToString().Trim());
        }


        [TestMethod]
        public void ExecutePositiveTest_FalseConstantStatement()
        {
            var output = new StringBuilder();
            var templateReader = new StringReader("@if (false) { B }");

            var ifStatement = new SingleLineIfStatement();
            (output, _) = ifStatement.Execute(templateReader, output);

            Assert.AreEqual("", output.ToString().Trim());
        }

        //
        [TestMethod]
        public void ExecutePositiveTest_EqualityConstantStatement()
        {
            var output = new StringBuilder();
            var templateReader = new StringReader("@if (true == true) { ! }");

            var ifStatement = new SingleLineIfStatement();
            (output, _) = ifStatement.Execute(templateReader, output);

            Assert.AreEqual("!", output.ToString().Trim());
        }

        //
        [TestMethod]
        public void ExecutePositiveTest_VariableBindStatement()
        {
            var output = new StringBuilder();
            var templateReader = new StringReader("@if (variable) { ! }");

            var ifStatement = new SingleLineIfStatement();
            (output, _) = ifStatement.Execute(templateReader, output, new Dictionary<string, object>()
            {
                { "variable", true }
            });

            Assert.AreEqual("!", output.ToString().Trim());
        }

    }
}
