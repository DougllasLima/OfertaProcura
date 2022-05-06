using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.Models
{
    public class ImagemPortifolio : BaseEntity
    {
        public Guid Id_Portifolio { get; set; }
        public Portifolio RefPortifolio { get; set; }
        public string Caminho_Imagem { get; set; }
    }
}
