using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.ViewModels
{
    public class ProfissaoViewModel
    {
        public Guid id { get; set; }
        public string nome { get; set; }
    }

    public class ProfissaoImputModel
    {
        public string nome { get; set; }
    }

    public class ProfissaoProfissionalViewModel
    {
        public ProfissaoProfissionalViewModel()
        {
            profissionais = new List<ProfissionalProfissaoViewModel>();
        }

        public List<ProfissionalProfissaoViewModel> profissionais { get; set; }
    }

    public class AvaliacoesProfissionalViewModel
    {
        public string nome_cliente { get; set; }
        public double rating { get; set; }
        public string data_comentario { get; set; }
        public string comentario { get; set; }
        public string img_perfil { get; set; }
    }

    public class OnlyNameProfissoesViewModel
    {
        public string nome { get; set; }
    }

    public class ObterProfissionalInputModel
    {
        ObterProfissionalInputModel()
        {
            filtroCidades = new List<string>();
        }

        public string nomeProfissao { get; set; }
        public List<string> filtroCidades { get; set; }
        public int tipoOrdenacao { get; set; }
    }
}
