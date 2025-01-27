using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Persistence.Repositorios
{
    public  class GenericRepository<TEntity>: IGenericRepository<TEntity> where TEntity : class
    {
        private readonly ControlEscolarXdbContext _controlEscolarXdbContext;
        public GenericRepository(ControlEscolarXdbContext controlEscolarXdbContext)
        {
            _controlEscolarXdbContext = controlEscolarXdbContext;
        }


        /// <summary>
        /// Agrega una nueva entidad a la base de datos.
        /// </summary>
        /// <param name="entidad">La entidad a agregar.</param>
        /// <returns>La entidad agregada.</returns>
        public async Task<TEntity?> Agregar(TEntity entidad)
        {
            await _controlEscolarXdbContext.Set<TEntity>().AddAsync(entidad);
            await _controlEscolarXdbContext.SaveChangesAsync();
            return entidad;
        }

        /// <summary>
        /// Obtiene una entidad por su identificador.
        /// </summary>
        /// <param name="id">El identificador de la entidad.</param>
        /// <returns>La entidad encontrada, o null si no se encuentra.</returns>
        public async Task<TEntity?> ObtenerPorId(int id)
        {
            return await _controlEscolarXdbContext.Set<TEntity>().FindAsync(id);
        }

        /// <summary>
        /// Obtiene una entidad por algun tipo de criterio de expresion LAMDA.
        /// </summary>
        /// <param name="id">El identificador de la entidad.</param>
        /// <returns>La entidad encontrada, o null si no se encuentra.</returns>
        public IQueryable<TEntity> ObtenerPorFiltro(Expression<Func<TEntity, bool>> criterio)
        {
            return _controlEscolarXdbContext.Set<TEntity>().AsQueryable().Where(criterio);
        }

        /// <summary>
        /// Obtiene todas las entidades de tipo <typeparamref name="T"/> de la base de datos.
        /// </summary>
        /// <returns>Una lista de solo lectura de todas las entidades.</returns>
        public async Task<IReadOnlyList<TEntity>> ObtenerTodos()
        {
            return await _controlEscolarXdbContext.Set<TEntity>().ToListAsync();
        }

        /// <summary>
        /// Obtiene el query de todas las entidades de tipo <typeparamref name="TEntity"/> de la base de datos.
        /// </summary>
        /// <returns>Un Query con todos los registros de la entidad </returns>
        public IQueryable<TEntity> ObtenerTodosAsQuery()
        {
            return _controlEscolarXdbContext.Set<TEntity>();
        }


        /***    Los IQueryble pueden ser asincronos segun lo necesitemos    ***/
        // Método para obtener con filtro y permite incluir entidades relacionadas
        /// <summary>
        /// Obtiene las entidades que cumplan con el criterio del a expresion lamda pero tambien las entidades que esten relacionados a ellas (Al usar con una especificacion podria ayudar a resolver las relaciones)
        /// </summary>
        /// <param name="criterio">Expresion lamda que se debe de cumplir</param>
        /// <param name="includes">Array de entidades relacionadas con la entidad a obtener </param>
        /// <returns></returns>
        public IQueryable<TEntity> ObtenerPorFiltroInclude(Expression<Func<TEntity, bool>> criterio, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _controlEscolarXdbContext.Set<TEntity>();

            // Aplicar los Includes si es necesario
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            // Retornar el IQueryable con los filtros aplicados
            return query.Where(criterio);
        }





        /// <summary>
        /// Actualiza una entidad existente en la base de datos.
        /// </summary>
        /// <param name="entidad">La entidad a actualizar.</param>
        /// <returns>La entidad actualizada.</returns>
        public async Task<TEntity?> Actualizar(TEntity entidad)
        {
            _controlEscolarXdbContext.Entry(entidad).State = EntityState.Modified;
            await _controlEscolarXdbContext.SaveChangesAsync();
            return entidad;
        }

 
        /// <summary>
        /// Elimina una entidad existente de la base de datos.
        /// </summary>
        /// <param name="entidad">La entidad a eliminar.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        public async Task<int> Eliminar(TEntity entidad)
        {
            _controlEscolarXdbContext.Remove(entidad);
            return await _controlEscolarXdbContext.SaveChangesAsync();
        }

        public async Task<int> EliminarLista(List<TEntity> listaItemEliminar)
        {
            _controlEscolarXdbContext.Set<TEntity>().RemoveRange(listaItemEliminar);
            return await _controlEscolarXdbContext.SaveChangesAsync();
        }


        /// <summary>
        /// Libera los recursos no administrados que el contexto está utilizando
        /// </summary>
        public void Dispose()
        {
            _controlEscolarXdbContext.Dispose();
        }

    }
}
