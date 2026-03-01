using BarberShop.API.Mappings;
using BarberShop.Application.DTOs.Business;
using BarberShop.Domain.Entities;
using BarberShop.Domain.Repositories;
using BarberShop.Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BarberShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusinessesController : ControllerBase
    {
        private readonly IBusinessRepository _repository;

        public BusinessesController(IBusinessRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var businesses = await _repository.GetAllAsync();
            return Ok(businesses.Select(b => b.ToResponse()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var business = await _repository.GetByIdAsync(id);

            if (business is null)
                return NotFound();

            return Ok(business.ToResponse());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateBusinessRequest request)
        {
            var address = new Address(
                request.Street,
                request.City,
                request.State,
                request.Country,
                request.Latitude,
                request.Longitude,
                request.PlaceId
            );

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim is null)
                return Unauthorized();

            var ownerUserId = Guid.Parse(userIdClaim);

            var business = new Business(
                request.Name,
                request.Description,
                address,
                request.Phone,
                ownerUserId
            );

            await _repository.AddAsync(business);
            await _repository.SaveChangesAsync();

            return Ok(business.Id);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateBusinessRequest request)
        {
            var business = await _repository.GetByIdAsync(id);

            if (business is null)
                return NotFound();

            var address = new Address(
                request.Street,
                request.City,
                request.State,
                request.Country,
                request.Latitude,
                request.Longitude,
                request.PlaceId
            );

            business.Update(
                request.Name,
                request.Description,
                request.Phone,
                address
            );

            _repository.Update(business);
            await _repository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var business = await _repository.GetByIdAsync(id);

            if (business is null)
                return NotFound();

            business.Deactivate();

            _repository.Update(business);
            await _repository.SaveChangesAsync();

            return NoContent();
        }


    }


}
