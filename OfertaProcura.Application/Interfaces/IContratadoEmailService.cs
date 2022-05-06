using OfertaProcura.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.Services.Interface
{
    public interface IContratadoEmailService
    {
        string ObterEmailContrato(Usuario usuario, Profissional profissional);
    }
}
