using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.ViewModels
{
    public class UsuarioViewModel
    {
        public UsuarioViewModel()
        {
            contratacoes = new List<ContratacaoViewModel>();
        }

        public Guid id { get; set; }
        public string nome { get; set; }
        public string data_nascimento { get; set; }
        public string cpf { get; set; }
        public string email { get; set; }
        public string logradouro { get; set; }
        public string numero_casa { get; set; }
        public string cep { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }
        public string telefone_residencia { get; set; }
        public string telefone_celular { get; set; }
        public double? nota_perfil { get; set; }
        public string img_perfil { get; set; }
        public ProfissionalViewModel profissao { get; set; }
        public List<ContratacaoViewModel> contratacoes { get; set; }
    }

    public class UsuarioImputModel
    {
        public string nome { get; set; }
        public string data_nascimento { get; set; }
        public string cpf { get; set; }
        public string senha { get; set; }
        public string email { get; set; }
        public string logradouro { get; set; }
        public string numero_casa { get; set; }
        public string cep { get; set; }
        public string bairro { get; set; }
        public string localidade { get; set; }
        public string uf { get; set; }
        public string telefone_residencia { get; set; }
        public string telefone_celular { get; set; }
    }

    public class UsuarioUpdateInputModel
    {
        public Guid id { get; set; }
        public string nome { get; set; }
        public string data_nascimento { get; set; }
        public string email { get; set; }
        public string logradouro { get; set; }
        public string numero_casa { get; set; }
        public string cep { get; set; }
        public string bairro { get; set; }
        public string localidade { get; set; }
        public string uf { get; set; }
        public string telefone_residencia { get; set; }
        public string telefone_celular { get; set; }
    }

    public class AtualizarImagemPerfilInputModel
    {
        public string base64 { get; set; }
    }
}
