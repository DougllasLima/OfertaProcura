using Castle.Core.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using OfertaProcura.Context;
using OfertaProcura.Controllers;
using OfertaProcura.Models;
using OfertaProcura.Notificacoes.Interface;
using OfertaProcura.Repositorys.Interface;
using OfertaProcura.Repositorys.Repository;
using OfertaProcura.Services.Interface;
using OfertaProcura.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace OfertaProcura.Tests
{
    [Collection("Application collection")]
    public class ObterProfissaoRequest
    {
        private readonly ApplicationTestsFixture _fixture;
        private readonly ILogger<ProfissaoController> _logger;

        public ObterProfissaoRequest(ApplicationTestsFixture fixture)
        {
            _fixture = fixture;
            _logger = Mock.Of<ILogger<ProfissaoController>>();
        }

        [Fact]
        public void AoInserirProfissaoDeveRetornar200()
        {
            //arrange 
            var _profissaoService = new Mock<IProfissaoService>();
            var repo = new ProfissaoRepository(_fixture.Context);
            var controlador = new ProfissaoController(_fixture.Notificador, _logger, _profissaoService.Object, repo);

            var profissaoImputModel = new ProfissaoImputModel();
            profissaoImputModel.nome = "xUnit";

            //act
            var result = controlador.InserirProfissao(profissaoImputModel);
            var okResult = result as OkObjectResult;

            //assert
            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status200OK, okResult?.StatusCode);
        }

        [Fact]
        public void AoBuscarOsNomesDaProfissaoDeveRetornarPeloMenosUmaProfissao()
        {
            //arrange 
            var profissoes = new List<OnlyNameProfissoesViewModel> { new OnlyNameProfissoesViewModel { nome = "PINTOR" } };
            var _profissaoService = new Mock<IProfissaoService>();
            _profissaoService.Setup(s => s.ObterNomesProfissoes()).Returns(profissoes);
            var mockRepo = new Mock<IProfissaoRepository>();

            var controlador = new ProfissaoController(_fixture.Notificador, _logger, _profissaoService.Object, mockRepo.Object);

            //act
            var result = controlador.ObterNomeProfissoes();

            //Assert
            Assert.Collection(result, item => item.nome.Contains("PINTOR"));
        }
    }
}