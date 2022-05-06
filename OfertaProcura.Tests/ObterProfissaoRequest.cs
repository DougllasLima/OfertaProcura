using Castle.Core.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using OfertaProcura.Context;
using OfertaProcura.Controllers;
using OfertaProcura.Notificacoes.Interface;
using OfertaProcura.Repositorys.Interface;
using OfertaProcura.Repositorys.Repository;
using OfertaProcura.Services.Interface;
using OfertaProcura.ViewModels;
using System.Collections.Generic;
using Xunit;

namespace OfertaProcura.Tests
{
    public class ObterProfissaoRequest
    {
        [Fact]
        public void AoObterTodasAsProfissoesRetornar200()
        {
            //arrange
            var mockConfiguration = new Mock<IConfiguration>();
            var mockNotificador = new Mock<INotificador>();
            var mockLogger = new Mock<ILogger<ProfissaoController>>();
            var mockProfissaoService = new Mock<IProfissaoService>();
            
            var options = new DbContextOptionsBuilder<OfertaProcuraContext>()
                .UseInMemoryDatabase("DbOfertaProcuraMemory")
                .Options;
            var contexto = new OfertaProcuraContext(options, mockConfiguration.Object);
            var repo = new ProfissaoRepository(contexto);

            var controlador = new ProfissaoController(mockNotificador.Object, mockLogger.Object, mockProfissaoService.Object, repo);

            var profissaoImputModel = new ProfissaoImputModel();
            profissaoImputModel.nome = "xUnit";

            //act
            var result = controlador.InserirProfissao(profissaoImputModel);
            var okResult = result as OkObjectResult;

            //assert
            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status200OK, okResult?.StatusCode);
        }
    }
}