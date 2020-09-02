using Miris.TemplateEngine.Engines.RazorAlike.CodeBlock;
using Miris.TemplateEngine.Engines.RazorAlike.Exceptions;
using Miris.TemplateEngine.Engines.RazorAlike.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Miris.TemplateEngine.Engines.RazorAlike.Statements
{
    public class MultiLineForeachStatement
        : IStatement
    {

        #region ' IStatement '

        public bool Accept(string command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            return command.Trim().StartsWith("@foreach");
        }

        public (StringBuilder, StringReader) Execute(
            string currentLine,
            StringReader templateReader,
            StringBuilder output,
            IDictionary<string, object> model = null)
        {
            if (currentLine == null) throw new ArgumentNullException(nameof(currentLine));

            var forEachDetails = Regex.Match(currentLine, @"@foreach\s*\((?'criteria'.*)\)");
            if (!forEachDetails.Success)
            {
                throw new MalformedException($"Malformed foreach statement: `{ currentLine }`.");
            }

            var loopCriteria = forEachDetails.Groups["criteria"].Value.Trim();
            if (!loopCriteria.StartsWith("var")) throw new MalformedException("Missing keyword `var`");

            var variableRefs = Regex.Match(loopCriteria, @"var\s*(?'varName'[\w]+)\s*in\s*(?'collection'[\w.]+)");
            if (!variableRefs.Success)
            {
                throw new MalformedException(loopCriteria);
            }

            var varName = variableRefs.Groups["varName"].Value;
            var collectionNavProps = variableRefs.Groups["collection"].Value;

            // resolve collection
            if (!(model.GetReference(collectionNavProps) is ICollection collection))
            {
                throw new Exception($"`{ collectionNavProps }` isn't a valid collection");
            }

            // look for the loop statements.
            var forBlock = BlockReader.ReadBlock(templateReader).ReadToEnd();

            var engine = new RazorAlikeEngine();
            foreach (var item in collection)
            {
                var iteratorModel = model.Clone();
                iteratorModel.DefineVariable(varName, item);

                var iterationOutput = engine.Run(forBlock, iteratorModel);

                output.Append(iterationOutput.ToString());
            }

            return (output, templateReader);
        }

        #endregion

        
    }
}
