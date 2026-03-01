using BarberShop.Application.DTOs.Barber;
using BarberShop.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BarberController : ControllerBase
    {
        private readonly IBarberService _service;

        public BarberController(IBarberService service)
        {
            _service = service;
        }


        [HttpGet("business/{businessId}")]
        public async Task<IActionResult> GetByBusiness(Guid businessId)
        {
            var result = await _service.GetByBusinessAsync(businessId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBarberRequest request)
        {
            var result = await _service.UpdateAsync(id, request);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}/deactivate")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            var success = await _service.DeactivateAsync(id);

            if (!success)
                return NotFound();

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}/activate")]
        public async Task<IActionResult> Activate(Guid id)
        {
            var success = await _service.ActivateAsync(id);

            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}