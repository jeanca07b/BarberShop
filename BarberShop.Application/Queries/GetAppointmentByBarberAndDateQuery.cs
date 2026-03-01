using MediatR;
public record AppointmentTimeDto(
    TimeSpan StartTime,
    TimeSpan EndTime
);
public record GetAppointmentsByBarberAndDateQuery(
    Guid BarberId,
    DateTime Date
) : IRequest<List<AppointmentTimeDto>>;

