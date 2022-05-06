using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    [Route("api/Perfil")]
    public class PortifolioController : MainController
    {
        private readonly ILogger<PortifolioController> _logger;
        private readonly IPortifolioService _portifolioService;
        public PortifolioController(INotificador notificador, ILogger<PortifolioController> logger, IPortifolioService portifolioService) : base(notificador)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _portifolioService = portifolioService ?? throw new ArgumentNullException(nameof(portifolioService));
        }

        [HttpGet("obterPortifolio")]
        [ProducesResponseType(typeof(PortifolioViewModel), StatusCodes.Status200OK)]
        public IActionResult ObterUsuarioPorId([FromQuery] Guid idProfissional)
        {
            return CustomResponse(_portifolioService.ObterPortifolioPorId(idProfissional));
        }

        [HttpGet("obterImagens")]
        [ProducesResponseType(typeof(List<ImagemPortifolioViewModel>), StatusCodes.Status200OK)]
        public IActionResult ObterImagens([FromQuery] Guid idProfissional)
        {
            return CustomResponse(_portifolioService.ObterImagensPerfilProfissional(idProfissional));
        }
    }
}
