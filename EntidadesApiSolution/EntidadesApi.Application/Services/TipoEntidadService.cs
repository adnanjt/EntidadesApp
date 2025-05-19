using AutoMapper;
using EntidadesApi.Application.DTOs;
using EntidadesApi.Application.Interfaces;
using EntidadesApi.Domain.Entities;
using EntidadesApi.Domain.Interfaces;

namespace EntidadesApi.Application.Services
{
    public class TipoEntidadService : ITipoEntidadService
    {
        private readonly ITipoEntidadRepository _repository;
        private readonly IMapper _mapper;

        public TipoEntidadService(ITipoEntidadRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TipoEntidadDto>> GetAllAsync()
        {
            var tipos = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TipoEntidadDto>>(tipos);
        }

        public async Task<TipoEntidadDto> GetByIdAsync(Guid id)
        {
            var tipo = await _repository.GetByIdAsync(id);
            return _mapper.Map<TipoEntidadDto>(tipo);
        }

        public async Task<TipoEntidadDto> CreateAsync(TipoEntidadCreateDto tipoDto)
        {
            var tipo = _mapper.Map<TipoEntidad>(tipoDto);
            tipo.Id = Guid.NewGuid();
            tipo.FechaRegistro = DateTime.Now;

            var created = await _repository.AddAsync(tipo);
            return _mapper.Map<TipoEntidadDto>(created);
        }

        public async Task<bool> UpdateAsync(Guid id, TipoEntidadUpdateDto tipoDto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return false;

            _mapper.Map(tipoDto, existing);
            existing.FechaModificacion = DateTime.Now;

            return await _repository.UpdateAsync(existing);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
