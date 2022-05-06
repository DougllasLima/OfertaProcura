using OfertaProcura.Models;
using OfertaProcura.Notificacoes.Interface;
using OfertaProcura.Repositorys.Interface;
using OfertaProcura.Services.Interface;
using OfertaProcura.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.Services.Service
{
    public class ProfissionalService : BaseService, IProfissionalService
    {
        private readonly IProfissionalRepository _profissionalRepository;
        private readonly IPortifolioRepository _portifolioRepository;
        private readonly IProfissaoRepository _profissaoRepository;
        private readonly IPortifolioService _portifolioService;
        public ProfissionalService(INotificador notificador, IProfissionalRepository profissionalRepository,
                                   IPortifolioRepository portifolioRepository, IProfissaoRepository profissaoRepository, IPortifolioService portifolioService) : base(notificador)
        {
            _profissionalRepository = profissionalRepository ?? throw new ArgumentNullException(nameof(profissionalRepository));
            _portifolioRepository = portifolioRepository ?? throw new ArgumentNullException(nameof(portifolioRepository));
            _profissaoRepository = profissaoRepository ?? throw new ArgumentNullException(nameof(profissaoRepository));
            _portifolioService = portifolioService ?? throw new ArgumentNullException(nameof(portifolioService));
        }

        public ProfissionalViewModel InserirProfisional(ProfissionalImputModel profissionalImputModel)
        {
            var profissional = _profissionalRepository.Inserir(ConvertImputModelToModel(profissionalImputModel));

            var portifolio = _portifolioRepository.Inserir(new Portifolio { Descricao = profissionalImputModel.portifolio.descricao });

            profissional.Id_Portifolio = portifolio.Id;
            profissional.RefPortifolio = portifolio;

            var result = _profissionalRepository.Atualizar(profissional);

            return ConvertModelsToViewModel(result, portifolio);
        }

        public PortifolioViewModel AtualizarPerfilProfissional(AtualizarDescricaoPortifolioImputModel atualizarDescricaoPortifolioImputModel)
        {
            var portifolio = _portifolioRepository.ObterPortifolioPorId(atualizarDescricaoPortifolioImputModel.idPortifolio);

            if (portifolio != null)
            {
                portifolio.Descricao = atualizarDescricaoPortifolioImputModel.descricao;
                portifolio.Data_Atualizacao = DateTime.Now;
            }

            return _portifolioService.ConvertModelToViewModel(_portifolioRepository.Atualizar(portifolio));
        }

        private Profissional ConvertImputModelToModel(ProfissionalImputModel profissionalImputModel)
        {
            return new Profissional
            {
                CNPJ = profissionalImputModel.cnpj,
                Id_Profissao = profissionalImputModel.id_profissao,
            };
        }

        private ProfissionalViewModel ConvertModelsToViewModel(Profissional profissional, Portifolio portifolio)
        {
            return new ProfissionalViewModel
            {
                id_profissional = profissional.Id,
                id_profissao = profissional.Id_Profissao,
                cnpj = profissional.CNPJ,
                portifolio = new PortifolioViewModel 
                { 
                    id_portifolio = portifolio.Id,

                }
            };
        }
    }
}
