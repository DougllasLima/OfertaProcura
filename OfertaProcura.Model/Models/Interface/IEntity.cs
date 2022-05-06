using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.Models.Interface
{
    public interface IEntity
    {
        public Guid Id { get; set; }
        public DateTime Data_Criacao { get; set; }
    }
}
