using BarberShop.Application.DTOs;
using BarberShop.Domain.Repositories;

namespace BarberShop.Application.Queries
{
    public class GetCustomerByIdQuery
    {
        private readonly ICustomerRepository _repository;

        public GetCustomerByIdQuery(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<CustomerDto?> ExecuteAsync(Guid id)
        {
            var customer = await _repository.GetByIdAsync(id);

            if (customer is null)
                return null;

            return new CustomerDto
            {
                Id = customer.Id,
                FirstName = $"{customer.FirstName}",
                LastName = $"{customer.LastName}",
                Email = customer.Email.Value
            };
        }
    }
}
