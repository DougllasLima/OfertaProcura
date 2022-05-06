using OfertaProcura.Models;
using OfertaProcura.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.Services.Interface
{
    public interface IImagemPortifolioService
    {
        List<ImagemPortifolio> ConvertImputModelToModel(ProfissionalImagemPortifolioImputModel imagemPortifolioImputModel, Guid idPortifolio);
        List<ImagemPortifolioViewModel> ObterImagensPortifolio(Guid? idPortifolio);
        ImagemPortifolioViewModel AtualizarImagemPortifolio(AtualizarImagemPortifolioImputModel atualizarImagemPortifolioImputModel);
        ImagemPortifolioViewModel AdicionarImagemPortifolio(ImagemPortifolioInputModel imagemPortifolioInputModel);
    }
}
