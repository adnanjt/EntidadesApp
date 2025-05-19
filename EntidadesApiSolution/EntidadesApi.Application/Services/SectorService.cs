using AutoMapper;
using EntidadesApi.Application.DTOs;
using EntidadesApi.Application.Interfaces;
using EntidadesApi.Domain.Entities;
using EntidadesApi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EntidadesApi.Application.Services
{
    public class SectorService : ISectorService
    {
        private readonly ISectorRepository _repository;
        private readonly IMapper _mapper;

        public SectorService(ISectorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SectorDto>> GetAllAsync()
        {
            var sectores = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<SectorDto>>(sectores);
        }

        public async Task<SectorDto> GetByIdAsync(Guid id)
        {
            var sector = await _repository.GetByIdAsync(id);
            return _mapper.Map<SectorDto>(sector);
        }

        public async Task<SectorDto> CreateAsync(SectorDto sectorDto)
        {
            var sector = _mapper.Map<Sector>(sectorDto);
            sector.Id = Guid.NewGuid();
            sector.FechaRegistro = DateTime.Now;

            var created = await _repository.AddAsync(sector);
            return _mapper.Map<SectorDto>(created);
        }

        public async Task<bool> UpdateAsync(Guid id, SectorDto sectorDto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return false;

            _mapper.Map(sectorDto, existing);
            existing.FechaModificacion = DateTime.Now;

            return await _repository.UpdateAsync(existing);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
