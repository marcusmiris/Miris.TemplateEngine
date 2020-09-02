using Miris.TemplateEngine.Engines.RazorAlike.Model;
using System;
using System.Collections.Generic;

namespace Miris.TemplateEngine.Engines.RazorAlike.Expressions
{
    public class ModelBindExpressionResolver
        : IExpressionResolver
    {

        #region ' IExpressionResolver '

        public bool Accept(string expression)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));

            return expression.TrimStart().StartsWith("@");
        }

        public object Resolve(
            string expression,
            IDictionary<string, object> model)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            if (model == null) throw new ArgumentNullException(nameof(model));

            var modelName = expression.Trim().TrimStart('@');

            var result = model.GetReference(modelName);

            return result;
        }

        #endregion
    }
}
