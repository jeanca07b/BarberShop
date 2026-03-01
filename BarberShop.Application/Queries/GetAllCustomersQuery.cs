using BarberShop.Application.DTOs.Customer;
using BarberShop.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.Queries
{
    public class GetAllCustomersQuery
    {
        private readonly ICustomerRepository _repository;

        public GetAllCustomersQuery(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CustomerResponse>> ExecuteAsync()
        {
            var customers = await _repository.GetAllAsync();

            return customers.Select(c => new CustomerResponse
            {
                Id = c.Id,
                FullName = $"{c.FirstName} {c.LastName}",
                Email = c.Email.Value,
                PhoneNumber = c.PhoneNumber.Value
            }).ToList();
        }
    }
}
