using Microsoft.Extensions.Configuration;
using OfertaProcura.Extensao;
using OfertaProcura.Jwt;
using OfertaProcura.Models;
using OfertaProcura.Notificacoes.Interface;
using OfertaProcura.Repositorys.Interface;
using OfertaProcura.Services.Interface;
using OfertaProcura.Utils;
using OfertaProcura.ViewModels;
using System;
using System.Globalization;
using System.Linq;

namespace OfertaProcura.Services.Service
{
    public class UsuarioService : BaseService, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IProfissionalService _profissionalService;
        private readonly IProfissionalRepository _profissionalRepository;
        private readonly IComentarioService _comentarioService;
        private readonly IContratacaoService _contratacaoService;
        private readonly IUserLoggedExtensions _userLoggedExtensions;
        private readonly IImagemPortifolioService _imagemPortifolioService;
        private readonly IProfissaoRepository _profissaoRepository;
        private readonly IConfiguration _configuration;
        public UsuarioService(INotificador notificador, IUsuarioRepository usuarioRepository, IProfissionalService profissionalService,
                              IProfissionalRepository profissionalRepository, IComentarioService comentarioService, IContratacaoService contratacaoService,
                              IUserLoggedExtensions userLoggedExtensions, IImagemPortifolioService imagemPortifolioService,
                              IProfissaoRepository profissaoRepository, IConfiguration configuration) : base(notificador)
        {
            _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
            _profissionalService = profissionalService ?? throw new ArgumentNullException(nameof(profissionalService));
            _profissionalRepository = profissionalRepository ?? throw new ArgumentNullException(nameof(profissionalRepository));
            _comentarioService = comentarioService ?? throw new ArgumentNullException(nameof(comentarioService));
            _contratacaoService = contratacaoService ?? throw new ArgumentNullException(nameof(contratacaoService));
            _userLoggedExtensions = userLoggedExtensions ?? throw new ArgumentNullException(nameof(userLoggedExtensions));
            _imagemPortifolioService = imagemPortifolioService ?? throw new ArgumentNullException(nameof(imagemPortifolioService));
            _profissaoRepository = profissaoRepository ?? throw new ArgumentNullException(nameof(profissaoRepository));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public AutenticarViewModel AutenticarUsuario(AutenticarImputModel authenticarImputModel)
        {
            AutenticarViewModel result = new AutenticarViewModel();

            var usuario = _usuarioRepository.ObterUsuarioPorCpf(authenticarImputModel.cpf);

            if(usuario == null)
                NotificarErro("Usuário não encontrado.");

            if (usuario != null)
            {
                if (ValidarSenha(authenticarImputModel.senha, usuario.Senha))
                {
                    result.token = TokenService.GenerateToken(usuario);
                    result.nome = usuario.Nome.Split(" ")[0];
                    result.email = usuario.Email;
                    return result;
                }

                NotificarErro("Senha incorreta.");
            }

            return result;
        }

        public UsuarioViewModel InserirProfissionalComProfissao(InsertProfissionalInputModel insertProfissionalInputModel)
        {
            var usuario = _usuarioRepository.ObterPorId(_userLoggedExtensions.getId());

            if (usuario != null)
            {

                var profissao = _profissaoRepository.ObterProfissaoPorNome(insertProfissionalInputModel.nomeProfissao);

                var profissional = _profissionalService.InserirProfisional(new ProfissionalImputModel
                {
                    id_profissao = profissao.Id,
                    cnpj = insertProfissionalInputModel.CNPJ,
                    portifolio = new PortifolioProfissionalImputModel
                    {
                        descricao = string.Empty
                    }
                });

                usuario.Id_Profissional = profissional.id_profissional;

                usuario.RefProfissional = _profissionalRepository.ObterPorId(profissional.id_profissional.Value);

                usuario.RefProfissional.Id_Portifolio = profissional.portifolio.id_portifolio;

                _usuarioRepository.Atualizar(usuario);

                return ConvertModelToViewModel(_usuarioRepository.ObterPorId(usuario.Id));
            }

            return null;
        }

        public UsuarioViewModel InserirUsuario(UsuarioImputModel usuarioImputModel)
        {
            if(usuarioImputModel.uf.Equals("UF"))
            {
                NotificarErro("Por favor, adicione a UF");
                return null;
            }

            return ConvertModelToViewModel(_usuarioRepository.Inserir(ConvertImputModelTOModel(usuarioImputModel)));
        }

        public UsuarioViewModel AtualizarUsuario(UsuarioUpdateInputModel usuarioUpdateInputModel)
        {
            var user = _usuarioRepository.ObterPorId(usuarioUpdateInputModel.id);

            var usuarioAtualizado = _usuarioRepository.Atualizar(AtualizarUsuarioViewModelToModel(usuarioUpdateInputModel, user));

            return ConvertModelToViewModel(usuarioAtualizado);
        }

        public UsuarioViewModel AtualizarImagemPerfilUsuario(string base64)
        {
            var user = _usuarioRepository.ObterPorId(_userLoggedExtensions.getId());

            var basePath = _configuration["Params:BasePathFiles"];

            if(!string.IsNullOrEmpty(user.Img_perfil))
            {
                FileUtil.DeleteFile(user.Img_perfil);
            }

            user.Img_perfil = FileUtil.SaveFileImgProfile(base64, basePath, user.Id);

            var usuarioAtualizado = _usuarioRepository.Atualizar(user);

            return ConvertModelToViewModel(usuarioAtualizado);
        }

        public UsuarioViewModel ObterPorId(Guid id)
        {
            return ConvertModelToViewModel(_usuarioRepository.ObterPorId(id));
        }

        public UsuarioViewModel RemoverUsuario(Guid id)
        {
            return ConvertModelToViewModel(_usuarioRepository.Deletar(id));
        }

        public UsuarioViewModel ObterDadosDoUsuario()
        {
            return ConvertModelToViewModel(_usuarioRepository.ObterPorId(_userLoggedExtensions.getId()));
        }

        public UsuarioViewModel ConvertModelToViewModel(Usuario usuario)
        {
            return new UsuarioViewModel
            {
                id = usuario.Id,
                nome = usuario.Nome,
                data_nascimento = usuario.Data_Nascimento.ToString("dd/MM/yyyy"),
                cpf = usuario.Cpf,
                email = usuario.Email,
                logradouro = usuario.Logradouro,
                numero_casa = usuario.Numero_Casa,
                cep = usuario.Cep,
                bairro = usuario.Bairro,
                cidade = usuario.Cidade,
                estado = usuario.Estado,
                telefone_celular = usuario.Numero_Celular,
                telefone_residencia = usuario.Numero_Residencia,
                nota_perfil = ObterNotaPerfil(usuario?.RefProfissional?.Id_Portifolio),
                img_perfil = !string.IsNullOrEmpty(FileUtil.FindFile(usuario.Img_perfil)) ? FileUtil.FindFile(usuario.Img_perfil) : ImgPerfilVazia.imgPerfilVazia,
                profissao = new ProfissionalViewModel
                {
                    id_profissional = usuario?.Id_Profissional,
                    id_profissao = usuario?.RefProfissional?.Id_Profissao.GetValueOrDefault(),
                    nome_profissao = usuario?.RefProfissional?.RefProfissao.Nome_Profissao,
                },
                //contratacoes = _contratacaoService.ConvertModelsToViewModels(usuario.RefContratacoes)
            };
        }

        private Usuario AtualizarUsuarioViewModelToModel(UsuarioUpdateInputModel usuarioImputModel, Usuario user)
        {
            return new Usuario
            {
                Id = usuarioImputModel.id,
                Nome = usuarioImputModel.nome,
                Cpf = user.Cpf,
                Senha = user.Senha,
                Data_Nascimento = DateTime.ParseExact(usuarioImputModel.data_nascimento, "dd/MM/yyyy", null),
                Logradouro = usuarioImputModel.logradouro,
                Numero_Casa = usuarioImputModel.numero_casa,
                Cep = usuarioImputModel.cep,
                Bairro = usuarioImputModel.bairro,
                Cidade = usuarioImputModel.localidade,
                Estado = usuarioImputModel.uf,
                Numero_Celular = usuarioImputModel.telefone_celular,
                Numero_Residencia = usuarioImputModel.telefone_residencia,
                Data_Atualizacao = DateTime.Now,
                Email = user.Email,
                Data_Criacao = user.Data_Criacao,
                Id_Profissional = user?.Id_Profissional,
                Nota_Perfil = user.Nota_Perfil,
                Img_perfil = user.Img_perfil
            };
        }

        private Usuario ConvertImputModelTOModel(UsuarioImputModel usuarioImputModel)
        {
            return new Usuario
            {
                Nome = usuarioImputModel.nome,
                Data_Nascimento = DateTime.ParseExact(usuarioImputModel.data_nascimento, "dd/MM/yyyy", null),
                Cpf = usuarioImputModel.cpf,
                Senha = BCrypt.Net.BCrypt.HashPassword(usuarioImputModel.senha),
                Email = usuarioImputModel.email,
                Logradouro = usuarioImputModel.logradouro,
                Numero_Casa = usuarioImputModel.numero_casa,
                Cep = usuarioImputModel.cep,
                Bairro = usuarioImputModel.bairro,
                Cidade = usuarioImputModel.localidade,
                Estado = usuarioImputModel.uf,
                Numero_Celular = usuarioImputModel.telefone_celular,
                Data_Atualizacao = DateTime.Now,
                Nota_Perfil = 0.0
            };
        }

        public double? ObterNotaPerfil(Guid? idPortifolio)
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

        private bool ValidarSenha(string senha, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(senha, hash);
        }
    }
}
