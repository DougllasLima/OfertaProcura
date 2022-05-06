using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OfertaProcura.Models;

namespace OfertaProcura.Mappings
{
    public class ContratacaoMap : IEntityTypeConfiguration<Contratacao>
    {
        public void Configure(EntityTypeBuilder<Contratacao> prContratacao)
        {
            prContratacao.ToTable("Contratacao");

            prContratacao.HasKey(x => x.Id);

            prContratacao.Property(x => x.Data_Criacao)
                .IsRequired();

            prContratacao.Property(x => x.Id_Contratado)
                .IsRequired();

            prContratacao.Property(x => x.Id_Contratante)
                .IsRequired();

            prContratacao.HasOne(x => x.RefContratante)
                .WithMany(x => x.RefContratacoes)
                .HasForeignKey(x => x.Id_Contratante);

            prContratacao.HasOne(x => x.RefContratado)
                .WithMany(x => x.RefContratacoes)
                .HasForeignKey(x => x.Id_Contratado);

            prContratacao.Property(x => x.Data_Criacao)
                .IsRequired();
        }
    }
}
