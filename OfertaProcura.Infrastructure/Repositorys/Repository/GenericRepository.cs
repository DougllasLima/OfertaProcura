using Microsoft.EntityFrameworkCore;
using OfertaProcura.Context;
using OfertaProcura.Models;
using OfertaProcura.Repositorys.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.Repositorys.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly OfertaProcuraContext context;
        private DbSet<T> entities;
        public GenericRepository(OfertaProcuraContext ofertaProcuraContext)
        {
            context = ofertaProcuraContext ?? throw new ArgumentNullException(nameof(ofertaProcuraContext));
            entities = ofertaProcuraContext.Set<T>();
        }

        public IEnumerable<T> ObterTodos()
        {
            return entities.AsEnumerable();
        }

        public T ObterPorId(Guid id)
        {
            return entities.SingleOrDefault(s => s.Id == id);
        }

        public T Inserir(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            entities.Add(entity);
            context.SaveChanges();

            return entity;
        }

        public T Atualizar(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
            return entity;
        }

        public T Deletar(Guid id)
        {
            if (id == null) throw new ArgumentNullException("entity");

            T entity = entities.SingleOrDefault(x => x.Id == id);
            entities.Remove(entity);
            context.SaveChanges();

            return entity;
        }
    }
}
