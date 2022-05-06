
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using OfertaProcura.Context;
using OfertaProcura.Notificacoes.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OfertaProcura.Tests
{
    [CollectionDefinition("Application collection")]
    public class ApplicationCollection : ICollectionFixture<ApplicationTestsFixture> { }

    public class ApplicationTestsFixture : IDisposable
    {
        public IConfiguration Configuration { get; private set; }
        public INotificador Notificador { get; private set; }
        public OfertaProcuraContext Context { get; private set; }

        public ApplicationTestsFixture()
        {
            Configuration = Mock.Of<IConfiguration>();
            Notificador = Mock.Of<INotificador>();
            Context = new OfertaProcuraContext(GetOptions(), Configuration);
        }

        public static DbContextOptions<OfertaProcuraContext>? GetOptions() => new DbContextOptionsBuilder<OfertaProcuraContext>()
                    .UseInMemoryDatabase("DbOfertaProcuraMemory")
                    .Options;

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
