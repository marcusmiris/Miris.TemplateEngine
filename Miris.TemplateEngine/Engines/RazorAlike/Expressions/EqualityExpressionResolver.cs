using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Miris.TemplateEngine.Engines.RazorAlike.Expressions
{
    public class EqualityExpressionResolver
        : IExpressionResolver
    {

        private const string regexPattern = @"^(?'left'\S*)\s*==\s*(?'right'\S*)$";

        #region ' IExpressionResolver '

        public bool Accept(string expression)
        {
            return Regex.IsMatch(expression, regexPattern);
        }

        public object Resolve(
            string expression,
            IDictionary<string, object> model)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            if (!Accept(expression)) throw new Exception($"Invalid equality expression: `{ expression }`.");

            var match = Regex.Match(expression, regexPattern);

            var leftExp = match.Groups["left"].Value;
            var rightExp = match.Groups["right"].Value;

            var engine = new RazorAlikeEngine();

            var left = engine.ResolveExpression(leftExp, model);
            var right = engine.ResolveExpression(rightExp, model);

            return left.Equals(right);
        }

        #endregion
    }
}
