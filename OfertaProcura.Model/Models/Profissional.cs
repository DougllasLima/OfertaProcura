using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.Models
{
    public class Profissional : BaseEntity
    {
        public Guid? Id_Profissao { get; set; }
        public Profissao RefProfissao { get; set; }
        public string CNPJ { get; set; }
        public List<Contratacao> RefContratacoes { get; set; }
        public Usuario RefUsuario { get; set; }
        public Guid? Id_Portifolio { get; set; }
        public virtual Portifolio RefPortifolio { get; set; }
    }
}
