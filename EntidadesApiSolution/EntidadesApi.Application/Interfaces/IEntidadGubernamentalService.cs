using EntidadesApi.Application.DTOs;

namespace EntidadesApi.Application.Interfaces
{
    public interface IEntidadGubernamentalService
    {
        Task<IEnumerable<EntidadGubernamentalDto>> GetAllEntidadesAsync();
        Task<EntidadGubernamentalDto> GetEntidadByIdAsync(Guid id);
        Task<EntidadGubernamentalDto> CreateEntidadAsync(EntidadGubernamentalCreateDto entidadDto);
        Task<bool> UpdateEntidadAsync(Guid id, EntidadGubernamentalUpdateDto entidadDto);
        Task<bool> DeleteEntidadAsync(Guid id);
        Task<PagedResponse<EntidadGubernamentalDto>> SearchEntidadesAsync(EntidadGubernamentalFilterDto filterDto);
    }
}
