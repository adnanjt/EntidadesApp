using EntidadesApi.Application.DTOs;
using EntidadesApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EntidadesApi.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SectorController : ControllerBase
    {
        private readonly ISectorService _sectorService;

        public SectorController(ISectorService sectorService)
        {
            _sectorService = sectorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sectores = await _sectorService.GetAllAsync();
            return Ok(sectores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var sector = await _sectorService.GetByIdAsync(id);
            if (sector == null) return NotFound();
            return Ok(sector);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SectorDto sectorDto)
        {
            var created = await _sectorService.CreateAsync(sectorDto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] SectorDto sectorDto)
        {
            var success = await _sectorService.UpdateAsync(id, sectorDto);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _sectorService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
