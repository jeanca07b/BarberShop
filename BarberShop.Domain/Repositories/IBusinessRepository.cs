using BarberShop.Domain.Entities;

namespace BarberShop.Domain.Repositories
{
    public interface IBusinessRepository
    {
        Task AddAsync(Business business);
        Task<Business?> GetByIdAsync(Guid id);
        Task<List<Business>> GetAllAsync();
        void Update(Business business);
        void Remove(Business business);
        Task SaveChangesAsync();
    }
}