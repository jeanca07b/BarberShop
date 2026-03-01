using BarberShop.Application.DTOs.Business;
using BarberShop.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BarberShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusinessRequestController : ControllerBase
    {
        private readonly BusinessRequestService _service;

        public BusinessRequestController(BusinessRequestService service)
        {
            _service = service;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateBusinessRequest request)
        {
            var userId = Guid.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            await _service.CreateRequestAsync(request, userId);

            return Ok();
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("pending")]
        public async Task<IActionResult> GetPending()
        {
            var result = await _service.GetPendingAsync();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}/approve")]
        public async Task<IActionResult> Approve(Guid id)
        {
            await _service.ApproveAsync(id);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}/reject")]
        public async Task<IActionResult> Reject(Guid id)
        {
            await _service.RejectAsync(id);
            return NoContent();
        }
    }
}
