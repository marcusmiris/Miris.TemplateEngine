using System;
using System.Collections.Generic;

namespace Miris.TemplateEngine.Engines.RazorAlike.Expressions
{
    public class BooleanExpressionResolver
        : IExpressionResolver
    {

        #region ' IExpressionResolver '

        public bool Accept(string expression)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));

            return bool.TryParse(expression.Trim(), out _);
        }

        public object Resolve(
            string expression,
            IDictionary<string, object> model)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));

            return bool.Parse(expression.Trim());
        }

        #endregion
    }
}
