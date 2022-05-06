using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OfertaProcura.Notificacoes.Interface;
using OfertaProcura.Services.Interface;
using OfertaProcura.ViewModels;
using System;

namespace OfertaProcura.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : MainController
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(INotificador notificador, ILogger<UsuarioController> logger, IUsuarioService usuarioService) : base(notificador)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _usuarioService = usuarioService ?? throw new ArgumentNullException(nameof(usuarioService));
        }

        [HttpGet("obterUsuario")]
        [ProducesResponseType(typeof(UsuarioViewModel), StatusCodes.Status200OK)]
        public IActionResult ObterUsuarioPorId([FromQuery] Guid id)
        {
            return CustomResponse(_usuarioService.ObterPorId(id));
        }

        [HttpGet("obterDadosUsuario")]
        [ProducesResponseType(typeof(UsuarioViewModel), StatusCodes.Status200OK)]
        public IActionResult ObterDadosUsuario()
        {
            return CustomResponse(_usuarioService.ObterDadosDoUsuario());
        }

        [HttpPut("atualizarUsuario")]
        [ProducesResponseType(typeof(UsuarioViewModel), StatusCodes.Status200OK)]
        public IActionResult AtualizarUsuario([FromBody] UsuarioUpdateInputModel usuario)
        {
            return CustomResponse(_usuarioService.AtualizarUsuario(usuario));
        }

        [HttpPut("atualizarImagemPerfil")]
        [ProducesResponseType(typeof(UsuarioViewModel), StatusCodes.Status200OK)]
        public IActionResult AtualizarImagemPerfil([FromBody] AtualizarImagemPerfilInputModel atualizarImagemPerfilInputModel)
        {
            return CustomResponse(_usuarioService.AtualizarImagemPerfilUsuario(atualizarImagemPerfilInputModel.base64));
        }
    }
}
