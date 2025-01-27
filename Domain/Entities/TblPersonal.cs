using System;
using System.Collections.Generic;

namespace Domain;

public partial class TblPersonal
{
    public int IdTblPersonal { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public DateTime FechaNacimiento { get; set; }

    public string NumeroControl { get; set; } = null!;

    public bool Estatus { get; set; }

    public int IdTblTipoPersonal { get; set; }

    public bool Activo { get; set; }

    public virtual TblTipoPersonal IdTblTipoPersonalNavigation { get; set; } = null!;

    public virtual ICollection<TblPersonalSueldo> TblPersonalSueldos { get; set; } = new List<TblPersonalSueldo>();
}
