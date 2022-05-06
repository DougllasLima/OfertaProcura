using Microsoft.Extensions.Options;
using OfertaProcura.Models;
using OfertaProcura.Models.Options;
using OfertaProcura.Notificacoes.Interface;
using OfertaProcura.Services.Interface;
using OfertaProcura.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.Services.Service
{
    public class ContratadoEmailService : BaseService, IContratadoEmailService
    {
        private readonly EmailOptions _emailOptions;
        private const string COMPROVANTE_CONTRATO = "1";

        public ContratadoEmailService(INotificador notificador, IOptions<EmailOptions> emailOptions) : base(notificador)
        {
            _emailOptions = emailOptions is null ? throw new ArgumentNullException(nameof(emailOptions)) : emailOptions.Value;
        }


        public string ObterEmailContrato(Usuario usuario, Profissional profissional)
        {
            var emailTemplate = _emailOptions.EmailTemplates.Where(x => x.Template == COMPROVANTE_CONTRATO).First();

            string diretorioTemplate = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), emailTemplate.ArquivoTemplate);
            string template = File.ReadAllText(diretorioTemplate);

            template = template.Replace("#NOME_CONTRATANTE#", usuario.Nome)
                               .Replace("#NOME_SERVICO#", profissional.RefProfissao.Nome_Profissao)
                               .Replace("#NOME_PROFISSIONAL#", profissional.RefUsuario.Nome)
                               .Replace("#EMAIL_PROFISSIONAL#", profissional.RefUsuario.Email)
                               .Replace("#CELULAR_PROFISSIONAL#", profissional.RefUsuario.Numero_Celular);

            return template;
        }
    }
}
