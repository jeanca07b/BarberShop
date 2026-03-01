using BarberShop.Domain.Entities;
using BarberShop.Domain.Enums;
using BarberShop.Domain.Repositories;
using BarberShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Infrastructure.Repositories
{
    public class BusinessRequestRepository : IBusinessRequestRepository
    {
        private readonly BarberShopDbContext _context;

        public BusinessRequestRepository(BarberShopDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(BusinessRequest request)
        {
            await _context.BusinessRequests.AddAsync(request);
        }

        public async Task<BusinessRequest?> GetByIdAsync(Guid id)
        {
            return await _context.BusinessRequests
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<BusinessRequest>> GetPendingAsync()
        {
            return await _context.BusinessRequests
                .Where(x => x.Status == BusinessRequestStatus.Pending)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
