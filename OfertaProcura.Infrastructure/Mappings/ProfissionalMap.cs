using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OfertaProcura.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.Mappings
{
    public class ProfissionalMap : IEntityTypeConfiguration<Profissional>
    {
        public void Configure(EntityTypeBuilder<Profissional> prProfissional)
        {
            prProfissional.ToTable("Profissional");

            prProfissional.HasKey(x => x.Id);

            prProfissional.Property(x => x.CNPJ)
                .HasMaxLength(20)
                .IsRequired(false);

            prProfissional.Property(x => x.Data_Criacao)
                .IsRequired();

            prProfissional.Property(x => x.Id_Portifolio)
                .IsRequired(false);

            prProfissional.HasOne(x => x.RefPortifolio)
                .WithOne(x => x.RefProfissional)
                .HasForeignKey<Profissional>(x => x.Id_Portifolio);

            prProfissional.Property(x => x.Id_Profissao)
                .IsRequired(false);

            prProfissional.HasOne(x => x.RefProfissao)
                .WithMany(x => x.RefProfissional)
                .HasForeignKey(x => x.Id_Profissao);
        }
    }
}
