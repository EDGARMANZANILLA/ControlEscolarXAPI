using Application.DTO;
using Application.Handlers.TipoPersonal.Commands;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<VwTipoPersonal, TipoPersonalDTO>()
                .ForMember(dest => dest.IdTipoPersonal, opt => opt.MapFrom(src => src.IdTblTipoPersonal));

            CreateMap<TipoPersonalDTO, UpdateTipoPersonalCommand>();
            CreateMap<CreateTipoPersonalDTO, CreateTipoPersonalCommand>();
        }
    }
}
