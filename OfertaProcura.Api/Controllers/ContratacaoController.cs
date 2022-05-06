using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfertaProcura.Notificacoes.Interface;
using OfertaProcura.Services.Interface;
using OfertaProcura.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContratacaoController : MainController
    {
        private readonly IContratacaoService _contratacaoService;
        public ContratacaoController(INotificador notificador, IContratacaoService contratacaoService) : base(notificador)
        {
            _contratacaoService = contratacaoService ?? throw new ArgumentNullException(nameof(contratacaoService));
        }

        [HttpPost("contratarProfissional")]
        [ProducesResponseType(typeof(UsuarioViewModel), StatusCodes.Status200OK)]
        public IActionResult InserirContratacao([FromBody] ContratacaoImputModel contratacaoImputModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            return CustomResponse(_contratacaoService.ContratarProfissional(contratacaoImputModel));
        }

        [HttpGet("obterHistoricoContratacao")]
        [ProducesResponseType(typeof(List<ContratadosViewModel>), StatusCodes.Status200OK)]
        public IActionResult ObterHistoricoContratacao()
        {
            return CustomResponse(_contratacaoService.ObterProfissionaisJaContratados());
        }
    }
}
