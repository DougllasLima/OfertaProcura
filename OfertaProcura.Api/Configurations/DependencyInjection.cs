using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OfertaProcura.Extensao;
using OfertaProcura.Models.Options;
using OfertaProcura.Notificacoes;
using OfertaProcura.Notificacoes.Interface;
using OfertaProcura.Repositorys.Interface;
using OfertaProcura.Repositorys.Repository;
using OfertaProcura.Services.Interface;
using OfertaProcura.Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            var configurationRoot = new ConfigurationBuilder()
                                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                        .AddJsonFile("emails.json", optional: false, reloadOnChange: true)
                                        .Build();

            services.AddOptions();

            services.AddHttpContextAccessor();

            RegisterApplicationServices(services);

            RegisterRepositories(services);

            RegisterNotification(services);

            RegisterExtension(services);

            services.Configure<EmailOptions>(configurationRoot);

            return services;
        }

        private static void RegisterApplicationServices(IServiceCollection services)
        {
            services.AddScoped<IProfissaoService, ProfissaoService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IPortifolioService, PortifolioService>();
            services.AddScoped<IProfissionalService, ProfissionalService>();
            services.AddScoped<IComentarioService, ComentarioService>();
            services.AddScoped<IContratacaoService, ContratacaoService>();
            services.AddScoped<IImagemPortifolioService, ImagemPortifolioService>();
            services.AddScoped<IContratadoEmailService, ContratadoEmailService>();
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IProfissaoRepository, ProfissaoRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IProfissionalRepository, ProfissionalRepository>();
            services.AddScoped<IPortifolioRepository, PortifolioRepository>();
            services.AddScoped<IComentarioRepository, ComentarioRepository>();
            services.AddScoped<IImagemPortifolioRepository, ImagemPortifolioRepository>();
            services.AddScoped<IContratacaoRepository, ContratacaoRepository>();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }

        private static void RegisterNotification(IServiceCollection services)
        {
            services.AddScoped<INotificador, Notificador>();
        }

        private static void RegisterExtension(IServiceCollection services)
        {
            services.AddScoped<IUserLoggedExtensions, UserLoggedExtensions>();
        }
    }
}
