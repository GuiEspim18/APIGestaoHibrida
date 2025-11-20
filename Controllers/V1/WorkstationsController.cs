using HybridWork.Dtos;
using HybridWork.Models;
using HybridWork.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HybridWork.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WorkstationsController : ControllerBase
    {
        private readonly WorkstationService _service;

        public WorkstationsController(WorkstationService service)
        {
            _service = service;
        }

        // GET: api/v1/workstations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkstationReadDto>>> GetAll()
        {
            var list = await _service.GetAllAsync();
            var dtos = list.Select(w => new WorkstationReadDto
            {
                Id = w.Id,
                Name = w.Name,
                Location = w.Location,
                Status = w.Status,
                Capacity = w.Capacity
            });

            return Ok(dtos);
        }

        // GET: api/v1/workstations/{id}
        [HttpGet("{id:length(24)}", Name = "GetWorkstationById")]
        public async Task<ActionResult<WorkstationReadDto>> GetById(string id)
        {
            var w = await _service.GetByIdAsync(id);
            if (w == null) return NotFound();

            var dto = new WorkstationReadDto
            {
                Id = w.Id,
                Name = w.Name,
                Location = w.Location,
                Status = w.Status,
                Capacity = w.Capacity
            };

            return Ok(dto);
        }

        // POST: api/v1/workstations
        [HttpPost]
        public async Task<ActionResult<WorkstationReadDto>> Create([FromBody] WorkstationCreateDto createDto)
        {
            if (createDto == null) return BadRequest();

            var w = new Workstation
            {
                Name = createDto.Name,
                Location = createDto.Location,
                Capacity = createDto.Capacity,
                Status = "Available"
            };

            var created = await _service.CreateAsync(w);

            var readDto = new WorkstationReadDto
            {
                Id = created.Id,
                Name = created.Name,
                Location = created.Location,
                Status = created.Status,
                Capacity = created.Capacity
            };

            // Return 201 Created with location header
            return CreatedAtRoute("GetWorkstationById", new { id = created.Id }, readDto);
        }

        // PUT: api/v1/workstations/{id}
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, [FromBody] WorkstationUpdateDto updateDto)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null) return NotFound();

            // apply updates (partial update support)
            if (!string.IsNullOrWhiteSpace(updateDto.Name)) existing.Name = updateDto.Name;
            if (!string.IsNullOrWhiteSpace(updateDto.Location)) existing.Location = updateDto.Location;
            if (!string.IsNullOrWhiteSpace(updateDto.Status)) existing.Status = updateDto.Status;
            if (updateDto.Capacity.HasValue) existing.Capacity = updateDto.Capacity.Value;

            var success = await _service.UpdateAsync(id, existing);
            if (!success) return StatusCode(500, "Failed to update the workstation.");

            return NoContent(); // 204
        }

        // DELETE: api/v1/workstations/{id}
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null) return NotFound();

            var success = await _service.DeleteAsync(id);
            if (!success) return StatusCode(500, "Failed to delete the workstation.");

            return NoContent(); // 204
        }
    }
}
