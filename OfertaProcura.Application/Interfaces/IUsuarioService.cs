using OfertaProcura.Models;
using OfertaProcura.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.Services.Interface
{
    public interface IUsuarioService
    {
        UsuarioViewModel InserirProfissionalComProfissao(InsertProfissionalInputModel insertProfissionalInputModel);
        AutenticarViewModel AutenticarUsuario(AutenticarImputModel authenticarImputModel);
        UsuarioViewModel InserirUsuario(UsuarioImputModel usuarioImputModel);
        UsuarioViewModel ObterPorId(Guid id);
        UsuarioViewModel AtualizarUsuario(UsuarioUpdateInputModel usuario);
        UsuarioViewModel AtualizarImagemPerfilUsuario(string base64);
        UsuarioViewModel RemoverUsuario(Guid id);
        UsuarioViewModel ObterDadosDoUsuario();
        UsuarioViewModel ConvertModelToViewModel(Usuario usuario);
        double? ObterNotaPerfil(Guid? idPortifolio);
    }
}
