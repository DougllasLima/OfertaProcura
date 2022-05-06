using OfertaProcura.Extensao;
using OfertaProcura.Models;
using OfertaProcura.Notificacoes.Interface;
using OfertaProcura.Repositorys.Interface;
using OfertaProcura.Services.Interface;
using OfertaProcura.Utils;
using OfertaProcura.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.Services.Service
{
    public class ContratacaoService : BaseService, IContratacaoService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IProfissionalRepository _profissionalRepository;
        private readonly IUserLoggedExtensions _userLoggedExtensions;
        private readonly IContratadoEmailService _contratadoEmailService;
        private readonly IProfissaoRepository _profissaoRepository;
        private readonly IContratacaoRepository _contratacaoRepository;

        public ContratacaoService(INotificador notificador, IUsuarioRepository usuarioRepository, IProfissionalRepository profissionalRepository,
                                  IUserLoggedExtensions userLoggedExtensions, IContratadoEmailService contratadoEmailService, IProfissaoRepository profissaoRepository,
                                  IContratacaoRepository contratacaoRepository) : base(notificador)
        {
            _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
            _profissionalRepository = profissionalRepository ?? throw new ArgumentNullException(nameof(profissionalRepository));
            _userLoggedExtensions = userLoggedExtensions ?? throw new ArgumentNullException(nameof(userLoggedExtensions));
            _contratadoEmailService = contratadoEmailService ?? throw new ArgumentNullException(nameof(contratadoEmailService));
            _profissaoRepository = profissaoRepository ?? throw new ArgumentNullException(nameof(profissaoRepository));
            _contratacaoRepository = contratacaoRepository ?? throw new ArgumentNullException(nameof(contratacaoRepository));
        }

        public bool ContratarProfissional(ContratacaoImputModel contratacaoImputModel)
        {
            var idUsuarioContratante = _userLoggedExtensions.getId();

            var usuarioContratante = _usuarioRepository.ObterPorId(idUsuarioContratante);

            var profissionalContratado = _profissionalRepository.ObterPorId(contratacaoImputModel.id_profissional);

            var entidade = ConvertViewModelToModel(contratacaoImputModel, idUsuarioContratante);

            var contratacao = _contratacaoRepository.Inserir(entidade);

            usuarioContratante.RefContratacoes.Add(contratacao);

            var usuarioAtualizado = _usuarioRepository.Atualizar(usuarioContratante);

            if (usuarioAtualizado != null)
            {
                Util.EnviarEmail(contratacaoImputModel.email, "", "Informações OfertaProcura", _contratadoEmailService.ObterEmailContrato(usuarioContratante, profissionalContratado));

                return true;
            }

            return false;
        }


        public List<ContratacaoViewModel> ConvertModelsToViewModels(List<Contratacao> contratacoes)
        {
            List<ContratacaoViewModel> contratados = new List<ContratacaoViewModel>();

            if (contratacoes != null)
            {
                foreach (var contratado in contratacoes)
                {
                    contratados.Add(new ContratacaoViewModel
                    {
                        id_profissional = contratado.Id_Contratado,
                        nome_profissional = contratado.RefContratado?.RefUsuario?.Nome,
                        telefone_profissional = contratado.RefContratado?.RefUsuario?.Numero_Celular,
                        id_contratante = contratado.Id_Contratante,
                        nome_contratante = contratado.RefContratante?.Nome
                    });
                }
            }

            return contratados;
        }

        public List<ContratadosViewModel> ObterProfissionaisJaContratados()
        {
            List<ContratadosViewModel> contratadosViewModel = new List<ContratadosViewModel>();

            var usuario = _usuarioRepository.ObterPorId(_userLoggedExtensions.getId());

            foreach(var contratacao in usuario.RefContratacoes)
            {
                var contracacoes = new ContratadosViewModel
                {
                    id_portifolio = contratacao.RefContratado.Id_Portifolio.GetValueOrDefault(),
                    profissao = _profissaoRepository.ObterPorId(contratacao.RefContratado.Id_Profissao.GetValueOrDefault()).Nome_Profissao,
                    nome_profissional = contratacao.RefContratado.RefUsuario.Nome.Split(" ")[0],
                    avaliado = usuario.RefComentario.Any(x => x.Id_Portifolio == contratacao.RefContratado.RefPortifolio.Id),
                    img_perfil = !string.IsNullOrEmpty(FileUtil.FindFile(contratacao.RefContratado.RefUsuario.Img_perfil)) ? FileUtil.FindFile(contratacao.RefContratado.RefUsuario.Img_perfil) 
                                                                                                                           : ImgPerfilVazia.imgPerfilVazia
                };

                contratadosViewModel.Add(contracacoes);
            }

            return contratadosViewModel;
        }

        private ContratacaoViewModel ConvertModelToViewModel(Contratacao contratacao)
        {
            return new ContratacaoViewModel
            {
                id_profissional = contratacao.Id_Contratado,
                id_contratante = contratacao.Id_Contratante
            };
        }

        private Contratacao ConvertViewModelToModel(ContratacaoImputModel contratacaoImputModel, Guid idContratante)
        {
            return new Contratacao
            {
                Id = new Guid(),
                Id_Contratado = contratacaoImputModel.id_profissional,
                Id_Contratante = idContratante,
            };
        }
    }
}
