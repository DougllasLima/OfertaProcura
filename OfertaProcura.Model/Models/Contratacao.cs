using OfertaProcura.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.Models
{
    public class Contratacao : BaseEntity
    {
        public Guid Id_Contratado { get; set; }
        public Profissional RefContratado { get; set; }
        public Guid Id_Contratante { get; set; }
        public Usuario RefContratante { get; set; }
    }
}
