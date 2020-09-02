using Microsoft.VisualStudio.TestTools.UnitTesting;
using Miris.TemplateEngine.Engines.RazorAlike.Expressions;
using System.Collections.Generic;

namespace Miris.TemplateEngine.Tests.Expressions
{
    [TestClass]
    public class EqualityExpressionResolverTest
    {

        [TestMethod]
        public void PositiveTest()
        {
            var candidate = new EqualityExpressionResolver();

            void test(bool expectedValue, string expression, IDictionary<string, object> model)
            {
                object result = candidate.Resolve(expression, model);
                Assert.IsInstanceOfType(result, typeof(bool));
                Assert.AreEqual(expectedValue, result);
            }

            // int
            test(true, "0==0", null);
            test(false, "0==1", null);

            // model & string
            test(true, @"@Name == ""John""", new Dictionary<string, object>()
            {
                { "Name", "John" }
            });
            test(false, @"@Name == ""Peter""", new Dictionary<string, object>()
            {
                { "Name", "John" }
            });
        }

    }
}
