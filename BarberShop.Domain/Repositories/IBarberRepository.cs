using BarberShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Domain.Repositories
{
    public interface IBarberRepository
    {
        Task AddAsync(Barber barber);
        Task<Barber?> GetByIdAsync(Guid id);
        Task<IEnumerable<Barber>> GetByBusinessAsync(Guid businessId);
        Task SaveChangesAsync();
    }
}
