using BarberShop.Application.DTOs.Barber;
using BarberShop.Domain.Entities;

namespace BarberShop.Application.Mappings
{
    public static class BarberMapper
    {
        public static BarberResponse ToResponse(Barber barber)
        {
            return new BarberResponse
            {
                Id = barber.Id,
                FullName = barber.FullName,
                Phone = barber.Phone,
                Description = barber.Description,
                IsActive = barber.IsActive
            };
        }
    }
}