using HybridWork.Dtos;
using HybridWork.Models;
using HybridWork.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HybridWork.Controllers.V1
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WorkspaceReservationsController : ControllerBase
    {
        private readonly WorkspaceReservationService _service;

        public WorkspaceReservationsController(WorkspaceReservationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkspaceReservationReadDto>>> GetAll()
        {
            var list = await _service.GetAllAsync();
            var dtos = list.Select(r => new WorkspaceReservationReadDto
            {
                Id = r.Id,
                EmployeeId = r.EmployeeId,
                WorkstationId = r.WorkstationId,
                Date = r.Date,
                StartTime = r.StartTime,
                EndTime = r.EndTime,
                Status = r.Status
            });
            return Ok(dtos);
        }

        [HttpGet("{id:length(24)}", Name = "GetReservationById")]
        public async Task<ActionResult<WorkspaceReservationReadDto>> GetById(string id)
        {
            var r = await _service.GetByIdAsync(id);
            if (r == null) return NotFound();

            var dto = new WorkspaceReservationReadDto
            {
                Id = r.Id,
                EmployeeId = r.EmployeeId,
                WorkstationId = r.WorkstationId,
                Date = r.Date,
                StartTime = r.StartTime,
                EndTime = r.EndTime,
                Status = r.Status
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<WorkspaceReservationReadDto>> Create([FromBody] WorkspaceReservationCreateDto createDto)
        {
            if (createDto == null) return BadRequest();

            var r = new WorkspaceReservation
            {
                EmployeeId = createDto.EmployeeId,
                WorkstationId = createDto.WorkstationId,
                Date = createDto.Date,
                StartTime = createDto.StartTime,
                EndTime = createDto.EndTime,
                Status = "Confirmed"
            };

            var created = await _service.CreateAsync(r);

            var readDto = new WorkspaceReservationReadDto
            {
                Id = created.Id,
                EmployeeId = created.EmployeeId,
                WorkstationId = created.WorkstationId,
                Date = created.Date,
                StartTime = created.StartTime,
                EndTime = created.EndTime,
                Status = created.Status
            };

            return CreatedAtRoute("GetReservationById", new { id = created.Id }, readDto);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, [FromBody] WorkspaceReservationUpdateDto updateDto)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null) return NotFound();

            if (!string.IsNullOrWhiteSpace(updateDto.EmployeeId)) existing.EmployeeId = updateDto.EmployeeId;
            if (!string.IsNullOrWhiteSpace(updateDto.WorkstationId)) existing.WorkstationId = updateDto.WorkstationId;
            if (updateDto.Date.HasValue) existing.Date = updateDto.Date.Value;
            if (updateDto.StartTime.HasValue) existing.StartTime = updateDto.StartTime.Value;
            if (updateDto.EndTime.HasValue) existing.EndTime = updateDto.EndTime.Value;
            if (!string.IsNullOrWhiteSpace(updateDto.Status)) existing.Status = updateDto.Status;

            var success = await _service.UpdateAsync(id, existing);
            if (!success) return StatusCode(500, "Failed to update the reservation.");

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null) return NotFound();

            var success = await _service.DeleteAsync(id);
            if (!success) return StatusCode(500, "Failed to delete the reservation.");

            return NoContent();
        }
    }
}
