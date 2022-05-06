using OfertaProcura.Models;
using OfertaProcura.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.Services.Interface
{
    public interface IPortifolioService
    {
        List<ImagemPortifolio> InserirProfissionalPortifolio(PortifolioProfissionalImputModel portifolioProfissionalImputModel);
        PortifolioViewModel ObterPortifolioPorId(Guid id);
        List<ImagemPortifolioViewModel> ObterImagensPerfilProfissional(Guid idProfissional);
        PortifolioViewModel ConvertModelToViewModel(Portifolio portifolio);
    }
}
