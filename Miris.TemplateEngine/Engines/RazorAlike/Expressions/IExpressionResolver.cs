using System.Collections.Generic;

namespace Miris.TemplateEngine.Engines.RazorAlike.Expressions
{
    public interface IExpressionResolver
    {
        bool Accept(string expression);

        object Resolve(
            string expression,
            IDictionary<string, object> model);
    }
}
