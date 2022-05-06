using OfertaProcura.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.Extensao
{
    public interface IUserLoggedExtensions
    {
        string getEmail();
        string getCpf();
        Guid getId();
    }
}
