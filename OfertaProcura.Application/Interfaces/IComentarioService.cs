using OfertaProcura.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.Services.Interface
{
    public interface IComentarioService
    {
        ComentarioViewModel InserirComentario(ComentarioImputModel comentarioImputModel);
        List<ComentarioViewModel> ObterTodosComentariosPorIdPortifolio(Guid id);
    }
}
