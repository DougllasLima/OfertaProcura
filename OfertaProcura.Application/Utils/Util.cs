using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OfertaProcura.Utils
{
    public static class Util
    {
        public static string FormatarCelular(string numero)
        {
            var numeroFormatado = string.Format("{0:(##) #####-####}", long.Parse(numero)); 

            return numeroFormatado;
        }

        public static string EnviarEmail(string Destinatario, string Remetente, string Assunto, string bodyHtml)
        {
            try
            {
                bool bValidaEmail = ValidaEnderecoEmail(Destinatario);

                if (bValidaEmail == false)
                    return "Email do destinatário inválido: " + Destinatario;

                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(Remetente));
                email.To.Add(MailboxAddress.Parse(Destinatario));
                email.Subject = Assunto;
                email.Body = new TextPart(TextFormat.Html) { Text = bodyHtml };

                using var smtp = new SmtpClient();
                smtp.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("", "");
                smtp.Authenticate("user", "user");
                smtp.Send(email);
                smtp.Disconnect(true);

                return "Mensagem enviada para  " + Destinatario + " às " + DateTime.Now.ToString() + ".";
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
                return ex.Message.ToString() + erro;
            }
        }

        public static bool ValidaEnderecoEmail(string enderecoEmail)
        {
            try
            {
                //define a expressão regulara para validar o email
                string texto_Validar = enderecoEmail;
                Regex expressaoRegex = new Regex(@"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}");

                // testa o email com a expressão
                if (expressaoRegex.IsMatch(texto_Validar))
                {
                    // o email é valido
                    return true;
                }
                else
                {
                    // o email é inválido
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
