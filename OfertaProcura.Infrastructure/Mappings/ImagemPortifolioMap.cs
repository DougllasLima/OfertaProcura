using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OfertaProcura.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.Mappings
{
    public class ImagemPortifolioMap : IEntityTypeConfiguration<ImagemPortifolio>
    {
        public void Configure(EntityTypeBuilder<ImagemPortifolio> prImagemPortifolio)
        {
            prImagemPortifolio.ToTable("ImagemPortifolio");

            prImagemPortifolio.HasKey(x => x.Id);

            prImagemPortifolio.Property(x => x.Id_Portifolio)
                .IsRequired();

            prImagemPortifolio.HasOne(x => x.RefPortifolio)
                .WithMany(x => x.RefImagemPortifolios)
                .HasForeignKey(x => x.Id_Portifolio);

            prImagemPortifolio.Property(x => x.Caminho_Imagem)
                .IsRequired();

            prImagemPortifolio.Property(x => x.Data_Criacao)
                .IsRequired();
        }
    }
}
