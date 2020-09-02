using Microsoft.VisualStudio.TestTools.UnitTesting;
using Miris.TemplateEngine.Engines.RazorAlike;
using System.Collections.Generic;

namespace Miris.TemplateEngine.Tests
{

    [TestClass]
    public class RazorAlikeEngineTest
    {

        [TestMethod]
        public void SimplePositiveCase()
        {

            var template = @"
Hello @user.name!

@if (@user.showId == true) { Your ID is: @user.id. }

Mail addresses: 
@foreach(var email in user.Emails) 
{
    @email.Value
}
";
            var model = new {
                user = new {
                    name = "Joseph Climber",
                    id = 12345,
                    showId = true,
                    Emails = new List<object>()
                    {
                        new { Value = "foo@bar.com" },
                        new { Value = "bar@foo.com" },
                    }
                }
            };

            var engine = new RazorAlikeEngine();
            var result = engine.Run(template, model);

            Assert.AreEqual(
                @"
Hello Joseph Climber!

Your ID is: 12345.

Mail addresses: 

    foo@bar.com
    bar@foo.com", result);
        }

    }
}
