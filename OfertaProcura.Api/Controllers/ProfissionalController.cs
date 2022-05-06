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
    [Route("api/[controller]")]
    public class ProfissionalController : MainController
    {
        private readonly ILogger<ProfissionalController> _logger;
        private readonly IUsuarioService _usuarioService;
        private readonly IProfissaoService _profissaoService;
        private readonly IImagemPortifolioService _imagemPortifolioService;
        private readonly IProfissionalService _profissionalService;

        public ProfissionalController(INotificador notificador, ILogger<ProfissionalController> logger,
                                      IUsuarioService usuarioService, IProfissaoService profissaoService, IImagemPortifolioService imagemPortifolioService,
                                      IProfissionalService profissionalService) : base(notificador)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _usuarioService = usuarioService ?? throw new ArgumentNullException(nameof(usuarioService));
            _profissaoService = profissaoService ?? throw new ArgumentNullException(nameof(profissaoService));
            _imagemPortifolioService = imagemPortifolioService ?? throw new ArgumentNullException(nameof(imagemPortifolioService));
            _profissionalService = profissionalService ?? throw new ArgumentNullException(nameof(profissionalService));
        }

        [HttpPost("inserirProfissional")]
        [ProducesResponseType(typeof(UsuarioViewModel), StatusCodes.Status200OK)]
        public IActionResult InserirProfissional([FromBody] InsertProfissionalInputModel profissionalImputModel)
        {
            var result = _usuarioService.InserirProfissionalComProfissao(profissionalImputModel);

            return CustomResponse(result);
        }

        [HttpGet("obterProfissionalPorProfissao")]
        [ProducesResponseType(typeof(ProfissaoProfissionalViewModel), StatusCodes.Status200OK)]
        public IActionResult ObterProfissionalPorProfissao([FromQuery] string nomeProfissao, [FromQuery] List<string> filtroCidades, [FromQuery] int tipoOrdenacao)
        {
            return CustomResponse(_profissaoService.ObterProfissionaisPorProfissao(nomeProfissao, filtroCidades, tipoOrdenacao));
        }

        [HttpPut("alterarImagemPortifolio")]
        [ProducesResponseType(typeof(ImagemPortifolioViewModel), StatusCodes.Status200OK)]
        public IActionResult AtualizarImagemPortifolio([FromBody] AtualizarImagemPortifolioImputModel atualizarImagemPortifolioImputModel)
        {
            return CustomResponse(_imagemPortifolioService.AtualizarImagemPortifolio(atualizarImagemPortifolioImputModel));
        }

        [HttpPut("alterarPortifolioProfissional")]
        [ProducesResponseType(typeof(PortifolioViewModel), StatusCodes.Status200OK)]
        public IActionResult AtualizarPortifolioProfissional([FromBody] AtualizarDescricaoPortifolioImputModel atualizarDescricaoPortifolioImputModel)
        {
            return CustomResponse(_profissionalService.AtualizarPerfilProfissional(atualizarDescricaoPortifolioImputModel));
        }

        [HttpGet("obterAvaliacoes")]
        [ProducesResponseType(typeof(List<AvaliacoesProfissionalViewModel>), StatusCodes.Status200OK)]
        public IActionResult ObterAvaliacoesPorIdProfissional([FromQuery] Guid idProfissional)
        {
            return CustomResponse(_profissaoService.ObterAvaliacoesPorIdProfissional(idProfissional));
        }

        [HttpPost("adicionarImagem")]
        [ProducesResponseType(typeof(ImagemPortifolioViewModel), StatusCodes.Status200OK)]
        public IActionResult AdicionarImagem([FromBody] ImagemPortifolioInputModel imagemPortifolioInputModel)
        {
            return CustomResponse(_imagemPortifolioService.AdicionarImagemPortifolio(imagemPortifolioInputModel));
        }
    }
}
