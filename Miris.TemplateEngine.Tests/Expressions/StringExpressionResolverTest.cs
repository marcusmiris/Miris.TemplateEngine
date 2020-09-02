using Microsoft.VisualStudio.TestTools.UnitTesting;
using Miris.TemplateEngine.Engines.RazorAlike.Expressions;

namespace Miris.TemplateEngine.Tests.Expressions
{

    [TestClass]
    public class StringExpressionResolverTest
    {
        private StringExpressionResolver resolver 
            = new StringExpressionResolver();


        [TestMethod]
        public void NegativeTest()
        {
            var candidate = @"Not Quoted String";
            Assert.IsFalse(resolver.Accept(candidate));

            candidate = @"Single Quoted""String";
            Assert.IsFalse(resolver.Accept(candidate));

            candidate = @"""Not quoted endend string";
            Assert.IsFalse(resolver.Accept(candidate));

            candidate = @"Not started with quoted""";
            Assert.IsFalse(resolver.Accept(candidate));
        }

        [TestMethod]
        public void PositiveTest()
        {
            var resolver = new StringExpressionResolver();

            void test(string expectedValue, string expression)
            {
                Assert.IsTrue(resolver.Accept(expression), $"Not recognized as a valid string expression: `{ expression }`");
                var result = resolver.Resolve(expression, null);
                Assert.IsInstanceOfType(result, typeof(string));
                Assert.AreEqual(expectedValue, result);
            }

            test("started and ended with quotes", @"""started and ended with quotes""");
            test(@"with internal "" double quotes", @"""with internal """" double quotes""");
            test("", @"""""");
        }

    }
}
