using BarberShop.Domain.Entities;

namespace BarberShop.Domain.Repositories
{
    public interface IServiceRepository
    {
        Task AddAsync(Service service);
        Task<Service?> GetByIdAsync(Guid id);
        Task<IEnumerable<Service>> GetByBusinessAsync(Guid businessId);
        Task SaveChangesAsync();
    }
}