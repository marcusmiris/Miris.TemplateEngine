using Microsoft.VisualStudio.TestTools.UnitTesting;
using Miris.TemplateEngine.Engines.RazorAlike.Model;
using System;
using System.Collections.Generic;

namespace Miris.TemplateEngine.Tests.Model
{
    [TestClass]
    public class ModelExtensionTests
    {

        [TestMethod]
        public void SimpleNavigationProperty()
        {
            var model = new Dictionary<string, object>()
            {
                { "Name", "Bill Gates"}
            };

            Assert.AreEqual(
                "Bill Gates",
                model.GetReference("Name"))
                ;
        }

        [TestMethod]
        public void CompositeNavigationProperty()
        {
            var model = new Dictionary<string, object>()
            {
                { "Name", "Bill Gates"}
            };

            Assert.AreEqual(
                "Bill Gates".Length,
                model.GetReference("Name.Length"))
                ;
        }

        [TestMethod]
        public void SimpleNavigationProperty_NullReturn()
        {
            var model = new Dictionary<string, object>()
            {
                { "Name", null }
            };

            Assert.AreEqual(
                null,
                model.GetReference("Name"))
                ;
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void SimpleNavigationProperty_NullReferenceException()
        {
            var model = new Dictionary<string, object>()
            {
                { "Name", null }
            };

            Assert.AreEqual(
                -10,
                model.GetReference("Name.Length"))
                ;
        }

    }
}
