using OfertaProcura.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.Services.Interface
{
    public interface IProfissionalService
    {
        ProfissionalViewModel InserirProfisional(ProfissionalImputModel profissionalImputModel);
        PortifolioViewModel AtualizarPerfilProfissional(AtualizarDescricaoPortifolioImputModel atualizarDescricaoPortifolioImputModel);
    }
}
