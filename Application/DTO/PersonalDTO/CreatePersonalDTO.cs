using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.PersonalDTO
{
    public class CreatePersonalDTO
    {
        public string Nombre { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string CorreoElectronico { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public bool Estatus { get; set; }
        public int IdTipoPersonal { get; set; }
        public string IdentificadorDeControl { get; set; } = null!;
        public decimal Sueldo { get; set; }
    }
}
