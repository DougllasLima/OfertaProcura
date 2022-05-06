using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OfertaProcura.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.Mappings
{
    public class ComentarioMap : IEntityTypeConfiguration<Comentario>
    {
        public void Configure(EntityTypeBuilder<Comentario> prComentario)
        {
            prComentario.ToTable("Comentarios");

            prComentario.HasKey(x => x.Id);

            prComentario.HasOne(x => x.RefCliente)
                .WithMany(x => x.RefComentario)
                .HasForeignKey(x => x.Id_Cliente);

            prComentario.HasOne(x => x.RefPortifolio)
                .WithMany(x => x.RefComentarios)
                .HasForeignKey(x => x.Id_Portifolio);

            prComentario.Property(x => x.Descricao)
                .HasMaxLength(500)
                .IsRequired();

            prComentario.Property(x => x.Nota)
                .IsRequired();

            prComentario.Property(x => x.Data_Criacao)
                .IsRequired();
        }
    }
}
