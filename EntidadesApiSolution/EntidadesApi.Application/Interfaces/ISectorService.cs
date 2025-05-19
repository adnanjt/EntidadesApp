using EntidadesApi.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EntidadesApi.Application.Interfaces
{
    public interface ISectorService
    {
        Task<IEnumerable<SectorDto>> GetAllAsync();
        Task<SectorDto> GetByIdAsync(Guid id);
        Task<SectorDto> CreateAsync(SectorDto sectorDto);
        Task<bool> UpdateAsync(Guid id, SectorDto sectorDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
