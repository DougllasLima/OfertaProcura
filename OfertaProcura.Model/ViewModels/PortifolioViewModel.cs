using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.ViewModels
{
    public class PortifolioViewModel
    {
        public Guid id_profissional { get; set; }
        public Guid? id_portifolio { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public string data_atualizacao { get; set; }
        public double rating { get; set; }
        public string img_perfil { get; set; }
        public int? contratacoes { get; set; }
        public string email { get; set; }
        public List<ImagemPortifolioViewModel> imagens_portifolio { get; set; }
    }

    public class PortifolioImputModel
    {
        public Guid id_profissional { get; set; }
        public string descricao { get; set; }
    }

    public class PortifolioProfissionalImputModel
    {
        public string descricao { get; set; }
        public ProfissionalImagemPortifolioImputModel imagemPortifolio { get; set; }
    }
}
