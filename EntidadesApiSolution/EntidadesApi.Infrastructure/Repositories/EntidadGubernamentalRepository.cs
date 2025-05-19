using EntidadesApi.Domain.Entities;
using EntidadesApi.Domain.Interfaces;
//using EntidadesApi.Infrastructure.Data;

namespace EntidadesApi.Infrastructure.Repositories
{
    public class EntidadGubernamentalRepository : Repository<EntidadGubernamental>, IEntidadGubernamentalRepository
    {
        private readonly ITipoEntidadRepository _tipoEntidadRepository;
        private readonly ISectorRepository _sectorRepository;

        public EntidadGubernamentalRepository(
            string filePath,
            ITipoEntidadRepository tipoEntidadRepository,
            ISectorRepository sectorRepository) : base(filePath)
        {
            _tipoEntidadRepository = tipoEntidadRepository;
            _sectorRepository = sectorRepository;
        }

        public async Task<IEnumerable<EntidadGubernamental>> GetEntidadesWithRelationsAsync()
        {
            var entidades = await GetAllAsync();
            //var tipos = await _tipoEntidadRepository.GetAllAsync();
            //var sectores = await _sectorRepository.GetAllAsync();

            //foreach (var entidad in entidades)
            //{
            //    entidad.TipoEntidad = tipos.FirstOrDefault(t => t.Id == entidad.TipoEntidadId);
            //    entidad.Sector = sectores.FirstOrDefault(s => s.Id == entidad.SectorId);
            //}

            return entidades;
        }

        public async Task<EntidadGubernamental> GetEntidadWithRelationsAsync(Guid id)
        {
            var entidad = await GetByIdAsync(id);
            if (entidad == null)
                return null;

            //entidad.TipoEntidad = await _tipoEntidadRepository.GetByIdAsync(entidad.TipoEntidadId);
            //entidad.Sector = await _sectorRepository.GetByIdAsync(entidad.SectorId);

            return entidad;
        }

        public async Task<IEnumerable<EntidadGubernamental>> SearchEntidadesAsync(
            string searchTerm = null,
            Guid? sectorId = null,
            Guid? tipoEntidadId = null,
            bool? activo = null,
            DateTime? fechaCreacionDesde = null,
            DateTime? fechaCreacionHasta = null,
            string orderBy = "Nombre",
            bool orderByDescending = false,
            int pageNumber = 1,
            int pageSize = 10)
        {
            IQueryable<EntidadGubernamental> query = _entities.AsQueryable();

            // Aplicar filtros
            query = ApplyFilters(query, searchTerm, sectorId, tipoEntidadId, activo, fechaCreacionDesde, fechaCreacionHasta);

            // Aplicar ordenamiento
            query = ApplyOrdering(query, orderBy, orderByDescending);

            // Aplicar paginación
            var result = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Cargar relaciones
            //var tipos = await _tipoEntidadRepository.GetAllAsync();
            //var sectores = await _sectorRepository.GetAllAsync();

            //foreach (var entidad in result)
            //{
            //    entidad.TipoEntidad = tipos.FirstOrDefault(t => t.Id == entidad.TipoEntidadId);
            //    entidad.Sector = sectores.FirstOrDefault(s => s.Id == entidad.SectorId);
            //}

            return result;
        }

        public async Task<int> CountSearchResultsAsync(
            string searchTerm = null,
            Guid? sectorId = null,
            Guid? tipoEntidadId = null,
            bool? activo = null,
            DateTime? fechaCreacionDesde = null,
            DateTime? fechaCreacionHasta = null)
        {
            IQueryable<EntidadGubernamental> query = _entities.AsQueryable();

            // Aplicar filtros
            query = ApplyFilters(query, searchTerm, sectorId, tipoEntidadId, activo, fechaCreacionDesde, fechaCreacionHasta);

            return await Task.FromResult(query.Count());
        }

        private IQueryable<EntidadGubernamental> ApplyFilters(
            IQueryable<EntidadGubernamental> query,
            string searchTerm = null,
            Guid? sectorId = null,
            Guid? tipoEntidadId = null,
            bool? activo = null,
            DateTime? fechaCreacionDesde = null,
            DateTime? fechaCreacionHasta = null)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(e =>
                    e.Nombre.ToLower().Contains(searchTerm) ||
                    e.Siglas.ToLower().Contains(searchTerm) ||
                    e.Descripcion.ToLower().Contains(searchTerm) ||
                    e.Direccion.ToLower().Contains(searchTerm) ||
                    e.CorreoElectronico.ToLower().Contains(searchTerm) ||
                    e.Telefono.Contains(searchTerm)
                );
            }

            //if (sectorId.HasValue)
            //{
            //    query = query.Where(e => e.SectorId == sectorId.Value);
            //}

            //if (tipoEntidadId.HasValue)
            //{
            //    query = query.Where(e => e.TipoEntidadId == tipoEntidadId.Value);
            //}

            if (activo.HasValue)
            {
                query = query.Where(e => e.Activo == activo.Value);
            }

            if (fechaCreacionDesde.HasValue)
            {
                query = query.Where(e => e.FechaCreacion >= fechaCreacionDesde.Value);
            }

            if (fechaCreacionHasta.HasValue)
            {
                query = query.Where(e => e.FechaCreacion <= fechaCreacionHasta.Value);
            }

            return query;
        }

        private IQueryable<EntidadGubernamental> ApplyOrdering(
            IQueryable<EntidadGubernamental> query,
            string orderBy,
            bool orderByDescending)
        {
            IOrderedQueryable<EntidadGubernamental> orderedQuery = null;

            switch (orderBy.ToLower())
            {
                case "nombre":
                    orderedQuery = orderByDescending
                        ? query.OrderByDescending(e => e.Nombre)
                        : query.OrderBy(e => e.Nombre);
                    break;
                case "siglas":
                    orderedQuery = orderByDescending
                        ? query.OrderByDescending(e => e.Siglas)
                        : query.OrderBy(e => e.Siglas);
                    break;
                //case "tipoentidad":
                //    orderedQuery = orderByDescending
                //        ? query.OrderByDescending(e => e.TipoEntidadId)
                //        : query.OrderBy(e => e.TipoEntidadId);
                //    break;
                //case "sector":
                //    orderedQuery = orderByDescending
                //        ? query.OrderByDescending(e => e.SectorId)
                //        : query.OrderBy(e => e.SectorId);
                //    break;
                case "fechacreacion":
                    orderedQuery = orderByDescending
                        ? query.OrderByDescending(e => e.FechaCreacion)
                        : query.OrderBy(e => e.FechaCreacion);
                    break;
                case "fecharegistro":
                    orderedQuery = orderByDescending
                        ? query.OrderByDescending(e => e.FechaRegistro)
                        : query.OrderBy(e => e.FechaRegistro);
                    break;
                default:
                    orderedQuery = orderByDescending
                        ? query.OrderByDescending(e => e.Nombre)
                        : query.OrderBy(e => e.Nombre);
                    break;
            }

            return orderedQuery;
        }
    }
}
