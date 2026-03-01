using BarberShop.Application.DTOs.Service;

namespace BarberShop.Application.Interfaces
{
    public interface IServiceService
    {
        Task<ServiceResponse> CreateAsync(CreateServiceRequest request);

        Task<IEnumerable<ServiceResponse>> GetByBusinessAsync(Guid businessId);

        Task<ServiceResponse?> GetByIdAsync(Guid id);

        Task<ServiceResponse?> UpdateAsync(Guid id, UpdateServiceRequest request);

        Task<bool> DeactivateAsync(Guid id);

        Task<bool> ActivateAsync(Guid id);
    }
}
