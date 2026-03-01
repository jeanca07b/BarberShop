using BarberShop.Application.DTOs.Business;
using BarberShop.Domain.Entities;

namespace BarberShop.API.Mappings
{
    public static class BusinessMapper
    {
        public static BusinessResponse ToResponse(this Business business)
        {
            return new BusinessResponse
            {
                Id = business.Id,
                Name = business.Name,
                Description = business.Description,
                Street = business.Address.Street,
                City = business.Address.City,
                State = business.Address.State,
                Country = business.Address.Country,
                Latitude = business.Address.Latitude,
                Longitude = business.Address.Longitude,
                Phone = business.Phone
            };
        }
    }
}
