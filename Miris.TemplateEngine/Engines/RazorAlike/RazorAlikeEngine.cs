using Miris.TemplateEngine.Engines.RazorAlike.Expressions;
using Miris.TemplateEngine.Engines.RazorAlike.Statements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Miris.TemplateEngine.Engines.RazorAlike
{
    public class RazorAlikeEngine
        : ITemplateEngine
    {

        #region ' ITemplateEngine '

        public string Run(
            string template,
            object model)
        {
            if (template == null) throw new ArgumentNullException(nameof(template));
            if (model == null) throw new ArgumentNullException(nameof(model));

            var modelAsDic = model.GetType()
                .GetProperties()
                .ToDictionary(_ => _.Name, _ => _.GetValue(model))
                ;

            return Run(template, modelAsDic);
        }

        public string Run(
            string template,
            IDictionary<string, object> model)
        {
            if (template == null) throw new ArgumentNullException(nameof(template));
            if (model == null) throw new ArgumentNullException(nameof(model));

            var output = new StringBuilder();
            var reader = new StringReader(template);

            var currentLine = reader.ReadLine();
            while (currentLine != null)
            {
                var statement = Statements.FirstOrDefault(_ => _.Accept(currentLine));
                if (statement == null)
                {
                    throw new Exception($"Not recognized statement: `{ statement }`.");
                }
                
                (output, reader) = statement.Execute(currentLine, reader, output, model);

                currentLine = reader.ReadLine();
            }

            return output.ToString().TrimEnd();
        }

        #endregion

        public object ResolveExpression(
            string expression,
            IDictionary<string, object> model)
        {
            var resolver = ExpressionResolvers.FirstOrDefault(_ => _.Accept(expression));
            if (resolver != null)
            {
                return resolver.Resolve(expression, model);
            }

            throw new Exception($"not recognized expression: `{ expression }`.");
        }

        List<IExpressionResolver> ExpressionResolvers = new List<IExpressionResolver>()
        {
            new ModelBindExpressionResolver(),
            new EqualityExpressionResolver(),
            new IntExpressionResolver(),
            new StringExpressionResolver(),
            new BooleanExpressionResolver(),
            new ModelExpressionResolver(),
        };

        List<IStatement> Statements = new List<IStatement>()
        {
            new SingleLineIfStatement(),
            new MultiLineForeachStatement(),
            new BindVariableStatement(),
            new BlockStatement(),
            new NoRazorStatement(),
        };
    }
}
