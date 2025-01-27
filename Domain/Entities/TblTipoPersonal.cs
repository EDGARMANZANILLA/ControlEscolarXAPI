using System;
using System.Collections.Generic;

namespace Domain;

public partial class TblTipoPersonal
{
    public int IdTblTipoPersonal { get; set; }

    public string TipoPersonal { get; set; } = null!;

    public string NumeroControl { get; set; } = null!;

    public bool RecibeSueldo { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<CatTipoPersonalTabuladorSueldo> CatTipoPersonalTabuladorSueldos { get; set; } = new List<CatTipoPersonalTabuladorSueldo>();

    public virtual ICollection<TblPersonal> TblPersonals { get; set; } = new List<TblPersonal>();
}
