using EntidadesApi.Domain.Entities;
using EntidadesApi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EntidadesApi.Infrastructure.Repositories
{
    public class TipoEntidadRepository : Repository<TipoEntidad>, ITipoEntidadRepository
    {
        public TipoEntidadRepository(string filePath) : base(filePath)
        {
        }
    }
}
