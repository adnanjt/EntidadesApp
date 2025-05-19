
using EntidadesApi.Application.DTOs;
using EntidadesApi.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EntidadesApi.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TiposEntidadController : ControllerBase
    {
        private readonly ITipoEntidadService _tipoEntidadService;
        private readonly ILogger<TiposEntidadController> _logger;

        public TiposEntidadController(
            ITipoEntidadService tipoEntidadService,
            ILogger<TiposEntidadController> logger)
        {
            _tipoEntidadService = tipoEntidadService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TipoEntidadDto>), 200)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var tipos = await _tipoEntidadService.GetAllAsync();
                return Ok(tipos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los tipos de entidad");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TipoEntidadDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var tipo = await _tipoEntidadService.GetByIdAsync(id);
                if (tipo == null)
                    return NotFound();

                return Ok(tipo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el tipo de entidad con ID: {id}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(TipoEntidadDto), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] TipoEntidadCreateDto tipoDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var tipo = await _tipoEntidadService.CreateAsync(tipoDto);
                return CreatedAtAction(nameof(GetById), new { id = tipo.Id }, tipo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear un tipo de entidad");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(Guid id, [FromBody] TipoEntidadUpdateDto tipoDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _tipoEntidadService.UpdateAsync(id, tipoDto);
                if (!result)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar el tipo de entidad con ID: {id}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _tipoEntidadService.DeleteAsync(id);
                if (!result)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar el tipo de entidad con ID: {id}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        //[HttpGet("search")]
        //[ProducesResponseType(typeof(PagedResponse<TipoEntidadDto>), 200)]
        //public async Task<IActionResult> Search([FromQuery] TipoEntidadFilterDto filterDto)
        //{
        //    try
        //    {
        //        var result = await _tipoEntidadService.SearchTipAsync(filterDto);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error al buscar tipos de entidad");
        //        return StatusCode(500, "Error interno del servidor");
        //    }
        //}
    }
}
