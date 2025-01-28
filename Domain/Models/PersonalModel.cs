using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PersonalModel
    {
        public int IdPersonalModel { get; set; }
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }
        public string NumeroControl { get; set; }
        public string TipoPersonal { get; set; }
        public decimal Sueldo { get; set; }
    }
}
