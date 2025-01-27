using System;
using System.Collections.Generic;

namespace Domain;

public partial class TblUsuario
{
    public int IdUsuarios { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string Contrasenia { get; set; } = null!;

    public DateTime FechaAlta { get; set; }

    public bool Activo { get; set; }
}
