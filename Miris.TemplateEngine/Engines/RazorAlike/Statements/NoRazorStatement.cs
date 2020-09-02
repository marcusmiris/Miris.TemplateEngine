using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Miris.TemplateEngine.Engines.RazorAlike.Statements
{
    public class NoRazorStatement
        : IStatement
    {

        #region ' IStatement '

        public bool Accept(string command)
        {
            return !command?.Contains("@") ?? true;
        }

        public (StringBuilder, StringReader) Execute(
            string currentLine,
            StringReader templateReader,
            StringBuilder output,
            IDictionary<string, object> model = null)
        {
            output.AppendLine(currentLine);
            return (output, templateReader);
        }

        #endregion
    }
}
