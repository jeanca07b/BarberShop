using BarberShop.Application.DTOs.Customer;
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

        public async Task<CustomerResponse?> ExecuteAsync(Guid id)
        {
            var customer = await _repository.GetByIdAsync(id);

            if (customer is null)
                return null;

            return new CustomerResponse
            {
                Id = customer.Id,
                FullName = $"{customer.FirstName} {customer.LastName}",
                Email = customer.Email.Value,
                PhoneNumber = customer.PhoneNumber.Value
            };
        }
    }
}
