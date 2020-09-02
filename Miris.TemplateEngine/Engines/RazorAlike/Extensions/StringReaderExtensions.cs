using System;
using System.IO;

namespace Miris.TemplateEngine.Engines.RazorAlike.Extensions
{
    public static class StringReaderExtensions
    {

        public static bool TryRead(
            this StringReader reader,
            out char? character)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));

            var n = reader.Read();
            character = n != -1
                ? (char)n 
                : (char?)null;

            return n != -1;
        }

    }
}
