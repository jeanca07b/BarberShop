using BarberShop.Application.DTOs.Service;
using BarberShop.Application.Interfaces;
using BarberShop.Application.Mappings;
using BarberShop.Domain.Repositories;

namespace BarberShop.Application.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _repository;

        public ServiceService(IServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse> CreateAsync(CreateServiceRequest request)
        {
            var service = ServiceMapper.ToEntity(request);

            await _repository.AddAsync(service);
            await _repository.SaveChangesAsync();

            return ServiceMapper.ToResponse(service);
        }

        public async Task<IEnumerable<ServiceResponse>> GetByBusinessAsync(Guid businessId)
        {
            var services = await _repository.GetByBusinessAsync(businessId);

            return services.Select(ServiceMapper.ToResponse);
        }

        public async Task<ServiceResponse?> GetByIdAsync(Guid id)
        {
            var service = await _repository.GetByIdAsync(id);

            if (service is null)
                return null;

            return ServiceMapper.ToResponse(service);
        }

        public async Task<ServiceResponse?> UpdateAsync(Guid id, UpdateServiceRequest request)
        {
            var service = await _repository.GetByIdAsync(id);

            if (service is null)
                return null;

            service.Update(
                request.Name,
                request.Description,
                request.Price,
                request.DurationInMinutes
            );

            await _repository.SaveChangesAsync();

            return ServiceMapper.ToResponse(service);
        }

        public async Task<bool> DeactivateAsync(Guid id)
        {
            var service = await _repository.GetByIdAsync(id);

            if (service is null)
                return false;

            service.Deactivate();

            await _repository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ActivateAsync(Guid id)
        {
            var service = await _repository.GetByIdAsync(id);

            if (service is null)
                return false;

            service.Activate();

            await _repository.SaveChangesAsync();

            return true;
        }
    }
}