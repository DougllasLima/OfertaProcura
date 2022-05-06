using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OfertaProcura.Models;

namespace OfertaProcura.Mappings
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> prUsuario)
        {
            prUsuario.ToTable("Usuario");

            prUsuario.HasKey(x => x.Id);

            prUsuario.Property(x => x.Nome)
                .HasMaxLength(150)
                .IsRequired();

            prUsuario.Property(x => x.Data_Nascimento)
                .IsRequired();

            prUsuario.Property(x => x.Cpf)
                .HasMaxLength(14)
                .IsRequired();

            prUsuario.Property(x => x.Senha)
                .IsRequired();

            prUsuario.Property(x => x.Email)
                .HasMaxLength(150)
                .IsRequired();

            prUsuario.Property(x => x.Logradouro)
                .HasMaxLength(100)
                .IsRequired(false);

            prUsuario.Property(x => x.Numero_Casa)
                .HasMaxLength(20)
                .IsRequired(false);

            prUsuario.Property(x => x.Cep)
                .HasMaxLength(50)
                .IsRequired(false);

            prUsuario.Property(x => x.Bairro)
                .HasMaxLength(40)
                .IsRequired(false);

            prUsuario.Property(x => x.Cidade)
                .HasMaxLength(50)
                .IsRequired(false);

            prUsuario.Property(x => x.Estado)
                .HasMaxLength(50)
                .IsRequired(false);

            prUsuario.Property(x => x.Numero_Residencia)
                .HasMaxLength(40)
                .IsRequired(false);

            prUsuario.Property(x => x.Numero_Celular)
                .HasMaxLength(40)
                .IsRequired(false);

            prUsuario.Property(x => x.Img_perfil)
                .IsRequired(false);

            prUsuario.Property(x => x.Data_Atualizacao)
                .IsRequired();

            prUsuario.Property(x => x.Data_Criacao)
                .IsRequired();

            prUsuario.Property(x => x.Nota_Perfil)
                .IsRequired();

            prUsuario.Property(x => x.Id_Profissional)
                .IsRequired(false);

            prUsuario.HasOne(x => x.RefProfissional)
                 .WithOne(x => x.RefUsuario)
                 .HasForeignKey<Usuario>(x => x.Id_Profissional);
        }
    }
}
