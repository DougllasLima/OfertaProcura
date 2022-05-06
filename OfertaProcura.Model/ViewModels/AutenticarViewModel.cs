using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.ViewModels
{
    public class AutenticarViewModel
    {
        public string token { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
    }

    public class AutenticarImputModel
    {
        public string cpf { get; set; }
        public string senha { get; set; }
    }
}
