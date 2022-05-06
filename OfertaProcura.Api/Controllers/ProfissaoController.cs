using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OfertaProcura.Models;
using OfertaProcura.Notificacoes.Interface;
using OfertaProcura.Repositorys.Interface;
using OfertaProcura.Services.Interface;
using OfertaProcura.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProfissaoController : MainController
    {
        private readonly ILogger<ProfissaoController> _logger;
        private readonly IProfissaoService _profissaoService;
        private readonly IProfissaoRepository _profissaoRepository;

        public ProfissaoController(INotificador notificador,
                                   ILogger<ProfissaoController> logger, 
                                   IProfissaoService profissaoService, 
                                   IProfissaoRepository profissaoRepository) : base(notificador)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _profissaoService = profissaoService ?? throw new ArgumentNullException(nameof(profissaoService));
            _profissaoRepository = profissaoRepository ?? throw new ArgumentNullException(nameof(profissaoRepository));
        }

        [HttpPost("inserirProfissao")]
        [ProducesResponseType(typeof(ProfissaoViewModel), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        public IActionResult InserirProfissao([FromBody] ProfissaoImputModel profissaoImputModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            return CustomResponse(_profissaoService.InserirProfissao(profissaoImputModel));
        }

        [HttpPut("atualizarProfissao")]
        public ProfissaoViewModel ObterProfissoes(ProfissaoViewModel profissaoViewModel)
        {
            return _profissaoService.AtualizarProfissao(profissaoViewModel);
        }

        [HttpDelete("removerProfissao")]
        public ProfissaoViewModel DeletarProfissoes([FromHeader] Guid id)
        {
            return _profissaoService.RemoverProfissao(id);
        }

        [HttpGet("obterPorId")]
        public ProfissaoViewModel ObterProfissaoPorId([FromHeader] Guid id)
        {
            return _profissaoService.ObterPorId(id);
        }


        [HttpGet("obterProfissoes")]
        public List<ProfissaoViewModel> ObterProfissoes()
        {
            return _profissaoService.ObterTodos();
        }

        [HttpGet("obterNomeProfissoes")]
        public List<OnlyNameProfissoesViewModel> ObterNomeProfissoes()
        {
            return _profissaoService.ObterNomesProfissoes();
        }
    }
}
