using OfertaProcura.Extensao;
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
    public class ComentarioService : BaseService, IComentarioService
    {
        private readonly IComentarioRepository _comentarioRepository;
        private readonly IUserLoggedExtensions _userLoggedExtensions;
        public ComentarioService(INotificador notificador, IComentarioRepository comentarioRepository, IUserLoggedExtensions userLoggedExtensions) : base(notificador)
        {
            _comentarioRepository = comentarioRepository ?? throw new ArgumentNullException(nameof(comentarioRepository));
            _userLoggedExtensions = userLoggedExtensions ?? throw new ArgumentNullException(nameof(userLoggedExtensions));
        }

        public ComentarioViewModel InserirComentario(ComentarioImputModel comentarioImputModel)
        {
            if (comentarioImputModel.nota is >= 1 and <= 5)
            {
                return ConvertModelToViewModel(_comentarioRepository.Inserir(ConvertImputModelTOModel(comentarioImputModel)));
            }

            NotificarErro("Só é permitido notas entre 1 a 5.");
            return null;
        }

        public List<ComentarioViewModel> ObterTodosComentariosPorIdPortifolio(Guid id)
        {
            List<ComentarioViewModel> profissoesViewModel = new List<ComentarioViewModel>();

            var comentarios = _comentarioRepository.ObterComentarioPorIdPortifolio(id);

            foreach (var comentario in comentarios)
            {
                profissoesViewModel.Add(ConvertModelToViewModel(comentario));
            }

            return profissoesViewModel;
        }

        private Comentario ConvertImputModelTOModel(ComentarioImputModel comentarioImputModel)
        {
            return new Comentario
            {
                Id_Cliente = _userLoggedExtensions.getId(),
                Id_Portifolio = comentarioImputModel.id_portifolio,
                Descricao = comentarioImputModel.comentario,
                Nota = comentarioImputModel.nota,
                Data_Atualizacao = DateTime.Now
            };
        }

        private ComentarioViewModel ConvertModelToViewModel(Comentario comentario)
        {
            return new ComentarioViewModel
            {
                id_usuario = comentario.Id_Cliente,
                nome_usuario = comentario?.RefCliente?.Nome,
                descricao = comentario.Descricao,
                data_comentario = comentario.Data_Criacao.ToString("dd/MM/yyyy"),
                nota = comentario.Nota
            };
        }
    }
}
