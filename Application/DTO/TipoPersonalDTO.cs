using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class TipoPersonalDTO
    {
        public int IdTipoPersonal { get; set; }
        public string TipoPersonal { get; set; } = null!;
        public string NumeroControl { get; set; } = null!;
        public decimal SueldoMin { get; set; }
        public decimal SueldoMax { get; set; }
    }
}
