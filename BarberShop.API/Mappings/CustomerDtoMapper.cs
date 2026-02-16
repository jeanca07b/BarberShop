using BarberShop.API.DTOs;
using BarberShop.Application.DTOs;
using BarberShop.Domain.Entities;

namespace BarberShop.API.Mappings
{
    public static class CustomerDtoMapper
    {
        public static CustomerResponse ToResponse(this CustomerDto dto)
        {
            return new CustomerResponse
            {
                Id = dto.Id,
                FullName = $"{dto.FirstName} {dto.LastName}",
                Email = dto.Email
            };
        }
    }
}
