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
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeService _service;

        public EmployeesController(EmployeeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeReadDto>>> GetAll()
        {
            var list = await _service.GetAllAsync();
            var dtos = list.Select(e => new EmployeeReadDto
            {
                Id = e.Id,
                Name = e.Name,
                Email = e.Email,
                Department = e.Department,
                Role = e.Role
            });
            return Ok(dtos);
        }

        [HttpGet("{id:length(24)}", Name = "GetEmployeeById")]
        public async Task<ActionResult<EmployeeReadDto>> GetById(string id)
        {
            var e = await _service.GetByIdAsync(id);
            if (e == null) return NotFound();
            var dto = new EmployeeReadDto
            {
                Id = e.Id,
                Name = e.Name,
                Email = e.Email,
                Department = e.Department,
                Role = e.Role
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeReadDto>> Create([FromBody] EmployeeCreateDto createDto)
        {
            if (createDto == null) return BadRequest();

            var e = new Employee
            {
                Name = createDto.Name,
                Email = createDto.Email,
                Department = createDto.Department,
                Role = createDto.Role
            };

            var created = await _service.CreateAsync(e);

            var readDto = new EmployeeReadDto
            {
                Id = created.Id,
                Name = created.Name,
                Email = created.Email,
                Department = created.Department,
                Role = created.Role
            };

            return CreatedAtRoute("GetEmployeeById", new { id = created.Id }, readDto);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, [FromBody] EmployeeUpdateDto updateDto)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null) return NotFound();

            if (!string.IsNullOrWhiteSpace(updateDto.Name)) existing.Name = updateDto.Name;
            if (!string.IsNullOrWhiteSpace(updateDto.Email)) existing.Email = updateDto.Email;
            if (!string.IsNullOrWhiteSpace(updateDto.Department)) existing.Department = updateDto.Department;
            if (!string.IsNullOrWhiteSpace(updateDto.Role)) existing.Role = updateDto.Role;

            var success = await _service.UpdateAsync(id, existing);
            if (!success) return StatusCode(500, "Failed to update the employee.");

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null) return NotFound();

            var success = await _service.DeleteAsync(id);
            if (!success) return StatusCode(500, "Failed to delete the employee.");

            return NoContent();
        }
    }
}
