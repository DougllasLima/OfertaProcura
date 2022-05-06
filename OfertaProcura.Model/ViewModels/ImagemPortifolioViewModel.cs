using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.ViewModels
{
    public class ImagemPortifolioViewModel
    {
        public Guid idImagemPortifolio { get; set; }
        public string base64 { get; set; }
    }

    public class ImagemPortifolioInputModel
    {
        public Guid id_portifolio { get; set; }
        public string base64 { get; set; }
    }

    public class ProfissionalImagemPortifolioImputModel
    {
        public List<string> base64 { get; set; }
    }

    public class AtualizarImagemPortifolioImputModel
    {
        public Guid idImagemPortifolio { get; set; }
        public string base64 { get; set; }
        public bool isPerfil { get; set; }
    }

    public class AtualizarDescricaoPortifolioImputModel
    {
        public Guid idPortifolio { get; set; }
        public string descricao { get; set; }
    }
}
