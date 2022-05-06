using OfertaProcura.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.Models
{
    public class BaseEntity : IEntity
    {
        public BaseEntity()
        {
            Id = new Guid();
        }

        public Guid Id { get; set; }
        public DateTime Data_Criacao { get; set; } = DateTime.Now;
    }
}
