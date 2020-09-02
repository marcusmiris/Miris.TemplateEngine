using Miris.TemplateEngine.Engines.RazorAlike.CodeBlock;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Miris.TemplateEngine.Engines.RazorAlike.Statements
{
    public class BlockStatement
        : IStatement
    {

        #region ' IStatement '

        public bool Accept(string command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            return command.TrimStart().StartsWith("{");
        }

        public (StringBuilder, StringReader) Execute(
            string currentLine,
            StringReader templateReader,
            StringBuilder output,
            IDictionary<string, object> model = null)
        {
            var blockBody = BlockReader.ReadBlock(new StringReader(currentLine));

            var engine = new RazorAlikeEngine();
            var resolved = engine.Run(blockBody.ReadToEnd(), model);
            output.AppendLine(resolved);

            return (output, templateReader);
        }

        #endregion
    }
}
