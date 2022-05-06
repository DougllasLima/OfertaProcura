using OfertaProcura.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.Services.Interface
{
    public interface IProfissaoService
    {
        ProfissaoViewModel InserirProfissao(ProfissaoImputModel profissaoImputModel);
        List<ProfissaoViewModel> ObterTodos();
        ProfissaoViewModel ObterPorId(Guid id);
        ProfissaoViewModel AtualizarProfissao(ProfissaoViewModel profissaoViewModel);
        ProfissaoViewModel RemoverProfissao(Guid id);
        ProfissaoProfissionalViewModel ObterProfissionaisPorProfissao(string nomeProfissao, List<string> filtroCidades, int tipoOrdenacao);
        List<AvaliacoesProfissionalViewModel> ObterAvaliacoesPorIdProfissional(Guid id);
        List<OnlyNameProfissoesViewModel> ObterNomesProfissoes();
    }
}
