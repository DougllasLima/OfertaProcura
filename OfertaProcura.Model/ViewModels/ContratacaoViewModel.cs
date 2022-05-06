using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.ViewModels
{
    public class ContratacaoViewModel
    {
        public Guid id_profissional { get; set; }
        public string nome_profissional { get; set; }
        public string telefone_profissional { get; set; }
        public Guid id_contratante { get; set; }
        public string nome_contratante { get; set; }
    }

    public class ContratacaoImputModel
    {
        public Guid id_profissional { get; set; }
        public string email { get; set; }
    }

    public class ContratadosViewModel
    {
        public Guid id_portifolio { get; set; }
        public string profissao { get; set; }
        public string nome_profissional { get; set; }
        public bool avaliado { get; set; }
        public string img_perfil { get; set; }
    }
}
