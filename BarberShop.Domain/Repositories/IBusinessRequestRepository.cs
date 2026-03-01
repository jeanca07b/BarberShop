using BarberShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Domain.Repositories
{
    public interface IBusinessRequestRepository
    {
        Task AddAsync(BusinessRequest request);
        Task<BusinessRequest?> GetByIdAsync(Guid id);
        Task<List<BusinessRequest>> GetPendingAsync();
        Task SaveChangesAsync();

    }
}
