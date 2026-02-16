using BarberShop.API.DTOs;
using BarberShop.Application.Queries;
using BarberShop.Domain.Entities;
using BarberShop.Domain.Repositories;
using BarberShop.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using BarberShop.API.Mappings;


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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _getAllCustomers.ExecuteAsync();

            var response = customers.Select(c => c.ToResponse());

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerRequest request)
        {
            var email = Email.Create(request.Email);

            var customer = new Customer(
                request.FirstName,
                request.LastName,
                email
            );

            await _customerRepository.AddAsync(customer);
            await _customerRepository.SaveChangesAsync();

            return Ok(customer.Id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var customer = await _getCustomerById.ExecuteAsync(id);

            if (customer is null)
                return NotFound();

            return Ok(customer.ToResponse());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateCustomerRequest request)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer is null)
                return NotFound();

            customer.UpdateName(request.FirstName, request.LastName);

            var email = Email.Create(request.Email);
            customer.UpdateEmail(email);

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
