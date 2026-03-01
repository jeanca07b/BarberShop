using BarberShop.Application.DTOs.Business;
using BarberShop.Domain.Entities;
using BarberShop.Domain.Enums;
using BarberShop.Domain.Repositories;
using BarberShop.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.Services
{
    public class BusinessRequestService
    {
        private readonly IBusinessRequestRepository _requestRepo;
        private readonly IBusinessRepository _businessRepo;
        private readonly IUserRepository _userRepo;

        public BusinessRequestService(
            IBusinessRequestRepository requestRepo,
            IBusinessRepository businessRepo,
            IUserRepository userRepo)
        {
            _requestRepo = requestRepo;
            _businessRepo = businessRepo;
            _userRepo = userRepo;
        }

        public async Task CreateRequestAsync(CreateBusinessRequest dto, Guid userId)
        {
            var request = new BusinessRequest(
                userId,
                dto.Name,
                dto.Description,
                dto.Phone,
                dto.Street,
                dto.City,
                dto.State,
                dto.Country,
                dto.Latitude,
                dto.Longitude,
                dto.PlaceId
            );

            await _requestRepo.AddAsync(request);
            await _requestRepo.SaveChangesAsync();
        }

        public async Task ApproveAsync(Guid requestId)
        {
            var request = await _requestRepo.GetByIdAsync(requestId);

            if (request is null)
                throw new Exception("Request not found");

            if (request.Status != BusinessRequestStatus.Pending)
                throw new Exception("Already processed");

            var address = new Address(
                request.Street,
                request.City,
                request.State,
                request.Country,
                request.Latitude,
                request.Longitude,
                request.PlaceId
            );

            var business = new Business(
                request.Name,
                request.Description,
                address,
                request.Phone,
                request.UserId
            );

            await _businessRepo.AddAsync(business);

            var user = await _userRepo.GetByIdAsync(request.UserId);
            user!.ChangeRole(UserRole.Owner);

            request.Approve();

            await _businessRepo.SaveChangesAsync();
            await _requestRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<BusinessRequest>> GetPendingAsync()
        {
            return await _requestRepo.GetPendingAsync();
        }

        public async Task RejectAsync(Guid id)
        {
            var request = await _requestRepo.GetByIdAsync(id);

            if (request is null)
                throw new Exception("Request not found");

            request.Reject();

            await _requestRepo.SaveChangesAsync();
        }
    }
}
