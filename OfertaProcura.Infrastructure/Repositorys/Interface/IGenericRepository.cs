using OfertaProcura.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.Repositorys.Interface
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        IEnumerable<T> ObterTodos();
        T ObterPorId(Guid id);
        T Inserir(T entity);
        T Atualizar(T entity);
        T Deletar(Guid id);
    }
}
