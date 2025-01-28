using Application.Interfaces;
using Ardalis.Specification.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositorios
{
    public class ArdalisRepository<TEntity> : RepositoryBase<TEntity>, IArdalisRepository<TEntity> where TEntity : class
    {
        private readonly ControlEscolarXdbContext _controlEscolarXdbContext;
       
        public ArdalisRepository(ControlEscolarXdbContext controlEscolarXdbContext) : base(controlEscolarXdbContext)
        {
            _controlEscolarXdbContext = controlEscolarXdbContext;
        }

    }
}
