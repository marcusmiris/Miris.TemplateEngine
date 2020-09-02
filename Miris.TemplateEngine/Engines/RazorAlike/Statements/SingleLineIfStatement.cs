using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Miris.TemplateEngine.Engines.RazorAlike.Statements
{
    public class SingleLineIfStatement
        : IStatement
    {

        private const string regexPattern = @"@[iI][fF]\s*\((?'condition'.*)\)\s*\{(?'statementsWhenTrue'.*)\}";

        #region ' IStatement '

        public bool Accept(string command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            return Regex.IsMatch(command, regexPattern);
        }

        public (StringBuilder, StringReader) Execute(
            string currentLine,
            StringReader templateReader,
            StringBuilder output,
            IDictionary<string, object> model = null)
        {
            if (templateReader == null) throw new ArgumentNullException(nameof(templateReader));
            if (output == null) throw new ArgumentNullException(nameof(output));
            if (model == null) model = new Dictionary<string, object>();

            var match = Regex.Match(currentLine, regexPattern);
            var condition = match.Groups["condition"].Value;
            var statementsWhenTrue = match.Groups["statementsWhenTrue"].Value.Trim();

            if (string.IsNullOrEmpty(condition)) throw new Exception("Missing if condition.");

            var conditionResult = EvaluateCondition(condition, model);
            if (conditionResult)
            {
                EvaluateStatements(statementsWhenTrue, model, output);
            }

            return (output, templateReader);
        }

        #endregion


        private bool EvaluateCondition(
            string condition,
            IDictionary<string, object> model)
        {
            if (condition == null) throw new ArgumentNullException(nameof(condition));

            // transformar este if num IStatement.
            if (condition.Trim().Equals("true", StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }

            if (condition.Trim().Equals("false", StringComparison.InvariantCultureIgnoreCase))
            {
                return false;
            }

            var match = Regex.Match(condition, @"(?'left'[^=<>!]*)(?'operator'[=<>!]*)(?'right'[^=<>!]*)");
            if (match.Success)
            {
                var engine = new RazorAlikeEngine();
                
                var leftExp = match.Groups["left"].Value.Trim();
                var rightExp = match.Groups["right"].Value.Trim();
                var op = match.Groups["operator"].Value;

                var leftObj = engine.ResolveExpression(leftExp, model);
                var rightObj = !string.IsNullOrEmpty(rightExp) ? engine.ResolveExpression(rightExp, model) : null;

                switch (op)
                {
                    case null:
                    case "":
                        if (!(leftObj is bool)) throw new InvalidCastException($"Expression `{ leftExp }` isn't a boolean variable.");
                        return (bool)leftObj;

                    case "==":
                        return leftObj.Equals(rightObj);

                    case "!=":
                        return !leftObj.Equals(rightObj);

                    default:
                        throw new NotImplementedException($"Operator not implemented: `{ op }`.");

                }
            }

            throw new NotImplementedException();
        }

        private void EvaluateStatements(
            string statements,
            IDictionary<string, object> model,
            StringBuilder output)
        {
            var engine = new RazorAlikeEngine();
            var result = engine.Run(statements, model);
            output.AppendLine(result);
        }
    }
}
