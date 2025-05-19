using EntidadesApi.Application.DTOs;
using EntidadesApi.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EntidadesApi.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EntidadesGubernamentalesController : ControllerBase
    {
        private readonly IEntidadGubernamentalService _entidadService;
        private readonly ILogger<EntidadesGubernamentalesController> _logger;

        public EntidadesGubernamentalesController(
            IEntidadGubernamentalService entidadService,
            ILogger<EntidadesGubernamentalesController> logger)
        {
            _entidadService = entidadService;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todas las entidades gubernamentales
        /// </summary>
        /// <returns>Lista de entidades gubernamentales</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EntidadGubernamentalDto>), 200)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var entidades = await _entidadService.GetAllEntidadesAsync();
                return Ok(entidades);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las entidades gubernamentales");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        /// <summary>
        /// Obtiene una entidad gubernamental por su ID
        /// </summary>
        /// <param name="id">ID de la entidad gubernamental</param>
        /// <returns>Entidad gubernamental</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EntidadGubernamentalDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var entidad = await _entidadService.GetEntidadByIdAsync(id);
                if (entidad == null)
                    return NotFound();

                return Ok(entidad);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener la entidad gubernamental con ID: {id}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        /// <summary>
        /// Crea una nueva entidad gubernamental
        /// </summary>
        /// <param name="entidadDto">Datos de la entidad gubernamental</param>
        /// <returns>Entidad gubernamental creada</returns>
        [HttpPost]
        [ProducesResponseType(typeof(EntidadGubernamentalDto), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] EntidadGubernamentalCreateDto entidadDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var entidad = await _entidadService.CreateEntidadAsync(entidadDto);
                return CreatedAtAction(nameof(GetById), new { id = entidad.Id }, entidad);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear una entidad gubernamental");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        /// <summary>
        /// Actualiza una entidad gubernamental existente
        /// </summary>
        /// <param name="id">ID de la entidad gubernamental</param>
        /// <param name="entidadDto">Datos actualizados de la entidad gubernamental</param>
        /// <returns>Resultado de la operación</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(Guid id, [FromBody] EntidadGubernamentalUpdateDto entidadDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _entidadService.UpdateEntidadAsync(id, entidadDto);
                if (!result)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar la entidad gubernamental con ID: {id}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        /// <summary>
        /// Elimina una entidad gubernamental
        /// </summary>
        /// <param name="id">ID de la entidad gubernamental</param>
        /// <returns>Resultado de la operación</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _entidadService.DeleteEntidadAsync(id);
                if (!result)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar la entidad gubernamental con ID: {id}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        /// <summary>
        /// Busca entidades gubernamentales con filtros y paginación
        /// </summary>
        /// <param name="filterDto">Filtros y parámetros de paginación</param>
        /// <returns>Lista paginada de entidades gubernamentales</returns>
        [HttpGet("search")]
        [ProducesResponseType(typeof(PagedResponse<EntidadGubernamentalDto>), 200)]
        public async Task<IActionResult> Search([FromQuery] EntidadGubernamentalFilterDto filterDto)
        {
            try
            {
                var result = await _entidadService.SearchEntidadesAsync(filterDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al buscar entidades gubernamentales");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
