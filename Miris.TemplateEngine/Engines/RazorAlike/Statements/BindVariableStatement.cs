using Miris.TemplateEngine.Engines.RazorAlike.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Miris.TemplateEngine.Engines.RazorAlike.Statements
{
    public class BindVariableStatement
        : IStatement
    {

        #region ' IStatement '

        public bool Accept(string command)
        {
            return command?.Contains("@") ?? true;
        }

        public (StringBuilder, StringReader) Execute(
            string currentLine,
            StringReader templateReader,
            StringBuilder output,
            IDictionary<string, object> model = null)
        {
            if (currentLine == null) throw new ArgumentNullException(nameof(currentLine));
            if (model == null) model = new Dictionary<string, object>();

            var references = Regex.Matches(currentLine, @"@[\w.]*\b")
                .Cast<Match>()
                .Where(_ => _.Success)
                .Select(_ => _.Value)
                .ToList();

            // bind
            foreach (var reference in references)
            {
                var value = model.GetReference(reference).ToString();
                currentLine = currentLine.Replace(reference, value);
            }

            output.AppendLine(currentLine);

            return (output, templateReader);
        }

        #endregion
    }
}
