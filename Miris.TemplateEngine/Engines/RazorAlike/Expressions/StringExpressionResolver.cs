using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Miris.TemplateEngine.Engines.RazorAlike.Expressions
{
    public class StringExpressionResolver
        : IExpressionResolver
    {

        private const string doubleQuotation = @"((?<= "")"" | ""(?=""))";
        private const string notQuotation = @"[^""]";
        public readonly string regexPattern = $@"^""({ doubleQuotation }|{ notQuotation})*""$";

        #region ' IExpressionResolver '

        public bool Accept(string expression)
        {
            return Regex.IsMatch(expression, regexPattern);
        }

        public object Resolve(string expression, IDictionary<string, object> model)
        {
            if (!Accept(expression))
            {
                throw new Exception($"Invalid string expression: `{ expression }`.");
            }

            return expression
                .Substring(1, expression.Length - 2)   
                .Replace(@"""""", @"""")
                ;
        }

        #endregion
    }
}
