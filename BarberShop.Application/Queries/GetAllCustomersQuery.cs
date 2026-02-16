using BarberShop.Application.DTOs;
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

        public async Task<List<CustomerDto>> ExecuteAsync()
        {
            var customers = await _repository.GetAllAsync();

            return customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email.Value
            }).ToList();
        }
    }
}
