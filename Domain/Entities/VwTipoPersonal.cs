using System;
using System.Collections.Generic;

namespace Domain;

public partial class VwTipoPersonal
{
    public int IdTblTipoPersonal { get; set; }

    public string TipoPersonal { get; set; } = null!;

    public string NumeroControl { get; set; } = null!;

    public decimal SueldoMin { get; set; }

    public decimal SueldoMax { get; set; }

    public bool Activo { get; set; }
}
