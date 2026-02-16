using BarberShop.API.DTOs;
using BarberShop.Domain.Entities;

namespace BarberShop.API.Mappings
{
    public static class CustomerMapper
    {
        public static CustomerResponse ToResponse(this Customer customer)
        {
            return new CustomerResponse
            {
                Id = customer.Id,
                FullName = $"{customer.FirstName} {customer.LastName}",
                Email = customer.Email.Value
            };
        }
    }
}
