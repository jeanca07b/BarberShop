using BarberShop.Domain.Entities;
using BarberShop.Domain.Enums;
using BarberShop.Domain.Repositories;
using MediatR;

public class GetAppointmentsByBarberAndDateQueryHandler
    : IRequestHandler<GetAppointmentsByBarberAndDateQuery, List<AppointmentTimeDto>>
{
    private readonly IAppointmentRepository _appointmentRepository;

    public GetAppointmentsByBarberAndDateQueryHandler(
        IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task<List<AppointmentTimeDto>> Handle(
        GetAppointmentsByBarberAndDateQuery request,
        CancellationToken cancellationToken)
    {
        var appointments = await _appointmentRepository
            .GetByBarberAndDateAsync(request.BarberId, request.Date);

        return appointments
            .Where(a => a.Status != AppointmentStatus.Cancelled)
            .Select(a => new AppointmentTimeDto(
                a.StartTime,
                a.EndTime))
            .ToList();
    }
}