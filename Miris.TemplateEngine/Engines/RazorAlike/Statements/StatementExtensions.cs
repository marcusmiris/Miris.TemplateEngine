using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Miris.TemplateEngine.Engines.RazorAlike.Statements
{
    public static class StatementExtensions
    {
        public static (StringBuilder, StringReader) Execute(
            this IStatement statement,
            StringReader stringReader,
            StringBuilder output,
            IDictionary<string, object> model = null)
        {
            if (statement == null) throw new ArgumentNullException(nameof(statement));
            if (stringReader == null) throw new ArgumentNullException(nameof(stringReader));

            var currentLine = stringReader.ReadLine();

            return statement.Execute(currentLine, stringReader, output, model);
        }

    }
}
