using System;
using System.Collections.Generic;

namespace Miris.TemplateEngine
{
    public interface ITemplateEngine
    {
        string Run(string template, object model);
        string Run(string template, IDictionary<string,object> model);
    }
}
