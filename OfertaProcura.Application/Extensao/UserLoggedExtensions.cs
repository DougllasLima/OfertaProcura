using Microsoft.AspNetCore.Http;
using OfertaProcura.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OfertaProcura.Extensao
{
    public class UserLoggedExtensions : IUserLoggedExtensions
    {
        private readonly IHttpContextAccessor _httpContextAcessor;

        public UserLoggedExtensions(IHttpContextAccessor httpContextAcessor)
        {
            _httpContextAcessor = httpContextAcessor ?? throw new ArgumentNullException(nameof(httpContextAcessor));
        }

        public string getCpf()
        {
            return _httpContextAcessor.HttpContext.User.FindFirst(x => x.Type == ClaimTypes.Name)?.Value;
        }

        public string getEmail()
        {
            return _httpContextAcessor.HttpContext.User.FindFirst(x => x.Type == ClaimTypes.Email)?.Value;
        }

        public Guid getId()
        {
            return Guid.Parse(_httpContextAcessor.HttpContext.User.FindFirst(x => x.Type == "Id")?.Value);
        }
    }
}
