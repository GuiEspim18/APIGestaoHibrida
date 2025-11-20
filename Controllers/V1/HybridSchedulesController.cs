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
    public class HybridSchedulesController : ControllerBase
    {
        private readonly HybridScheduleService _service;

        public HybridSchedulesController(HybridScheduleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HybridScheduleReadDto>>> GetAll()
        {
            var list = await _service.GetAllAsync();
            var dtos = list.Select(s => new HybridScheduleReadDto
            {
                Id = s.Id,
                EmployeeId = s.EmployeeId,
                DayOfWeek = s.DayOfWeek,
                WorkMode = s.WorkMode
            });
            return Ok(dtos);
        }

        [HttpGet("{id:length(24)}", Name = "GetScheduleById")]
        public async Task<ActionResult<HybridScheduleReadDto>> GetById(string id)
        {
            var s = await _service.GetByIdAsync(id);
            if (s == null) return NotFound();

            var dto = new HybridScheduleReadDto
            {
                Id = s.Id,
                EmployeeId = s.EmployeeId,
                DayOfWeek = s.DayOfWeek,
                WorkMode = s.WorkMode
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<HybridScheduleReadDto>> Create([FromBody] HybridScheduleCreateDto createDto)
        {
            if (createDto == null) return BadRequest();

            var s = new HybridSchedule
            {
                EmployeeId = createDto.EmployeeId,
                DayOfWeek = createDto.DayOfWeek,
                WorkMode = createDto.WorkMode
            };

            var created = await _service.CreateAsync(s);

            var readDto = new HybridScheduleReadDto
            {
                Id = created.Id,
                EmployeeId = created.EmployeeId,
                DayOfWeek = created.DayOfWeek,
                WorkMode = created.WorkMode
            };

            return CreatedAtRoute("GetScheduleById", new { id = created.Id }, readDto);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, [FromBody] HybridScheduleUpdateDto updateDto)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null) return NotFound();

            if (!string.IsNullOrWhiteSpace(updateDto.DayOfWeek)) existing.DayOfWeek = updateDto.DayOfWeek;
            if (!string.IsNullOrWhiteSpace(updateDto.WorkMode)) existing.WorkMode = updateDto.WorkMode;

            var success = await _service.UpdateAsync(id, existing);
            if (!success) return StatusCode(500, "Failed to update the schedule.");

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null) return NotFound();

            var success = await _service.DeleteAsync(id);
            if (!success) return StatusCode(500, "Failed to delete the schedule.");

            return NoContent();
        }
    }
}
