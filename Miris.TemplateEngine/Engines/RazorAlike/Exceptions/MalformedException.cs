using System;

namespace Miris.TemplateEngine.Engines.RazorAlike.Exceptions
{
    public class MalformedException
        : Exception
    {

        public MalformedException(): base()
        {

        }

        public MalformedException(string message)
            : base(message)
        {

        }
    }
}
