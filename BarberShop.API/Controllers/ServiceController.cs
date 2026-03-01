using BarberShop.Application.DTOs.Service;
using BarberShop.Application.Interfaces;
using BarberShop.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _service;

        public ServiceController(IServiceService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateServiceRequest request)
        {
            var result = await _service.CreateAsync(request);
            return Ok(result);
        }

        [HttpGet("business/{businessId}")]
        public async Task<IActionResult> GetByBusiness(Guid businessId)
        {
            var services = await _service.GetByBusinessAsync(businessId);
            return Ok(services);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var service = await _service.GetByIdAsync(id);

            if (service is null)
                return NotFound();

            return Ok(service);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateServiceRequest request)
        {
            var updated = await _service.UpdateAsync(id, request);

            if (updated is null)
                return NotFound();

            return Ok(updated);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            var success = await _service.DeactivateAsync(id);

            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}