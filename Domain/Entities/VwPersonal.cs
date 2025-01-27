using System;
using System.Collections.Generic;

namespace Domain;

public partial class VwPersonal
{
    public int IdTblPersonal { get; set; }

    public string NombreCompleto { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string NumeroControl { get; set; } = null!;

    public string TipoPersonal { get; set; } = null!;

    public decimal Sueldo { get; set; }
}
