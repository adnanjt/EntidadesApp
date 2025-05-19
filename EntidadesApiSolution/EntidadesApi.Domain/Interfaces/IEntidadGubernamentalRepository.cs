using EntidadesApi.Domain.Entities;

namespace EntidadesApi.Domain.Interfaces
{
    public interface IEntidadGubernamentalRepository : IRepository<EntidadGubernamental>
    {
        Task<IEnumerable<EntidadGubernamental>> GetEntidadesWithRelationsAsync();
        Task<EntidadGubernamental> GetEntidadWithRelationsAsync(Guid id);
        Task<IEnumerable<EntidadGubernamental>> SearchEntidadesAsync(
            string searchTerm = null,
            Guid? sectorId = null,
            Guid? tipoEntidadId = null,
            bool? activo = null,
            DateTime? fechaCreacionDesde = null,
            DateTime? fechaCreacionHasta = null,
            string orderBy = "Nombre",
            bool orderByDescending = false,
            int pageNumber = 1,
            int pageSize = 10);

        Task<int> CountSearchResultsAsync(
            string searchTerm = null,
            Guid? sectorId = null,
            Guid? tipoEntidadId = null,
            bool? activo = null,
            DateTime? fechaCreacionDesde = null,
            DateTime? fechaCreacionHasta = null);
    }
}
