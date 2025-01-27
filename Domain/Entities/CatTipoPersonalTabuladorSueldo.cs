using System;
using System.Collections.Generic;

namespace Domain;

public partial class CatTipoPersonalTabuladorSueldo
{
    public int IdCatTipoPersonalTabuladorSueldos { get; set; }

    public int IdTblTipoPersonal { get; set; }

    public decimal SueldoMin { get; set; }

    public decimal SueldoMax { get; set; }

    public bool Activo { get; set; }

    public virtual TblTipoPersonal IdTblTipoPersonalNavigation { get; set; } = null!;
}
