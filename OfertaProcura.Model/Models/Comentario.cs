using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.Models
{
    public class Comentario : BaseEntity
    {
        public Guid Id_Cliente { get; set; }
        public virtual Usuario RefCliente { get; set; }
        public Guid Id_Portifolio { get; set; }
        public Portifolio RefPortifolio { get; set; } 
        public string Descricao { get; set; }
        public double Nota { get; set; }
        public DateTime Data_Atualizacao { get; set; }
    }
}
