using OfertaProcura.Context;
using OfertaProcura.Models;
using OfertaProcura.Repositorys.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.Repositorys.Repository
{
    public class ImagemPortifolioRepository : GenericRepository<ImagemPortifolio>, IImagemPortifolioRepository
    {
        public ImagemPortifolioRepository(OfertaProcuraContext ofertaProcuraContext) : base(ofertaProcuraContext)
        {
        }

        public List<ImagemPortifolio> ObterImagemPorIdPortifolio(Guid idPortifolio)
        {
            var imagens = context.ImagemPortifolio.Where(x => x.Id_Portifolio == idPortifolio).ToList();

            return imagens;
        }
    }
}
