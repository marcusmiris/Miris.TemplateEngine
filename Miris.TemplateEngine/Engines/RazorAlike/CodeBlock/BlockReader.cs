using Miris.TemplateEngine.Engines.RazorAlike.Extensions;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Miris.TemplateEngine.Engines.RazorAlike.CodeBlock
{
    public static class BlockReader
    {
        public static StringReader ReadBlock(
            StringReader templateReader)
        {
            if (templateReader == null) throw new ArgumentNullException(nameof(templateReader));

            // check if next valid char is '{'
            var found = templateReader.TryMoveToNexValidChar(out var readed);
            if (!found || readed.Value != '{')
            {
                throw new Exception("Missing opening `{` char.");
            }

            var output = new StringBuilder();

            // look for the closing `}`
            // ps: it's possible to have others `{`
            bool foundClosingBraces = false;
            uint ident = 0;

            while (!foundClosingBraces && templateReader.TryRead(out readed))
            {
                switch (readed.Value)
                {
                    case '{':
                        ident++;
                        output.Append(readed.Value);
                        break;

                    case '}' when ident > 0:
                        ident--;
                        output.Append(readed.Value);
                        break;

                    case '}' when ident == 0:
                        foundClosingBraces = true;
                        break;

                    default:
                        output.Append(readed.Value);
                        break;

                }
            }


            if (!foundClosingBraces)
            {
                throw new Exception("Missing enclosing braces `}`.");
            }

            return new StringReader(output.ToString());
        }


        #region ' internal '

        private static bool TryMoveToNexValidChar(
            this StringReader templateReader,
            out char? character)
        {
            if (templateReader == null) throw new ArgumentNullException(nameof(templateReader));

            bool validChar(char c) => !" \r\n\t".ToCharArray().Contains(c);

            do
            {
                if (!templateReader.TryRead(out character))
                {
                    return false;
                }
            }
            while (!validChar(character.Value));

            return true;
        }

        #endregion



    }
}
