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
using static OfertaProcura.Enum.TipoFiltroEnum;

namespace OfertaProcura.Services.Service
{
    public class ProfissaoService : BaseService, IProfissaoService
    {
        private readonly IProfissaoRepository _profissaoRepository;
        private readonly IUsuarioService _usuarioService;
        private readonly IProfissionalRepository _profissionalRepository;
        private readonly IComentarioRepository _comentarioRepository;
        private readonly IUserLoggedExtensions _userLoggedExtensions;
        private readonly IUsuarioRepository _usuarioRepository;
        public ProfissaoService(INotificador notificador, IProfissaoRepository profissaoRepository,
                                IUsuarioService usuarioService, IProfissionalRepository profissionalRepository,
                                IComentarioRepository comentarioRepository,
                                IUserLoggedExtensions userLoggedExtensions,
                                IUsuarioRepository usuarioRepository) : base(notificador)
        {
            _profissaoRepository = profissaoRepository ?? throw new ArgumentNullException(nameof(profissaoRepository));
            _usuarioService = usuarioService ?? throw new ArgumentNullException(nameof(usuarioService));
            _profissionalRepository = profissionalRepository ?? throw new ArgumentNullException(nameof(profissionalRepository));
            _comentarioRepository = comentarioRepository ?? throw new ArgumentNullException(nameof(comentarioRepository));
            _userLoggedExtensions = userLoggedExtensions ?? throw new ArgumentNullException(nameof(userLoggedExtensions));
            _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
        }

        public ProfissaoViewModel InserirProfissao(ProfissaoImputModel profissaoImputModel)
        {
            return ConvertModelToViewModel(_profissaoRepository.Inserir(ConvertImputModelTOModel(profissaoImputModel)));
        }

        public ProfissaoViewModel ObterPorId(Guid id)
        {
            return ConvertModelToViewModel(_profissaoRepository.ObterPorId(id));
        }

        public List<AvaliacoesProfissionalViewModel> ObterAvaliacoesPorIdProfissional(Guid id)
        {
            List<AvaliacoesProfissionalViewModel> avaliacoesProfissionalViewModels = new List<AvaliacoesProfissionalViewModel>();
            var profissional = _profissionalRepository.ObterPorId(id);

            if (profissional != null)
            {
                var comentarios = _comentarioRepository.ObterComentarioPorIdPortifolio(profissional.Id_Portifolio.Value).OrderByDescending(x => x.Data_Criacao);

                foreach (var comentario in comentarios)
                {
                    avaliacoesProfissionalViewModels.Add(FormatAvaliacoesProfissional(comentario));
                }
            }

            return avaliacoesProfissionalViewModels;
        }

        public ProfissaoViewModel AtualizarProfissao(ProfissaoViewModel profissaoViewModel)
        {
            return ConvertModelToViewModel(_profissaoRepository.Atualizar(ConvertViewModelTOModel(profissaoViewModel)));
        }

        public ProfissaoViewModel RemoverProfissao(Guid id)
        {
           return ConvertModelToViewModel(_profissaoRepository.Deletar(id));
        }

        private AvaliacoesProfissionalViewModel FormatAvaliacoesProfissional(Comentario comentario)
        {
            return new AvaliacoesProfissionalViewModel
            {
                nome_cliente = comentario.RefCliente.Nome.Split(' ')[0],
                rating = comentario.Nota,
                comentario = comentario.Descricao,
                data_comentario = comentario.Data_Criacao.ToString("dd/MM/yyyy"),
                img_perfil = !string.IsNullOrEmpty(FileUtil.FindFile(comentario.RefCliente.Img_perfil)) ? FileUtil.FindFile(comentario.RefCliente.Img_perfil) : ImgPerfilVazia.imgPerfilVazia
            };
        }

        public ProfissaoProfissionalViewModel ObterProfissionaisPorProfissao(string nomeProfissao, List<string> filtroCidades, int tipoOrdenacao)
        {
            var usuario = _usuarioRepository.ObterPorId(_userLoggedExtensions.getId());

            ProfissaoProfissionalViewModel result = new ProfissaoProfissionalViewModel();

            ProfissaoProfissionalViewModel resultadoOrdenada = new ProfissaoProfissionalViewModel();

            var profissionais = _profissaoRepository.ObterProfissionais(nomeProfissao, filtroCidades);

            if (profissionais != null)
            {

                foreach (var profissional in profissionais.RefProfissional)
                {
                    result.profissionais.Add(ModelToConverterProfissionalProfissaoViewModel(profissional));
                }
            }

            switch (tipoOrdenacao)
            {
                case (int)TipoFiltro.Padrao:
                    result.profissionais = result.profissionais.OrderBy(x => x.nome_profissional).ToList();
                    break;

                case (int)TipoFiltro.Avaliacao:
                    result.profissionais = result.profissionais.OrderByDescending(x => x.nota_perfil).ToList();
                    break;

                default:
                    result.profissionais = result.profissionais.OrderBy(x => x.nome_profissional).ToList();
                    break;
            }

            result.profissionais.RemoveAll(x => x.id_usuario == usuario.Id);

            return result;
        }

        public List<OnlyNameProfissoesViewModel> ObterNomesProfissoes()
        {
            List<OnlyNameProfissoesViewModel> onlyNameProfissoesViewModels = new List<OnlyNameProfissoesViewModel>();

            var profissoes = _profissaoRepository.ObterTodos().ToList();

            foreach (var profissao in profissoes)
            {
                onlyNameProfissoesViewModels.Add(new OnlyNameProfissoesViewModel { nome = profissao.Nome_Profissao });
            }

            return onlyNameProfissoesViewModels;
        }

        public List<ProfissaoViewModel> ObterTodos()
        {
            List<ProfissaoViewModel> profissoesViewModel = new List<ProfissaoViewModel>();

            var profissoes = _profissaoRepository.ObterTodos().ToList();

            foreach(var profissao in profissoes)
            {
                profissoesViewModel.Add(ConvertModelToViewModel(profissao));
            }

            return profissoesViewModel;
        }

        private ProfissionalProfissaoViewModel ModelToConverterProfissionalProfissaoViewModel(Profissional profissional)
        {
            return new ProfissionalProfissaoViewModel
            {
                id_portifolio = profissional.Id_Portifolio.Value,
                cidade = profissional?.RefUsuario.Cidade,
                id_usuario = profissional.RefUsuario.Id,
                nome_profissional = profissional?.RefUsuario.Nome,
                nota_perfil = _usuarioService.ObterNotaPerfil(profissional.Id_Portifolio),
                id_profissional = profissional.Id,
                img_perfil = !string.IsNullOrEmpty(FileUtil.FindFile(profissional?.RefUsuario.Img_perfil)) ? FileUtil.FindFile(profissional?.RefUsuario.Img_perfil) : ImgPerfilVazia.imgPerfilVazia
            };
        }

        private ProfissaoViewModel ConvertModelToViewModel(Profissao profissao)
        {
            return new ProfissaoViewModel
            {
                id = profissao.Id,
                nome = profissao.Nome_Profissao
            };
        }

        private Profissao ConvertImputModelTOModel(ProfissaoImputModel profissaoImputModel)
        {
            return new Profissao
            {
                Nome_Profissao = profissaoImputModel.nome,
            };
        }

        private Profissao ConvertViewModelTOModel(ProfissaoViewModel profissaoViewModel)
        {
            return new Profissao
            {
                Id = profissaoViewModel.id,
                Nome_Profissao = profissaoViewModel.nome
            };
        }
    }
}
