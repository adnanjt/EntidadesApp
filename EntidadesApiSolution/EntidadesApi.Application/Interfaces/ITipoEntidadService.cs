using EntidadesApi.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EntidadesApi.Application.Interfaces
{
    public interface ITipoEntidadService
    {
        Task<IEnumerable<TipoEntidadDto>> GetAllAsync();
        Task<TipoEntidadDto> GetByIdAsync(Guid id);
        Task<TipoEntidadDto> CreateAsync(TipoEntidadCreateDto tipoDto);
        Task<bool> UpdateAsync(Guid id, TipoEntidadUpdateDto tipoDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
