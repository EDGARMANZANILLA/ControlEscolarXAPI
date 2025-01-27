using Application.DTO;
using Application.Handlers.TipoPersonal.Commands;
using Application.Handlers.TipoPersonal.Queries;
using Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITipoPersonalUseCase
    {
        /// <summary>
        /// Obtiene una lista de tipo personal
        /// </summary>
        /// <returns>Retorna una lista de objetos de tipo TipoPersonalDTO</returns>
        Task<APIReply<List<TipoPersonalDTO>>> GetListTipoPersonalHandler();


        /// <summary>
        /// Actualiza un registro de tipo personal 
        /// </summary>
        /// <returns>Retorna una lista de objetos de tipo TipoPersonalDTO</returns>
        Task<APIReply<TipoPersonalDTO>> UpdateTipoPersonalHandler(TipoPersonalDTO tipoPersonal);

        /// <summary>
        /// Crea un recurso de tipo personal 
        /// </summary>
        /// <returns>Retorna un valor boleano segun sea exitoso o no el registro</returns>
        Task<APIReply<bool>> CreateTipoPersonalHandler(CreateTipoPersonalDTO tipoPersonal);

        /// <summary>
        /// Eliminacion en cascada de un tipo de personal 
        /// </summary>
        /// /// <returns>Retorna un valor boleano segun sea exitoso o no el registro</returns>
        Task<APIReply<bool>> DeleteTipoPersonalHandler(CreateTipoPersonalDTO tipoPersonal);

    }
}
