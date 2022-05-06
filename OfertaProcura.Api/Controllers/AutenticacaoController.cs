using Microsoft.AspNetCore.Mvc;
using OfertaProcura.Notificacoes.Interface;
using OfertaProcura.Services.Interface;
using OfertaProcura.ViewModels;

namespace OfertaProcura.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutenticacaoController : MainController
    {
        private readonly ILogger<AutenticacaoController> _logger;
        private readonly IUsuarioService _usuarioService;
        public AutenticacaoController(INotificador notificador, ILogger<AutenticacaoController> logger, IUsuarioService usuarioService) : base(notificador)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _usuarioService = usuarioService ?? throw new ArgumentNullException(nameof(usuarioService));
        }

        [HttpPost("autenticar")]
        [ProducesResponseType(typeof(UsuarioViewModel), StatusCodes.Status200OK)]
        public IActionResult Authenticar([FromBody] AutenticarImputModel authenticarImputModel)
        {
            return CustomResponse(_usuarioService.AutenticarUsuario(authenticarImputModel));
        }

        [HttpPost("registrar")]
        [ProducesResponseType(typeof(UsuarioViewModel), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        public IActionResult InserirProfissao([FromBody] UsuarioImputModel usuarioImputModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            return CustomResponse(_usuarioService.InserirUsuario(usuarioImputModel));
        }
    }
}
