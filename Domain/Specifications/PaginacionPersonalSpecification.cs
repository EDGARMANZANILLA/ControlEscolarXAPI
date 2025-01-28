using Ardalis.Specification;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Specifications
{
    /// <summary>
    /// Specificacion de la paginacion de vwpersonal
    /// </summary>
    public class PaginacionPersonalSpecification : Specification<VwPersonal>
    {
        
        /// <summary>
        /// obtiene la paginacion de la vista vwPersonal si el filter es true 
        /// </summary>
        /// <param name="skipElements">Elementos a saltar</param>
        /// <param name="takeElements">Elementos a obtener</param>
        /// <param name="filter">booleno que intentifica si se debe aplicar la paginacion o no </param>
        public PaginacionPersonalSpecification(int skipElements, int takeElements, bool filter ) 
        {
            if (filter)
            {
                Query
                       .OrderByDescending(orderDesc => orderDesc.IdTblPersonal)
                       .Skip(skipElements)
                       .Take(takeElements);
            }
            else
            {
                Query.OrderByDescending(orderDesc => orderDesc);
            }
        }    

    }
}
