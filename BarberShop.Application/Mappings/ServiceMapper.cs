using BarberShop.Application.DTOs.Service;
using BarberShop.Domain.Entities;

namespace BarberShop.Application.Mappings
{
    public static class ServiceMapper
    {
        public static Service ToEntity(CreateServiceRequest request)
        {
            return new Service(
                request.BusinessId,
                request.Name,
                request.Description,
                request.Price,
                request.DurationInMinutes
            );
        }

        public static ServiceResponse ToResponse(Service service)
        {
            return new ServiceResponse
            {
                Id = service.Id,
                BusinessId = service.BusinessId,
                Name = service.Name,
                Description = service.Description,
                Price = service.Price,
                DurationInMinutes = service.DurationInMinutes,
                IsActive = service.IsActive
            };
        }
    }
}