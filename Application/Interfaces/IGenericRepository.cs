using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Obtiene todas las entidades de tipo <typeparamref name="T"/> de la base de datos.
        /// </summary>
        /// <returns>Una lista de solo lectura de todas las entidades.</returns>
        Task<IReadOnlyList<T>> ObtenerTodos();
        IQueryable<T> ObtenerTodosAsQuery();

        /// <summary>
        /// Obtiene una entidad por su identificador.
        /// </summary>
        /// <param name="id">El identificador de la entidad.</param>
        /// <returns>La entidad encontrada, o null si no se encuentra.</returns>
        Task<T?> ObtenerPorId(int id);

        /// <summary>
        /// Obtiene las entidades que se cumplean con el filtro de la expresion lamda
        /// </summary>
        /// <param name="criterio">Expresion lamda sobre el criterio a buscar en las entidades</param>
        /// <returns>El query en memoria de las entiendades que cumplan con el criterio enpecificado</returns>
        IQueryable<T> ObtenerPorFiltro(Expression<Func<T, bool>> criterio);


        IQueryable<T> ObtenerPorFiltroInclude(Expression<Func<T, bool>> criterio, params Expression<Func<T, object>>[] includes);



        /// <summary>
        /// Agrega una nueva entidad a la base de datos.
        /// </summary>
        /// <param name="entidad">La entidad a agregar.</param>
        /// <returns>La entidad agregada.</returns>
        Task<T?> Agregar(T entidad);

        /// <summary>
        /// Actualiza una entidad existente en la base de datos.
        /// </summary>
        /// <param name="entidad">La entidad a actualizar.</param>
        /// <returns>La entidad actualizada.</returns>
        Task<T?> Actualizar(T entidad);

        /// <summary>
        /// Elimina una entidad existente de la base de datos.
        /// </summary>
        /// <param name="entidad">La entidad a eliminar.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        Task Eliminar(T entidad);

        void Dispose();
    }
}
