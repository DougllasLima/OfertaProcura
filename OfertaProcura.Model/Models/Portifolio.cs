using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.Models
{
    public class Portifolio : BaseEntity
    {
        public string Descricao { get; set; }
        public DateTime Data_Atualizacao { get; set; }
        public List<Comentario> RefComentarios { get; set; }
        public virtual List<ImagemPortifolio> RefImagemPortifolios { get; set; }
        public virtual Profissional RefProfissional { get; set; }
    }
}
