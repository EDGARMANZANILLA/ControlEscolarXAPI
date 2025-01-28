using Application.DTO.PersonalDTO;
using Application.Responses;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPersonalUseCase
    {
        /// <summary>
        /// Contrato para encontrar un numero de control
        /// </summary>
        /// <param name="numeroControl">numero de control a buscar</param>
        /// <returns>retorna un personal coincidente con la busqueda </returns>
        Task<APIReply<VwPersonal>> SearchNumControlPersonal(string numeroControl);

        /// <summary>
        /// Lista paginada del personal
        /// </summary>
        /// <param name="skip">Elementos a saltar</param>
        /// <param name="take">Elementos a tomar</param>
        /// <returns>retorna una lista de personal</returns>
        Task<APIReply<PaginationDTO<List<VwPersonal>>>> GetPaginatedListPersonal(int skip, int take);

        /// <summary>
        /// Crea un nuevo recurso de tipo personal
        /// </summary>
        /// <param name="createPersonal">DTO necesario para la creacion del recurso</param>
        /// <returns>retorna una cadena si fue exitoso </returns>
        Task<APIReply<string>> CreatePersonal(CreatePersonalDTO createPersonal);

        /// <summary>
        /// Actualiza un recurso de tipo personal
        /// </summary>
        /// <param name="idPersonal">Numero identificador del recurso a actualizar</param>
        /// <param name="updatePersonal">Informacion de la que se podria actualizar</param>
        /// <returns>Retorna la entidad actualizada</returns>
        Task<APIReply<UpdatePersonalDTO>> UpdatePersonal(int idPersonal, UpdatePersonalDTO updatePersonal);

        /// <summary>
        /// Elimina un recurso de tipo personal
        /// </summary>
        /// <param name="idPersonal">identificador del recurso a eliminar</param>
        /// <returns>retorna el numero de recursos eliminados </returns>
        Task<APIReply<int>> DeletePersonal(int idPersonal);

    }
}
