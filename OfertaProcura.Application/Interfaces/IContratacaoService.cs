using OfertaProcura.Models;
using OfertaProcura.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.Services.Interface
{
    public interface IContratacaoService
    {
        bool ContratarProfissional(ContratacaoImputModel contratacaoImputModel);
        List<ContratacaoViewModel> ConvertModelsToViewModels(List<Contratacao> contratacoes);
        List<ContratadosViewModel> ObterProfissionaisJaContratados();
    }
}
