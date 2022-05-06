using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.ViewModels
{
    public class ComentarioViewModel
    {
        public Guid id_usuario { get; set; }
        public string nome_usuario { get; set; }
        public string descricao { get; set; }
        public double nota { get; set; }
        public string data_comentario { get; set; }
    }

    public class ComentarioImputModel
    {
        public Guid id_portifolio { get; set; }
        public string comentario { get; set; }
        public double nota { get; set; }
    }
}
