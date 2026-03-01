using BarberShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarberShop.Application.DTOs.Barber;
public interface IBarberService
{
    Task<IEnumerable<BarberResponse>> GetByBusinessAsync(Guid businessId);
    Task<BarberResponse?> GetByIdAsync(Guid id);
    Task<BarberResponse?> UpdateAsync(Guid id, UpdateBarberRequest request);
    Task<bool> DeactivateAsync(Guid id);
    Task<bool> ActivateAsync(Guid id);
}

