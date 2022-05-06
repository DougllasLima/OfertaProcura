using OfertaProcura.Models;
using OfertaProcura.Notificacoes.Interface;
using OfertaProcura.Repositorys.Interface;
using OfertaProcura.Services.Interface;
using OfertaProcura.Utils;
using OfertaProcura.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OfertaProcura.Services.Service
{
    public class PortifolioService : BaseService, IPortifolioService
    {
        private readonly IPortifolioRepository _portifolioRepository;
        private readonly IImagemPortifolioService _imagemPortifolioService;
        private readonly IComentarioService _comentarioService;
        private readonly IProfissionalRepository _profissionalRepository;
        public PortifolioService(INotificador notificador, IPortifolioRepository portifolioRepository,
                                 IImagemPortifolioService imagemPortifolioService, IComentarioService comentarioService,
                                 IProfissionalRepository profissionalRepository) : base(notificador)
        {
            _portifolioRepository = portifolioRepository ?? throw new ArgumentNullException(nameof(portifolioRepository));
            _imagemPortifolioService = imagemPortifolioService ?? throw new ArgumentNullException(nameof(imagemPortifolioService));
            _comentarioService = comentarioService ?? throw new ArgumentNullException(nameof(comentarioService));
            _profissionalRepository = profissionalRepository ?? throw new ArgumentNullException(nameof(profissionalRepository));
        }

        public PortifolioViewModel ObterPortifolioPorId(Guid idProfissional)
        {
            var portifolio = _portifolioRepository.ObterPorId(idProfissional);

            return ConvertModelToViewModel(portifolio);
        }

        public List<ImagemPortifolio> InserirProfissionalPortifolio(PortifolioProfissionalImputModel portifolioProfissionalImputModel)
        {
            var portifolio = ConvertImputModelToModel(portifolioProfissionalImputModel);

            portifolio.RefImagemPortifolios = _imagemPortifolioService.ConvertImputModelToModel(portifolioProfissionalImputModel.imagemPortifolio, portifolio.Id);

            return portifolio.RefImagemPortifolios;
        }

        private Portifolio ConvertImputModelToModel(PortifolioProfissionalImputModel portifolioImputModel)
        {
            return new Portifolio
            {
                Descricao = portifolioImputModel.descricao,
                Data_Atualizacao = DateTime.Now
            };
        }

        public List<ImagemPortifolioViewModel> ObterImagensPerfilProfissional(Guid idProfissional)
        {
            var profissional = _profissionalRepository.ObterPorId(idProfissional);

            return _imagemPortifolioService.ObterImagensPortifolio(profissional.Id_Portifolio);
        }

        public PortifolioViewModel ConvertModelToViewModel(Portifolio portifolio)
        {
            return new PortifolioViewModel
            {
                id_profissional = portifolio.RefProfissional.Id,
                id_portifolio = portifolio.Id,
                nome = portifolio.RefProfissional.RefUsuario.Nome,
                descricao = portifolio.Descricao,
                email = portifolio.RefProfissional.RefUsuario.Email,
                contratacoes = portifolio?.RefProfissional?.RefContratacoes.Count == 0 ? 0 : portifolio?.RefProfissional?.RefContratacoes.Count,
                rating = ObterNotaPerfil(portifolio.Id).GetValueOrDefault(),
                img_perfil = !string.IsNullOrEmpty(portifolio.RefProfissional.RefUsuario.Img_perfil) ? FileUtil.FindFile(portifolio.RefProfissional.RefUsuario.Img_perfil) : ImgPerfilVazia.imgPerfilVazia

            };
        }

        private double? ObterNotaPerfil(Guid? idPortifolio)
        {
            if (idPortifolio != null)
            {
                var comentarios = _comentarioService.ObterTodosComentariosPorIdPortifolio(idPortifolio.Value);

                if (comentarios.Count <= 0)
                    return 0;

                var nota = (comentarios.Sum(x => x.nota) / comentarios.Count());

                return nota <= 0 ? 0 : Math.Round(nota, 1);
            }

            return null;
        }
    }
}
