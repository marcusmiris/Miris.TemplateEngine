using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Miris.TemplateEngine.Engines.RazorAlike.Statements
{
    public interface IStatement
    {
        /// <summary>
        ///     Define if the current statement is valid to execute the candidate command.
        /// </summary>
        bool Accept(string command);

        (StringBuilder, StringReader) Execute(
            string currentLine,
            StringReader templateReader,
            StringBuilder output,
            IDictionary<string, object> model = null);
    }
}
