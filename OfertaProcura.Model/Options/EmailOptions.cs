using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.Models.Options
{
    public class EmailOptions
    {
        public TemplateEmail[] EmailTemplates { get; set; }
    }

    public class TemplateEmail
    {
        public string Template { get; set; }
        public string ArquivoTemplate { get; set; }
    }
}
