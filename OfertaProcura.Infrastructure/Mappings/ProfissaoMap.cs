using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OfertaProcura.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.Mappings
{
    public class ProfissaoMap : IEntityTypeConfiguration<Profissao>
    {
        public void Configure(EntityTypeBuilder<Profissao> prProfissao)
        {
            prProfissao.ToTable("Profissao");

            prProfissao.HasKey(x => x.Id);

            prProfissao.Property(x => x.Nome_Profissao)
                .HasMaxLength(50)
                .IsRequired();

            prProfissao.Property(x => x.Data_Criacao)
                .IsRequired();

        }
    }
}
