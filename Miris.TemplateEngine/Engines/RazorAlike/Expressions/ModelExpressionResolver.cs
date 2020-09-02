using Miris.TemplateEngine.Engines.RazorAlike.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Miris.TemplateEngine.Engines.RazorAlike.Expressions
{
    public class ModelExpressionResolver
        : IExpressionResolver
    {

        #region ' IExpressionResolver '

        public bool Accept(string expression)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));

            return Regex.IsMatch(expression, @"^\S*$");
        }

        public object Resolve(
            string expression,
            IDictionary<string, object> model)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            if (model == null) throw new ArgumentNullException(nameof(model));

            var result = model.GetReference(expression);

            return result;
        }

        #endregion
    }
}
