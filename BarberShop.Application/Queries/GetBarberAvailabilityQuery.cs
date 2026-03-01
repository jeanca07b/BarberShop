using BarberShop.Application.Queries;
using MediatR;

public record GetBarberAvailabilityQuery(
    Guid BarberId,
    DateTime Date,
    int SlotDurationInMinutes = 30
) : IRequest<List<AvailabilitySlotDto>>;