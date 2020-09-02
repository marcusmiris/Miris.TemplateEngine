using Microsoft.VisualStudio.TestTools.UnitTesting;
using Miris.TemplateEngine.Engines.RazorAlike.Expressions;
using System.Collections.Generic;

namespace Miris.TemplateEngine.Tests.Expressions
{
    [TestClass]
    public class ModelExpressionResolverTest
    {

        [TestMethod]
        public void PositiveTest()
        {

            var candidate = new ModelBindExpressionResolver();


            var result = candidate.Resolve(
                "@FooBar",
                new Dictionary<string, object>()
                {
                    { "FooBar", 12 }
                });

            Assert.IsInstanceOfType(result, typeof(int));
            Assert.AreEqual(12, result);
        }

    }
}
