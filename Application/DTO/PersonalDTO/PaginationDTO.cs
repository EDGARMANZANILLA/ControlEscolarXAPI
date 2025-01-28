using Application.Responses;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.PersonalDTO
{
    public class PaginationDTO<T>
    {
        public int TotalRegistros { get; set; }
        public T?  ListaPaginada { get; set; }

    }
}
