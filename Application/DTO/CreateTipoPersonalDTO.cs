using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class CreateTipoPersonalDTO
    {
        public string TipoPersonal { get; set; }
        public string NumeroControl { get; set; }
        public bool RecibeSueldo { get; set; }
        public decimal SueldoMin { get; set; }
        public decimal SueldoMax { get; set; }
    }
}
