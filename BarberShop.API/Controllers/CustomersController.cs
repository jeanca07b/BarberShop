using BarberShop.Application.DTOs.Customer;
using BarberShop.Application.Queries;
using BarberShop.Domain.Entities;
using BarberShop.Domain.Repositories;
using BarberShop.Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly GetAllCustomersQuery _getAllCustomers;
        private readonly ICustomerRepository _customerRepository;
        private readonly GetCustomerByIdQuery _getCustomerById;

        public CustomersController(
            GetAllCustomersQuery getAllCustomers,
            GetCustomerByIdQuery getCustomerById,
            ICustomerRepository customerRepository)
        {
            _getAllCustomers = getAllCustomers;
            _getCustomerById = getCustomerById;
            _customerRepository = customerRepository;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _getAllCustomers.ExecuteAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var customer = await _getCustomerById.ExecuteAsync(id);

            if (customer is null)
                return NotFound();

            return Ok(customer);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateCustomerRequest request)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer is null)
                return NotFound();

            customer.UpdateName(request.FirstName, request.LastName);
            customer.UpdateEmail(Email.Create(request.Email));
            customer.UpdatePhone(PhoneNumber.Create(request.PhoneNumber));

            await _customerRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer is null)
                return NotFound();

            _customerRepository.Remove(customer);
            await _customerRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}