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
    public class ComentarioController : MainController
    {
        private readonly IComentarioService _comentarioService;
        public ComentarioController(INotificador notificador, IComentarioService comentarioService) : base(notificador)
        {
            _comentarioService = comentarioService ?? throw new ArgumentNullException(nameof(comentarioService));
        }

        [HttpPost("inserirComentario")]
        [ProducesResponseType(typeof(ComentarioViewModel), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        public IActionResult InserirComentario([FromBody] ComentarioImputModel comentarioImputModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            return CustomResponse(_comentarioService.InserirComentario(comentarioImputModel));
        }

        [HttpGet("obterComentariosDoProfissional/{id}")]
        [ProducesResponseType(typeof(UsuarioViewModel), StatusCodes.Status200OK)]
        public IActionResult ObterComentariosPorIdPortifolio([FromRoute] Guid id)
        {
            return CustomResponse(_comentarioService.ObterTodosComentariosPorIdPortifolio(id));
        }

    }
}
