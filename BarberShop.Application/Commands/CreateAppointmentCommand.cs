using MediatR;
using BarberShop.Application.Common;

public record CreateAppointmentCommand(
    Guid BusinessId,
    Guid CustomerId,
    Guid BarberId,
    Guid ServiceId,
    DateTime Date,
    TimeSpan StartTime
) : IRequest<Result<Guid>>;
