using System;
using System.Collections.Generic;

namespace Domain;

public partial class TblPersonalSueldo
{
    public int IdTblPersonalSueldos { get; set; }

    public decimal Sueldo { get; set; }

    public DateTime FechaActivo { get; set; }

    public int IdTblPersonal { get; set; }

    public bool Activo { get; set; }

    public virtual TblPersonal IdTblPersonalNavigation { get; set; } = null!;
}
