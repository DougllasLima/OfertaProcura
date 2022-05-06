using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.Models
{
    public class Profissao : BaseEntity
    {
        public string Nome_Profissao { get; set; }
        public virtual List<Profissional> RefProfissional { get; set; }
    }
}
