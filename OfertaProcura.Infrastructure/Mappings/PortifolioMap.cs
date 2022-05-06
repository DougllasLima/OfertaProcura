using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OfertaProcura.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.Mappings
{
    public class PortifolioMap : IEntityTypeConfiguration<Portifolio>
    {
        public void Configure(EntityTypeBuilder<Portifolio> prPortifolio)
        {
            prPortifolio.ToTable("Portifolio");

            prPortifolio.HasKey(x => x.Id);

            prPortifolio.Property(x => x.Descricao)
                .HasMaxLength(400)
                .IsRequired(false);

            prPortifolio.Property(x => x.Data_Atualizacao)
                .IsRequired();

            prPortifolio.Property(x => x.Data_Criacao)
                .IsRequired();

            prPortifolio.HasOne(x => x.RefProfissional)
                .WithOne(x => x.RefPortifolio)
                .HasForeignKey<Profissional>(x => x.Id_Portifolio);
        }
    }
}
