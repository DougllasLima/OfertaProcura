using Microsoft.Extensions.Configuration;
using OfertaProcura.Extensao;
using OfertaProcura.Models;
using OfertaProcura.Notificacoes.Interface;
using OfertaProcura.Repositorys.Interface;
using OfertaProcura.Services.Interface;
using OfertaProcura.Utils;
using OfertaProcura.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.Services.Service
{
    public class ImagemPortifolioService : BaseService, IImagemPortifolioService
    {
        private readonly IImagemPortifolioRepository _imagemPortifolioRepository;
        private readonly IConfiguration _configuration;
        private readonly IUserLoggedExtensions _userLoggedExtensions;
        public ImagemPortifolioService(INotificador notificador, IImagemPortifolioRepository imagemPortifolioRepository, 
                                       IConfiguration configuration, IUserLoggedExtensions userLoggedExtensions) : base(notificador)
        {
            _imagemPortifolioRepository = imagemPortifolioRepository ?? throw new ArgumentNullException(nameof(imagemPortifolioRepository));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _userLoggedExtensions = userLoggedExtensions ?? throw new ArgumentNullException(nameof(userLoggedExtensions));
        }

        public ImagemPortifolioViewModel AdicionarImagemPortifolio(ImagemPortifolioInputModel imagemPortifolioInputModel)
        {
            var basePath = _configuration["Params:BasePathFiles"];
            var idUsuario = _userLoggedExtensions.getId();

            var imagemPortifolio = new ImagemPortifolio
            {
                Caminho_Imagem = FileUtil.SaveFile(imagemPortifolioInputModel.base64, basePath, idUsuario),
                Id_Portifolio = imagemPortifolioInputModel.id_portifolio,
            };

            var result = _imagemPortifolioRepository.Inserir(imagemPortifolio);

            return ConvertModelToImagemPortifolio(result);
        }

        public List<ImagemPortifolio> ConvertImputModelToModel(ProfissionalImagemPortifolioImputModel imagemPortifolioImputModel, Guid idPortifolio)
        {
            List<ImagemPortifolio> imagemPortifolios = new List<ImagemPortifolio>();

            var basePath = _configuration["Params:BasePathFiles"];
            var idUsuario = _userLoggedExtensions.getId();

            foreach(var imagemBase64 in imagemPortifolioImputModel.base64)
            {
                imagemPortifolios.Add(new ImagemPortifolio
                {
                    Id_Portifolio = idPortifolio,
                    Caminho_Imagem = FileUtil.SaveFile(imagemBase64, basePath, idUsuario)
                });
            }

            return imagemPortifolios;
        }

        public List<ImagemPortifolioViewModel> ObterImagensPortifolio(Guid? idPortifolio)
        {
            List<ImagemPortifolioViewModel> result = new List<ImagemPortifolioViewModel>();

            if (idPortifolio.HasValue)
            {
                var imagens = _imagemPortifolioRepository.ObterImagemPorIdPortifolio(idPortifolio.Value);

                foreach (var imagem in imagens)
                {
                    ImagemPortifolioViewModel imagemPortifolio = new ImagemPortifolioViewModel
                    {
                        base64 = FileUtil.FindFile(imagem.Caminho_Imagem),
                        idImagemPortifolio = imagem.Id
                    };

                    result.Add(imagemPortifolio);
                }
            }

            return result;
        }

        public ImagemPortifolioViewModel AtualizarImagemPortifolio(AtualizarImagemPortifolioImputModel atualizarImagemPortifolioImputModel)
        {
            var basePath = _configuration["Params:BasePathFiles"];

            var imagem = _imagemPortifolioRepository.ObterPorId(atualizarImagemPortifolioImputModel.idImagemPortifolio);

            if (File.Exists(imagem.Caminho_Imagem))
            {
                FileUtil.DeleteFile(imagem.Caminho_Imagem);
            }

            var novaImagemPath = FileUtil.SaveFile(atualizarImagemPortifolioImputModel.base64, basePath, _userLoggedExtensions.getId());

            imagem.Caminho_Imagem = novaImagemPath;

            return ConvertModelToImagemPortifolio(_imagemPortifolioRepository.Atualizar(imagem));
        }

        private ImagemPortifolioViewModel ConvertModelToImagemPortifolio(ImagemPortifolio imagemPortifolio)
        {
            return new ImagemPortifolioViewModel
            {
                idImagemPortifolio = imagemPortifolio.Id,
                base64 = FileUtil.FindFile(imagemPortifolio.Caminho_Imagem)
            };
        }
    }
}
