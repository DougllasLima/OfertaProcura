using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.Models
{
    public class Usuario : BaseEntity
    {
        public string Nome { get; set; }
        public DateTime Data_Nascimento { get; set; }
        public string Cpf { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public string Logradouro { get; set; }
        public string Numero_Casa { get; set; }
        public string Cep { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Numero_Residencia { get; set; }
        public string Numero_Celular { get; set; }
        public string Img_perfil { get; set; }
        public DateTime Data_Atualizacao { get; set; }
        public double Nota_Perfil { get; set; } // ver de deixar isso nao obrigatorio, falar com rapha e matheus
        public Guid? Id_Profissional { get; set; }
        public Profissional RefProfissional { get; set; }
        public virtual List<Comentario> RefComentario { get; set; }
        public virtual List<Contratacao> RefContratacoes { get; set; }
        
    }
}
