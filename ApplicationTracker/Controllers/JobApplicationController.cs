using ApplicationTracker.DTOs;
using ApplicationTracker.Models;
using ApplicationTracker.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationTracker.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class JobApplicationsController(IJobApplicationService service) : ControllerBase
    {
        // GET api/jobapplications
        // GET api/jobapplications?status=InterviewScheduled
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ApplicationStatus? status)
        {
            var results = await service.GetAllAsync(status);
            return Ok(results);
        }

        // GET api/jobapplications/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await service.GetByIdAsync(id);
            return result is null ? NotFound() : Ok(result);
        }

        // POST api/jobapplications
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateJobApplicationDto dto)
        {
            var created = await service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT api/jobapplications/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateJobApplicationDto dto)
        {
            var updated = await service.UpdateAsync(id, dto);
            return updated is null ? NotFound() : Ok(updated);
        }

        // DELETE api/jobapplications/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}