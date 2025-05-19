using AutoMapper;
using EntidadesApi.Application.DTOs;
using EntidadesApi.Application.Interfaces;
using EntidadesApi.Domain.Entities;
using EntidadesApi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesApi.Application.Services
{
    public class EntidadGubernamentalService : IEntidadGubernamentalService
    {
        private readonly IEntidadGubernamentalRepository _entidadRepository;
        private readonly IMapper _mapper;

        public EntidadGubernamentalService(IEntidadGubernamentalRepository entidadRepository, IMapper mapper)
        {
            _entidadRepository = entidadRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EntidadGubernamentalDto>> GetAllEntidadesAsync()
        {
            var entidades = await _entidadRepository.GetEntidadesWithRelationsAsync();
            return _mapper.Map<IEnumerable<EntidadGubernamentalDto>>(entidades);
        }

        public async Task<EntidadGubernamentalDto> GetEntidadByIdAsync(Guid id)
        {
            var entidad = await _entidadRepository.GetEntidadWithRelationsAsync(id);
            return _mapper.Map<EntidadGubernamentalDto>(entidad);
        }

        public async Task<EntidadGubernamentalDto> CreateEntidadAsync(EntidadGubernamentalCreateDto entidadDto)
        {
            var entidad = _mapper.Map<EntidadGubernamental>(entidadDto);
            entidad.Id = Guid.NewGuid();
            entidad.FechaRegistro = DateTime.Now;

            var result = await _entidadRepository.AddAsync(entidad);
            return _mapper.Map<EntidadGubernamentalDto>(result);
        }

        public async Task<bool> UpdateEntidadAsync(Guid id, EntidadGubernamentalUpdateDto entidadDto)
        {
            var existingEntidad = await _entidadRepository.GetByIdAsync(id);
            if (existingEntidad == null)
                return false;

            _mapper.Map(entidadDto, existingEntidad);
            existingEntidad.FechaModificacion = DateTime.Now;

            return await _entidadRepository.UpdateAsync(existingEntidad);
        }

        public async Task<bool> DeleteEntidadAsync(Guid id)
        {
            return await _entidadRepository.DeleteAsync(id);
        }

        public async Task<PagedResponse<EntidadGubernamentalDto>> SearchEntidadesAsync(EntidadGubernamentalFilterDto filterDto)
        {
            var entidades = await _entidadRepository.SearchEntidadesAsync(
                filterDto.SearchTerm,
                filterDto.SectorId,
                filterDto.TipoEntidadId,
                filterDto.Activo,
                filterDto.FechaCreacionDesde,
                filterDto.FechaCreacionHasta,
                filterDto.OrderBy,
                filterDto.OrderByDescending,
                filterDto.PageNumber,
                filterDto.PageSize);

            var count = await _entidadRepository.CountSearchResultsAsync(
                filterDto.SearchTerm,
                filterDto.SectorId,
                filterDto.TipoEntidadId,
                filterDto.Activo,
                filterDto.FechaCreacionDesde,
                filterDto.FechaCreacionHasta);

            var entidadesDto = _mapper.Map<EntidadGubernamentalDto[]>(entidades);

            return new PagedResponse<EntidadGubernamentalDto>(
                entidadesDto,
                count,
                filterDto.PageNumber,
                filterDto.PageSize);
        }
    }
}
