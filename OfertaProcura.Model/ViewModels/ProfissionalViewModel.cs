using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.ViewModels
{
    public class ProfissionalViewModel
    {
        public string cnpj { get; set; }
        public Guid? id_profissional { get; set; }
        public Guid? id_profissao { get; set; }
        public string nome_profissao { get; set; }
        public PortifolioViewModel portifolio { get; set; }
    }

    public class ProfissionalImputModel
    {
        public string cnpj { get; set; }
        public Guid id_profissao { get; set; }
        public PortifolioProfissionalImputModel portifolio { get; set; }
    }

    public class ProfissionalProfissaoImputModel
    {
        public string nome_profissional { get; set; }
        public double? nota_perfil { get; set; }
        public Guid id_usuario { get; set; }
    }

    public class ProfissionalProfissaoViewModel
    {
        public string nome_profissional { get; set; }
        public string cidade { get; set; }
        public double? nota_perfil { get; set; }
        public Guid id_usuario { get; set; }
        public Guid id_portifolio { get; set; }
        public Guid id_profissional { get; set; }
        public string img_perfil { get; set; }
    }

    public class InsertProfissionalInputModel
    {
        public string nomeProfissao { get; set; }
        public string CNPJ { get; set; }
    }
}
