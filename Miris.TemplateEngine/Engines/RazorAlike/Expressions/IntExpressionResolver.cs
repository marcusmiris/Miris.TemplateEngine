using System;
using System.Collections.Generic;

namespace Miris.TemplateEngine.Engines.RazorAlike.Expressions
{
    public class IntExpressionResolver
        : IExpressionResolver
    {

        #region ' IExpressionResolver '
        
        public bool Accept(string expression)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));

            return int.TryParse(expression.Trim(), out _);
        }

        public object Resolve(
            string expression,
            IDictionary<string, object> model)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));

            return int.Parse(expression.Trim());
        }

        #endregion
    }
}
