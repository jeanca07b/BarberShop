using BarberShop.Domain.Entities;
using BarberShop.Domain.Repositories;
using BarberShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly BarberShopDbContext _context;

        public CustomerRepository(BarberShopDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
        }

        public async Task<Customer?> GetByIdAsync(Guid id)
        {
            return await _context.Customers
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Customer?> GetByEmailAsync(string email)
        {
            return await _context.Customers
                .FirstOrDefaultAsync(c => c.Email.Value == email);
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public void Update(Customer customer)
        {
            _context.Customers.Update(customer);
        }

        public void Remove(Customer customer)
        {
            _context.Customers.Remove(customer);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
