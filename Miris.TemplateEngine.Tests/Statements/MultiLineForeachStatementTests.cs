using Microsoft.VisualStudio.TestTools.UnitTesting;
using Miris.TemplateEngine.Engines.RazorAlike.Exceptions;
using Miris.TemplateEngine.Engines.RazorAlike.Statements;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Miris.TemplateEngine.Tests.Statements
{
    [TestClass]
    public class MultiLineForeachStatementTests
    {

        MultiLineForeachStatement candidate = new MultiLineForeachStatement();


        [TestMethod]
        public void PositiveTest()
        {
            var (output, reader) = candidate.Execute(
                "@foreach (var item in Collection)",
                new StringReader("{@item}"),
                new StringBuilder(),
                new Dictionary<string, object>()
                {
                    { "Collection", new List<int>() { 1, 2, 3} }
                });

            Assert.AreEqual("123", output.ToString());
        }

        [TestMethod]
        public void AcceptTests()
        {
            Assert.IsTrue(candidate.Accept("@foreach"));
            Assert.IsTrue(candidate.Accept("    @foreach"));

            Assert.IsFalse(candidate.Accept("foreach"));
        }

        [TestMethod]
        [ExpectedException(typeof(MalformedException))]
        public void MalformedStatement_NotForeach()
        {
            candidate.Execute(
                "asdf",
                new StringReader(""),
                new StringBuilder(),
                new Dictionary<string, object>());
        }

        [TestMethod]
        [ExpectedException(typeof(MalformedException))]
        public void MalformedCriteria()
        {
            candidate.Execute(
                "@foreach item in @Collection",
                new StringReader(""),
                new StringBuilder(),
                new Dictionary<string, object>());
        }

        [TestMethod]
        [ExpectedException(typeof(MalformedException))]
        public void MalformedCriteria_MissingVarKeyword()
        {
            candidate.Execute(
                "@foreach (item in @Collection)",
                new StringReader(""),
                new StringBuilder(),
                new Dictionary<string, object>());
        }

    }
}
