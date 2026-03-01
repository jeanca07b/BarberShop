using BarberShop.Application.DTOs.Barber;
using BarberShop.Application.Mappings;
using BarberShop.Domain.Repositories;

namespace BarberShop.Application.Services
{
    public class BarberService : IBarberService
    {
        private readonly IBarberRepository _repository;

        public BarberService(IBarberRepository repository)
        {
            _repository = repository;
        }

        

        public async Task<IEnumerable<BarberResponse>> GetByBusinessAsync(Guid businessId)
        {
            var barbers = await _repository.GetByBusinessAsync(businessId);

            return barbers.Select(BarberMapper.ToResponse);
        }

        public async Task<BarberResponse?> GetByIdAsync(Guid id)
        {
            var barber = await _repository.GetByIdAsync(id);

            if (barber is null)
                return null;

            return BarberMapper.ToResponse(barber);
        }

        public async Task<BarberResponse?> UpdateAsync(Guid id, UpdateBarberRequest request)
        {
            var barber = await _repository.GetByIdAsync(id);

            if (barber is null)
                return null;

            barber.Update(
                request.FullName,
                request.Phone,
                request.Description
            );

            await _repository.SaveChangesAsync();

            return BarberMapper.ToResponse(barber);
        }

        public async Task<bool> DeactivateAsync(Guid id)
        {
            var barber = await _repository.GetByIdAsync(id);

            if (barber is null)
                return false;

            barber.Deactivate();

            await _repository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ActivateAsync(Guid id)
        {
            var barber = await _repository.GetByIdAsync(id);

            if (barber is null)
                return false;

            barber.Activate();

            await _repository.SaveChangesAsync();

            return true;
        }
    }
}