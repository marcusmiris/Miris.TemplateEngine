using Microsoft.VisualStudio.TestTools.UnitTesting;
using Miris.TemplateEngine.Engines.RazorAlike.Expressions;

namespace Miris.TemplateEngine.Tests.Expressions
{
    [TestClass]
    public class IntExpressionResolverTest
    {


        [TestMethod]
        public void PositiveTests()
        {
            var candidate = new IntExpressionResolver();

            // int
            object result = candidate.Resolve("0", null);
            Assert.IsInstanceOfType(result, typeof(int));
            Assert.AreEqual(0, result);

        }

    }
}
