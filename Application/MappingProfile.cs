using Application.DTO;
using Application.DTO.PersonalDTO;
using Application.Handlers.Personal.Commands;
using Application.Handlers.TipoPersonal.Commands;
using Application.Handlers.Usuario;
using AutoMapper;
using Domain;
using Domain.Models;
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
            
            CreateMap<TblUsuario, UsuarioModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.NombreUsuario))
                .ForMember(dest => dest.AccessToken, opt => opt.Ignore());

            //Mapping para actualizar un tipo de personal
            CreateMap<TipoPersonalDTO, UpdateTipoPersonalCommand>()
                    .ForMember(dest => dest.IdTipoPersonal, opt => opt.Ignore());
            //Mapping para devolver el dto despues de actualizar el tipo de personal
            CreateMap<UpdateTipoPersonalCommand, TipoPersonalDTO>();

            CreateMap<CreateTipoPersonalDTO, CreateTipoPersonalCommand>();
            CreateMap<UsuarioAutenticadoDTO, AutenticationUsuarioQuery>();

            //Mappin para actualizar un personal
            //Funciona para actualizar un personal
            CreateMap<UpdatePersonalDTO, UpdatePersonalCommand>();
            CreateMap<UpdatePersonalCommand, UpdatePersonalDTO>();

            //Mapping para crear un personal
            CreateMap<CreatePersonalDTO, CreatePersonalCommand>();
        }
    }
}
